# Serpent's Hand ![Downloads](https://img.shields.io/github/downloads/MedveMarci/SerpentsHand/total) 

An SCP: Secret Laboratory LabApi plugin which adds Serpent's Hand to the game.

# Features

- Adds a fully functional separate Serpent's Hand Wave and Custom Role.
- You can use the `wave`command with it. Aliases: SH, Serpents, SerpentsHand.
- Fully customizable via config.
- Currently, it spawns with the Chaos Insurgency car but in the future I'm planning to add a custom one.
- **Optional [RespawnTimer](https://github.com/MedveMarci/RespawnTimer) integration** — when RespawnTimer is
  installed, the Serpent's Hand wave is registered with it automatically. No extra setup is needed, and the
  plugin works exactly the same without it.

# Installation

- Download
  `SerpentsHand.dll`, [UncomplicatedCustomRoles](https://github.com/UncomplicatedCustomServer/UncomplicatedCustomRoles/releases/latest), [CustomRespawnWaves](https://github.com/SlejmUr/CustomRespawnWaves/releases)
  and `dependencies.zip`.
- Move `SerpentsHand.dll`, `UncomplicatedCustomRoles.dll` and `CustomRespawnWaves.dll` to => /SCP Secret
  Laboratory/LabApi/plugins/global/
- Unzip `dependencies.zip` to => /SCP Secret Laboratory/LabApi/dependencies/global/

# RespawnTimer

If [RespawnTimer](https://github.com/MedveMarci/RespawnTimer/releases/latest) is installed (any of the three variants:
`RespawnTimer.dll`, `RespawnTimer-HSM.dll` or `RespawnTimer-RueI.dll`), Serpent's Hand registers itself with it on
startup. This is a soft dependency - the integration is skipped silently when RespawnTimer is not present.

The following placeholders then become available in `TimerBeforeSpawn.txt` and `TimerDuringSpawn.txt`:

| Placeholder | Description |
|-------------|-------------|
| `{shminutes}` / `{shseconds}` | Serpent's Hand spawn countdown |
| `{shtoken}` | Serpent's Hand respawn tokens |
| `{team}` | Shows `Serpent's Hand` while the SH wave is spawning |
| `{next_team}` | Shows `Serpent's Hand` when the SH wave is the next one able to spawn |

The name is displayed as <code>&lt;color=#FF96DE&gt;Serpent's Hand&lt;/color&gt;</code>, matching the faction color.

Note that `{next_team}` only lists the SH wave once it actually has respawn tokens, so it stays hidden until the wave
is unlocked by the `sh_wave_milestones` config (or by `initial_tokens` in `sh_wave_config`).

# For Support

<div align="left">
<a href='https://discord.gg/KmpA8cfaSA'><img src='https://www.allkpop.com/upload/2021/01/content/262046/1611711962-discord-button.png' height="100"></a>
</div>
