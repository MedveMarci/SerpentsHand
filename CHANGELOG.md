## [1.2.0]

### Added

- **RespawnTimer integration.** When RespawnTimer is installed, the Serpent's Hand wave is registered with it on
  startup, providing the `{shminutes}`, `{shseconds}` and `{shtoken}`
  placeholders and making `{team}` and `{next_team}` display `Serpent's Hand`.

### Changed

- RespawnTimer is now a soft dependency resolved through reflection instead of a hard assembly reference. Previously the
  plugin was compiled against `RespawnTimer-HSM.dll`, so it failed to load on servers without that exact variant
  installed; it now works with all three variants (`RespawnTimer`, `RespawnTimer-HSM`, `RespawnTimer-RueI`) and with
  none of them.

## [1.1.0]

### Added

- `reset_other_waves_on_spawn` option in `sh_wave_config`. When `true`, spawning the Serpent's Hand wave also resets the
  NTF/Chaos respawn timers (vanilla behavior). When set to `false`, the SH wave spawns independently and no longer
  pushes the NTF/Chaos waves back to a full respawn timer.

### Changed

- Updated to LabAPI 1.1.7.
- Changed the Serpent's Hand custom role ID and updated to the latest UncomplicatedCustomRoles.

### Fixed

- The wave announcement can now be disabled by leaving it empty.
- Fixed custom modules not being applied correctly to spawned members.
- Fixed an `InvalidOperationException` in `ScpKillObjective` when a player died from Pocket Dimension decay while no
  SCP-106 was present on the server.

## [1.0.0]

### Added

- Initial release: Serpent's Hand custom respawn wave that spawns on the SCP faction, with its own announcement, custom
  role, keycard, objectives and milestone-based respawn tokens.
