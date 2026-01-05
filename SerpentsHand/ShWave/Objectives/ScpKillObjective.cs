using System;
using System.Linq;
using System.Text;
using Cassie;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerStatsSystem;
using Respawning;
using Respawning.Objectives;
using Respawning.Waves;
using SerpentsHand.Managers;
using Subtitles;
using UncomplicatedCustomRoles.Extensions;

namespace SerpentsHand.ShWave.Objectives;

public sealed class ScpKillObjective : FactionObjectiveBase, ICustomObjective
{
    private static readonly float ScpKillTimer = SerpentsHand.Singleton.Config?.ScpKillTimerInfluence ?? -2;
    private static readonly float ScpKillInfluence = SerpentsHand.Singleton.Config?.ScpKillPointInfluence ?? 1;

    private readonly int _usurpationIndex;

    public ScpKillObjective()
    {
        _usurpationIndex = FactionInfluenceManager.Objectives.FindIndex(p => p is HumanKillObjective);
    }

    public override void OnInstanceDestroyed()
    {
        base.OnInstanceDestroyed();
        PlayerStats.OnAnyPlayerDied -= OnKill;
    }

    public override void OnInstanceCreated()
    {
        base.OnInstanceCreated();
        PlayerStats.OnAnyPlayerDied += OnKill;
    }

    private void OnKill(ReferenceHub victimHub, DamageHandlerBase dhb)
    {
        if (dhb is not AttackerDamageHandler &&
            dhb.DeathScreenText != DeathTranslations.PocketDecay.DeathscreenTranslation) return;
        var attacker = dhb is AttackerDamageHandler adh
            ? adh.Attacker.Hub
            : Player.ReadyList.First(p => p.Role is RoleTypeId.Scp106).ReferenceHub;
            
        if (!attacker) return;
        var killer = Player.Get(attacker);
        var victim = Player.Get(victimHub);
        if (killer == null || victim == null) return;
        var faction = killer.TryGetSummonedInstance(out var customRole) && customRole.Role.Id == 2
            ? Faction.SCP
            : killer.RoleBase.Team.GetFaction();

        if (!IsValidFaction(faction) || !IsValidEnemy(victim)) return;
        if (ScpKillInfluence != 0)
            GrantInfluence(faction, ScpKillInfluence);
        if (ScpKillTimer != 0)
            ReduceTimer(faction, ScpKillTimer);

        var usurpation = new KillObjectiveFootprint
        {
            InfluenceReward = ScpKillInfluence,
            TimeReward = ScpKillTimer,
            AchievingPlayer = new ObjectiveHubFootprint(attacker),
            VictimFootprint = new ObjectiveHubFootprint(victimHub)
        };

        try
        {
            var killObjective = (HumanKillObjective)FactionInfluenceManager.Objectives[_usurpationIndex];
            killObjective.ObjectiveFootprint = usurpation;
            killObjective.ServerSendUpdate();
        }
        catch (Exception e)
        {
            LogManager.Error("Fail to send objective completion by usurpation: " + e);
        }
    }

    private static bool IsValidEnemy(Player victim)
    {
        var faction = victim.TryGetSummonedInstance(out var customRole) && customRole.Role.Id == 2
            ? Faction.SCP
            : victim.RoleBase.Team.GetFaction();
        return faction != Faction.SCP;
    }

    public override bool IsValidFaction(Faction faction)
    {
        return faction == Faction.SCP;
    }
}