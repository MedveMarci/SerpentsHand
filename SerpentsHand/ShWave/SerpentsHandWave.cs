using System.Collections.Generic;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using Respawning;
using Respawning.Announcements;
using Respawning.Config;
using Respawning.Waves;
using Respawning.Waves.Generic;
using UncomplicatedCustomRoles.Extensions;
using UnityEngine;

namespace SerpentsHand.ShWave;

public class SerpentsHandWave : CustomTimeBasedWave, IAnnouncedWave, ILimitedWave, IAnimatedWave
{
    private List<RespawnTokensManager.Milestone> _milestoneValues = [new(10)];
    public override string Name => "SHWAVE";
    public override Faction TargetFaction => Faction.SCP;

    public override float InitialSpawnInterval =>
        SerpentsHand.Singleton.Config?.ShWaveConfig.InitialSpawnInterval ?? 320f;

    public override IWaveConfig Configuration => SerpentsHand.Singleton.Config?.ShWaveConfig ?? new ShWaveConfig();

    public float AnimationDuration => 13.49f;
    public bool IsAnimationPlaying { get; set; }
    public WaveAnnouncementBase Announcement => new ShAnnouncement();
    public int InitialRespawnTokens { get; set; } = 1;
    public int RespawnTokens { get; set; }

    public override void Init()
    {
        if (Configuration is not ShWaveConfig shWaveConfig)
            return;
        InitialRespawnTokens = shWaveConfig.InitialTokens;

        if (SerpentsHand.Singleton.Config?.ShWaveMilestones != null &&
            SerpentsHand.Singleton.Config.ShWaveMilestones.Count > 0)
            _milestoneValues =
                SerpentsHand.Singleton.Config.ShWaveMilestones.ConvertAll(milestone =>
                    new RespawnTokensManager.Milestone(milestone));

        RespawnTokensManager.Milestones[Faction.SCP] = _milestoneValues;
    }

    public override void PopulateQueue(Queue<RoleTypeId> queueToFill, int playersToSpawn)
    {
        for (var i = 0; i < playersToSpawn; i++) queueToFill.Enqueue(RoleTypeId.CustomRole);
    }

    public override void WaveSpawned(List<Player> spawnedPlayers)
    {
        foreach (var player in spawnedPlayers)
        {
            player.SetCustomRole(SerpentsHand.Singleton.Config?.ShRole.Id ?? 4000);
            Timing.CallDelayed(0.1f,
                () =>
                {
                    KeycardItem.CreateCustomKeycardSite02(player, "Serpent's Hand Keycard", $"SH {player.Nickname}",
                        "Serpent's Hand", new KeycardLevels(2, 3, 2), new Color(1f, 0.588f, 0.87f), Color.black,
                        Color.white, 0);
                });
        }
    }
}