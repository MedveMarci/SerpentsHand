using System;
using System.Linq;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerStatsSystem;
using Respawning;
using Respawning.Objectives;
using SerpentsHand.ApiFeatures;
using UncomplicatedCustomRoles.API.Features;
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
        if (dhb is not AttackerDamageHandler && dhb.DeathScreenText != DeathTranslations.PocketDecay.DeathscreenTranslation) return;
        ReferenceHub attacker = dhb is AttackerDamageHandler adh ? adh.Attacker.Hub : Player.ReadyList.FirstOrDefault(p => p.Role is RoleTypeId.Scp106)?.ReferenceHub;

        if (!attacker) return;
        Player killer = Player.Get(attacker);
        Player victim = Player.Get(victimHub);
        if (killer == null || victim == null) return;
        Faction faction = killer.TryGetSummonedInstance(out SummonedCustomRole customRole) && customRole.Role.Id == (SerpentsHand.Singleton.Config?.ShRole.Id ?? 4000) ? Faction.SCP : killer.RoleBase.Team.GetFaction();

        if (!IsValidFaction(faction) || !IsValidEnemy(victim)) return;
        if (ScpKillInfluence != 0)
            GrantInfluence(faction, ScpKillInfluence);
        if (ScpKillTimer != 0)
            ReduceTimer(faction, ScpKillTimer);

        KillObjectiveFootprint usurpation = new()
        {
            InfluenceReward = ScpKillInfluence,
            TimeReward = ScpKillTimer,
            AchievingPlayer = new ObjectiveHubFootprint(attacker),
            VictimFootprint = new ObjectiveHubFootprint(victimHub)
        };

        try
        {
            HumanKillObjective killObjective = (HumanKillObjective)FactionInfluenceManager.Objectives[_usurpationIndex];
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
        Faction faction = victim.TryGetSummonedInstance(out SummonedCustomRole customRole) && customRole.Role.Id == (SerpentsHand.Singleton.Config?.ShRole.Id ?? 4000) ? Faction.SCP : victim.RoleBase.Team.GetFaction();
        return faction != Faction.SCP;
    }

    public override bool IsValidFaction(Faction faction)
    {
        return faction == Faction.SCP;
    }
}