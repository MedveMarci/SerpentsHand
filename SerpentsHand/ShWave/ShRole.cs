using System.Collections.Generic;
using PlayerRoles;
using SerpentsHand.ApiFeatures;
using UncomplicatedCustomRoles.API.Enums;
using UncomplicatedCustomRoles.API.Features;
using UncomplicatedCustomRoles.API.Features.Behaviour;
using UncomplicatedCustomRoles.API.Features.CustomModules;
using UncomplicatedCustomRoles.Manager;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SerpentsHand.ShWave;

public class ShRole : EventCustomRole
{
    [YamlIgnore] public override int Id { get; set; } = 4000;

    [YamlIgnore] public override string Name { get; set; } = "<color=#FF96DE>Serpent's Hand</color>";

    [YamlIgnore] public override bool OverrideRoleName { get; set; } = true;

    public override string Nickname { get; set; } = "";

    [YamlIgnore] public override string CustomInfo { get; set; } = "<color=#C50000>SCP</color>";

    public override string BadgeName { get; set; } = "";
    public override string BadgeColor { get; set; } = "";
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    [YamlIgnore] public override Team? Team { get; set; } = PlayerRoles.Team.SCPs;
    public override RoleTypeId RoleAppearance { get; set; } = RoleTypeId.Tutorial;
    [YamlIgnore] public override List<Team> IsFriendOf { get; set; } = [PlayerRoles.Team.SCPs];
    public override HealthBehaviour Health { get; set; } = new();
    public override AhpBehaviour Ahp { get; set; } = new();
    public override HumeShieldBehaviour HumeShield { get; set; } = new();
    public override List<Effect> Effects { get; set; } = [];
    public override StaminaBehaviour Stamina { get; set; } = new();
    public override int MaxScp330Candies { get; set; } = 2;
    [YamlIgnore] public override bool CanEscape { get; set; } = false;
    [YamlIgnore] public override Dictionary<string, string> RoleAfterEscape { get; set; } = new();
    public override Vector3 Scale { get; set; } = Vector3.one;

    public override string SpawnBroadcast { get; set; } =
        "You are a member of the <color=#FF96DE>Serpent's Hand</color>.\nWork with the <color=#C50000>SCPs</color> to eliminate all threats.";

    public override ushort SpawnBroadcastDuration { get; set; } = 10;
    public override string SpawnHint { get; set; } = "";
    public override float SpawnHintDuration { get; set; } = 0;
    public override Dictionary<ItemCategory, sbyte> CustomInventoryLimits { get; set; } = new();

    public override List<ItemType> Inventory { get; set; } =
        [ItemType.GunCrossvec, ItemType.Medkit, ItemType.Adrenaline, ItemType.ArmorCombat];

    public override List<uint> CustomItemsInventory { get; set; } = [];

    public override Dictionary<ItemType, ushort> Ammo { get; set; } = new()
    {
        { ItemType.Ammo9x19, 120 }
    };

    public override float DamageMultiplier { get; set; } = 1;

    [YamlIgnore] public override SpawnBehaviour SpawnSettings { get; set; } = new()
    {
        Spawn = SpawnType.RoleSpawn,
        SpawnRoles = [RoleTypeId.ChaosRifleman]
    };

    public override List<object> CustomFlags { get; set; } = [];

    [YamlIgnore] public override bool IgnoreSpawnSystem { get; set; } = true;

    public override void OnSpawned(SummonedCustomRole role)
    {
        LogManager.Debug($"Applying Serpent's Hand role modules on spawn. Player: {role.Player.Nickname}");
        role.AddModule(typeof(SilentAnnouncer));
        role.AddModule(typeof(ColorfulNickname), new Dictionary<string, object> { { "color", "#FF96DE" } });
        role.AddModule(typeof(ColorfulRaName), new Dictionary<string, object> { { "color", "#FF96DE" } });
        base.OnSpawned(role);
    }
}