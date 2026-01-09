using System.Collections.Generic;
using PlayerRoles;
using UncomplicatedCustomRoles.API.Enums;
using UncomplicatedCustomRoles.API.Features.Behaviour;
using UncomplicatedCustomRoles.API.Interfaces;
using UncomplicatedCustomRoles.Manager;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SerpentsHand.ShWave;

public class ShRole : ICustomRole
{
    [YamlIgnore]
    public int Id { get; set; } = 2;
    [YamlIgnore]
    public string Name { get; set; } = "<color=#FF96DE>Serpent's Hand</color>";
    [YamlIgnore]
    public bool OverrideRoleName { get; set; } = true;
    public string Nickname { get; set; } = "";
    [YamlIgnore]
    public string CustomInfo { get; set; } = "<color=#C50000>SCP</color>";
    public string BadgeName { get; set; } = "";
    public string BadgeColor { get; set; } = "";
    public RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    public Team? Team { get; set; } = PlayerRoles.Team.SCPs;
    public RoleTypeId RoleAppearance { get; set; } = RoleTypeId.Tutorial;
    public List<Team> IsFriendOf { get; set; } = [PlayerRoles.Team.SCPs];
    public HealthBehaviour Health { get; set; } = new();
    public AhpBehaviour Ahp { get; set; } = new();
    public HumeShieldBehaviour HumeShield { get; set; } = new();
    public List<Effect> Effects { get; set; } = [];
    public StaminaBehaviour Stamina { get; set; } = new();
    public int MaxScp330Candies { get; set; } = 2;
    public bool CanEscape { get; set; } = false;
    public Dictionary<string, string> RoleAfterEscape { get; set; } = new();
    public Vector3 Scale { get; set; } = Vector3.one;
    public string SpawnBroadcast { get; set; } = "You are a member of the <color=#FF96DE>Serpent's Hand</color>.\nWork with the SCPs to eliminate all threats.";
    public ushort SpawnBroadcastDuration { get; set; } = 10;
    public string SpawnHint { get; set; } = "";
    public float SpawnHintDuration { get; set; } = 0;
    public Dictionary<ItemCategory, sbyte> CustomInventoryLimits { get; set; } = new();
    public List<ItemType> Inventory { get; set; } = [ItemType.GunCrossvec, ItemType.Medkit, ItemType.Adrenaline, ItemType.ArmorCombat];
    public List<uint> CustomItemsInventory { get; set; } = [];
    public Dictionary<ItemType, ushort> Ammo { get; set; } = new()
    {
        { ItemType.Ammo9x19, 120 }
    };

    public float DamageMultiplier { get; set; } = 1;
    public SpawnBehaviour SpawnSettings { get; set; } = new()
    {
        Spawn = SpawnType.RoleSpawn,
        SpawnRoles = [RoleTypeId.ChaosRifleman]
    };

    public List<object> CustomFlags { get; set; } =
    [
        "SilentAnnouncer",
        new Dictionary<string, Dictionary<string, object>> { { "ColorfulNickname", new Dictionary<string, object> { { "color", "#FF96DE" } } } },
        new Dictionary<string, Dictionary<string, object>> { { "ColorfulRaName", new Dictionary<string, object> { { "color", "#FF96DE" } } } },
    ];

    public bool IgnoreSpawnSystem { get; set; } = true;
}