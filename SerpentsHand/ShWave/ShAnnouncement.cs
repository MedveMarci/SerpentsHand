using System.Collections.Generic;
using System.Text;
using Respawning.Announcements;
using Subtitles;

namespace SerpentsHand.ShWave;

public class ShAnnouncement : WaveAnnouncementBase
{
    public override void CreateAnnouncement(
        StringBuilder builder,
        List<ReferenceHub> spawnedPlayers,
        out SubtitlePart[] subtitles)
    {
        builder.Append(string.IsNullOrEmpty(SerpentsHand.Singleton.Config?.ShWaveAnnouncement)
            ? "SECURITY ALERT. Serpents Hand activity detected. All security personnel must proceed with emergency protocol _SUFFIX_PLURAL_REGULAR."
            : SerpentsHand.Singleton.Config.ShWaveAnnouncement);
        subtitles = [];
    }
}