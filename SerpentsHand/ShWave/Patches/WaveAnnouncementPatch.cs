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
        if (wave is not SerpentsHandWave) return true;
        StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
        __instance.CreateAnnouncement(stringBuilder, spawnedPlayers, out _);
        var customSubtitle = string.IsNullOrEmpty(SerpentsHand.Singleton.Config?.ShWaveSubtitle)
            ? "<pos=-0.8%,10em> <color=#FF96DE>C.A.S.S.I.E : </color></pos>Security Alert. Serpents Hand activity detected. All security personnel must proceed with emergency protocols."
            : SerpentsHand.Singleton.Config.ShWaveSubtitle;
        CassieTtsPayload payload = new CassieTtsPayload(StringBuilderPool.Shared.ToStringReturn(stringBuilder), customSubtitle);
        new CassieWaveAnnouncement(wave, payload).AddToQueue();
        return false;
    }
}