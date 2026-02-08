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
        builder.Append(SerpentsHand.Singleton.Config?.ShWaveAnnouncement);
        subtitles = [];
    }
}