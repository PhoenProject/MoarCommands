# MoarCommands

Plugin that adds some widely wanted commands.
Some of the SCP-096 commands can be buggy if used too close together (Such as cooldown after using enrage)

| Config Option | Default value | Description  |
|:-------------:|:---------------:|:---------------:|
| mc_addexp_ranks | owner,admin | Which server roles can give SCP-079 additional EXP |
| mc_light_flicker_ranks | owner,admin | Which server roles can flicker the lights in LCZ and HCZ |
| mc_light_failure_announcement | true | If there should be a cassie announcement when the lights are flickered |
| mc_rslot_ranks | owner | Which server roles can give reserved slot from ingame |
| mc_rage_state_ranks | owner | Which server roles can forcefully change SCP-096's rage state (Slightly buggy, and may be incompatable with plugins that alter SCP-096's rage system) |

| Command | variables | Description |
|:-------------:|:---------------:|:---------------:|
| addexp | Value | Adds EXP to SCP-079 |
| flicker | | Flickers the lights in LCZ and HCZ |
| rslot | PlayerID/SteamID/Name | Grants a reserved slot to a player |
| panic | | Places SCP-096 into a panic state (As if they had just been looked at) |
| enrage | | Enrages SCP-096 (When they go super fast, and can break doors) |
| cooldown | | Cools down SCP-096 from an enraged state (they can't be triggered) |
| docile | | Makes SCP-096 docile (They can be triggered when looked at) |