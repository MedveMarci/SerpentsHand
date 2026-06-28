## [1.1.0]

### Added
- `reset_other_waves_on_spawn` option in `sh_wave_config`. When `true`,
  spawning the Serpent's Hand wave also resets the NTF/Chaos respawn timers
  (vanilla behavior). When set to `false`, the SH wave spawns independently and
  no longer pushes the NTF/Chaos waves back to a full respawn timer.

### Changed
- Updated to LabAPI 1.1.7.
- Changed the Serpent's Hand custom role ID and updated to the latest
  UncomplicatedCustomRoles.

### Fixed
- The wave announcement can now be disabled by leaving it empty.
- Fixed custom modules not being applied correctly to spawned members.

## [1.0.0]

### Added
- Initial release: Serpent's Hand custom respawn wave that spawns on the SCP
  faction, with its own announcement, custom role, keycard, objectives and
  milestone-based respawn tokens.
