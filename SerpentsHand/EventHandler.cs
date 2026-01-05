using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using MapGeneration;
using MEC;
using PlayerRoles;
using ProjectMER.Features;
using ProjectMER.Features.Serializable.Schematics;
using Respawning;
using Respawning.Waves;
using SerpentsHand.Managers;
using UncomplicatedCustomRoles.Extensions;
using UnityEngine;

namespace SerpentsHand;

internal sealed class EventHandler
{
    public static void OnEscaping(PlayerEscapingEventArgs ev)
    {
        if (ev.Player.DisarmedBy == null || !ev.Player.IsDisarmed ||
            !ev.Player.DisarmedBy.TryGetSummonedInstance(out var customRole) ||
            customRole.Role.Id != 2)
            return;

        ev.IsAllowed = false;
        ev.Player.SetCustomRole(2);

        var cfg = SerpentsHand.Singleton?.Config;
        if (cfg == null) return;
        if (cfg.EscapePointInfluence != 0)
            FactionInfluenceManager.Add(Faction.SCP, cfg.EscapePointInfluence);
        if (cfg.EscapeTimeInfluence != 0)
            WaveManager.AdvanceTimer(Faction.SCP, cfg.EscapeTimeInfluence);
    }

    public static void OnWaveTrigger(SpawnableWaveBase wave1)
    {
        if (wave1 is not CustomTimeBasedWave wave) return;
        if (wave is not IAnimatedWave animateWave) return;
        var duration = animateWave.AnimationDuration;
        animateWave.IsAnimationPlaying = true;
        if (RespawnWaves.PrimaryChaosWave != null) RespawnWaves.PrimaryChaosWave.PlayRespawnEffect();
        Timing.CallDelayed(duration, () => { animateWave.IsAnimationPlaying = false; });
    }

    public static void OnWaitingForPlayers()
    {
        _ = VersionManager.CheckForUpdatesAsync(SerpentsHand.Singleton.Version);
    }
}