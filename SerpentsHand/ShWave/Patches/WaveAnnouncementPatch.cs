using System.Collections.Generic;
using System.Text;
using Cassie;
using HarmonyLib;
using NorthwoodLib.Pools;
using Respawning.Announcements;
using Respawning.Waves;

namespace SerpentsHand.ShWave.Patches;

[HarmonyPatch(typeof(WaveAnnouncementBase), nameof(WaveAnnouncementBase.PlayAnnouncement))]
public static class WaveAnnouncementPatch
{
    public static bool Prefix(WaveAnnouncementBase __instance, List<ReferenceHub> spawnedPlayers, IAnnouncedWave wave)
    {
        if (wave is not SerpentsHandWave || string.IsNullOrEmpty(SerpentsHand.Singleton.Config?.ShWaveAnnouncement)) return true;
        StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
        __instance.CreateAnnouncement(stringBuilder, spawnedPlayers, out _);
        CassieTtsPayload payload = new(StringBuilderPool.Shared.ToStringReturn(stringBuilder), SerpentsHand.Singleton.Config?.ShWaveSubtitle);
        new CassieWaveAnnouncement(wave, payload).AddToQueue();
        return false;
    }
}