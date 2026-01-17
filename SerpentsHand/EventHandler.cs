using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using Respawning;
using Respawning.Waves;
using SerpentsHand.ApiFeatures;
using UncomplicatedCustomRoles.Extensions;

namespace SerpentsHand;

internal static class EventHandler
{
    public static void OnEscaping(PlayerEscapingEventArgs ev)
    {
        if (ev.Player.DisarmedBy == null || !ev.Player.IsDisarmed ||
            !ev.Player.DisarmedBy.TryGetSummonedInstance(out var customRole) ||
            customRole.Role.Id != (SerpentsHand.Singleton.Config?.ShRole.Id ?? 4000))
            return;
        LogManager.Debug($"Prevented SH escape for {ev.Player.Nickname}.");
        ev.IsAllowed = false;
        ev.Player.SetCustomRole(4000);

        var cfg = SerpentsHand.Singleton?.Config;
        if (cfg == null) return;
        if (cfg.EscapePointInfluence != 0)
            FactionInfluenceManager.Add(Faction.SCP, cfg.EscapePointInfluence);
        if (cfg.EscapeTimeInfluence != 0)
            WaveManager.AdvanceTimer(Faction.SCP, cfg.EscapeTimeInfluence);
    }

    public static void OnWaveTrigger(SpawnableWaveBase wave1)
    {
        LogManager.Debug($"Wave triggered: {wave1.GetType().Name}");
        if (wave1 is not CustomTimeBasedWave wave) return;
        LogManager.Debug($"{wave.GetType().Name} is a CustomTimeBasedWave.");
        if (wave is not IAnimatedWave animateWave) return;
        LogManager.Debug($"{animateWave.GetType().Name} is an IAnimatedWave.");
        var duration = animateWave.AnimationDuration;
        animateWave.IsAnimationPlaying = true;
        LogManager.Debug($"Playing {animateWave.GetType().Name} animation for {duration} seconds.");
        if (RespawnWaves.PrimaryChaosWave != null) RespawnWaves.PrimaryChaosWave.PlayRespawnEffect();
        Timing.CallDelayed(duration, () =>
        {
            LogManager.Debug($"Stopping {animateWave.GetType().Name} animation.");
            animateWave.IsAnimationPlaying = false;
        });
    }

    public static void OnWaitingForPlayers()
    {
        ApiManager.CheckForUpdates();
    }
}