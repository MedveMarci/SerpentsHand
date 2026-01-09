using System.Linq;
using CommandSystem.Commands.RemoteAdmin;
using CustomRespawnWaves;
using HarmonyLib;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using Respawning;
using SerpentsHand.ShWave;
using SerpentsHand.ShWave.Objectives;
using UncomplicatedCustomRoles.API.Features;
using Version = System.Version;

namespace SerpentsHand;

public class SerpentsHand : Plugin<Config>
{
    public static SerpentsHand Singleton;
    private Harmony _harmony;
    public override string Name => "SerpentsHand";
    public override string Description => "serpents_hand";
    public override string Author => "MedveMarci";
    public override Version Version { get; } = new(1, 0, 1);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    private ShRole _shRole;
    public string githubRepo = "MedveMarci/SerpentsHand";
    
    public override void Enable()
    {
        _harmony = new Harmony(Name + " " + Version);
        _harmony.PatchAll();
        Singleton = this;
        FactionInfluenceManager.Objectives.Add(new ScpKillObjective());
        WaveManager.OnWaveTrigger += EventHandler.OnWaveTrigger;
        PlayerEvents.Escaping += EventHandler.OnEscaping;
        ServerEvents.WaitingForPlayers += EventHandler.OnWaitingForPlayers;
        CustomWaves.RegisterWave();
        CustomRole.Register(_shRole);
        TargetWaveCommandBase.WaveAliases[typeof(SerpentsHandWave)] = ["SH", "Serpents", "SerpentsHand"];
    }
    
    public override void LoadConfigs()
    {
        base.LoadConfigs();
        _shRole = Config != null ? Config.ShRole : new ShRole();
    }

    public override void Disable()
    {
        _harmony = null;
        Singleton = null;
        FactionInfluenceManager.Objectives.RemoveAll(obj => obj is ScpKillObjective);
        CustomWaves.UnRegisterWave();
        WaveManager.OnWaveTrigger -= EventHandler.OnWaveTrigger;
        PlayerEvents.Escaping -= EventHandler.OnEscaping;
        ServerEvents.WaitingForPlayers -= EventHandler.OnWaitingForPlayers;
        CustomRole.Unregister(_shRole);
        TargetWaveCommandBase.WaveAliases.Remove(typeof(SerpentsHandWave));
    }
}