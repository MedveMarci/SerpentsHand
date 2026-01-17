using System.ComponentModel;
using CustomRespawnWaves.Configs;
using YamlDotNet.Serialization;

namespace SerpentsHand.ShWave;

public sealed class ShWaveConfig : CustomWaveConfig
{
    [YamlIgnore] public override string Name { get; set; } = "SHWAVE";

    [YamlIgnore] public override bool IsEnabled { get; set; } = true;

    [Description("Maximum number of Serpents Hand members to spawn in a wave.")]
    public override int MaxWaveSize { get; set; } = 5;

    [Description("Whether to use size percentage instead of max wave size.")]
    public override bool UseSizePercentage { get; set; } = false;

    [Description("The size percentage of players to spawn as Serpents Hand members if UseSizePercentage is true.")]
    public override float SizePercentage { get; set; }

    [Description("The amount of spawn tokens the wave starts with.")]
    public int InitialTokens { get; set; } = 0;

    [Description("The amount of seconds the wave starts with.")]
    public float InitialSpawnInterval { get; set; } = 320f;
}