using System.Collections.Generic;
using System.ComponentModel;
using SerpentsHand.ShWave;

namespace SerpentsHand;

public class Config
{
    [Description("Enable/Disable debug logs.")]
    public bool Debug { get; set; } = false;

    [Description("The announcement when the Serpents Hand wave spawns.")]
    public string ShWaveAnnouncement { get; set; } =
        "SECURITY ALERT. Serpents Hand activity detected. All security personnel must proceed with emergency protocol _SUFFIX_PLURAL_REGULAR.";

    [Description("The subtitle for the Serpents Hand wave announcement.")]
    public string ShWaveSubtitle { get; set; } =
        "<pos=-0.8%,10em> <color=#FF96DE>C.A.S.S.I.E : </color></pos>Security Alert. Serpent's Hand activity detected. All security personnel must proceed with emergency protocols.";

    [Description(
        "The amounts of point to add to the SH Wave when an SCP kills someone. By default, SCPs need to kill 10 people to gain a respawn token.")]
    public int ScpKillPointInfluence { get; set; } = 1;

    [Description("The amount of time to reduce from the SH Wave when an SCP kills someone.")]
    public int ScpKillTimerInfluence { get; set; } = -2;

    [Description("The amount of points to add when a player escapes while he was cuffed by an SH Member.")]
    public int EscapePointInfluence { get; set; } = 1;

    [Description("The amount of time to reduce when a player escapes while he was cuffed by an SH Member.")]
    public int EscapeTimeInfluence { get; set; } = -5;

    [Description("The amounts where the SH Wave will gain respawn tokens.")]
    public List<int> ShWaveMilestones { get; set; } = [10];

    [Description("Configuration for the SH Wave.")]
    public ShWaveConfig ShWaveConfig { get; set; } = new();

    [Description("Configuration for the SH Role.")]
    public ShRole ShRole { get; set; } = new();
}