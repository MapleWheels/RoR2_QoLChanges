This plugin is a collection of Quality of Life changes for Risk of Rain 2's 1.0 release update. All options are configurable via the configuration file.

Note: This plugin requires the following modding libraries:
- [BepInEx](https://github.com/BepInEx/BepInEx) v5.3.x or higher
- [BepInEx.Extensions](https://github.com/MapleWheels/BepInEx_Extensions) (Included) v1.0.0.2 or higher.
- [R2API](https://github.com/risk-of-thunder/R2API) (Risk of Rain 2 build 1.0.0.5 or higher)


**Feature-Set:**
**Items, Procs & Mechanics:**
- Item - Fresh Meat:
	- *Completed*: Now heals for 0.5% (+0.25%/stack) Max HP.
	
- Item - Gorag's Opus:
	- *Planned*: Buff time increased from 7 to 8 seconds (to match War Horn).
	- *Planned*: Cooldown time increased from 45 seconds to 55 seconds.

- Mechanic - Bleeding:
	- *Planned*: Bleed damage reduced from 240% to 180% base damage per stack.
	- *Planned*: Tri-tip Dagger proc chance reduced from 15%/stack to 6%/stack.
	- *Planned*: Tri-Tip Bleeding Debuff time increased from 4 seconds to 6 seconds.
	- *Planned*: Shatterspleen Bleeding Debuff time reduced from 3 seconds to 1 second.
	
**Survivors & Entities**
- Artificer:
	- *Planned*: Fire Bolt (M1) gains +1 max charge per 10 levels (capped at +2 charges).
	- *Planned*: Fire Bolt (M1) gains +1 max charge per 3 Backup Magazines (capped at +2 charges).
	- *Planned*: Fire Bolt (M1)'s cooldown is reduced by 1% per 10% bonus attack speed. **Note**: This is applied **multiplicatively after** all other cooldown reduction has been applied.

- Captain:
	- **Vanilla BUGFIX**: Captain can now properly use Equipment Item *Gorag's Opus*.
	- *Planned*: Special: Healing Station Beacon's % Max HP healing is increased by +0.75% Max HP/level.
	- *Planned*: Special: Beacon Calldown max uses per Stage increased by 1 per 5 levels (capped at 4 beacons).

- Engineer: The Best Class
	- *Planned*: Carbonizer Turrets walking speed is set at Engineer's sprinting speed. **Note** Sprinting is used conservatively by the AI Director to help the turrets catch up to the player and cannot attack while sprinting.
	- *Planned*: Turrets will now re-copy/update their inventory when Engineer picks up an item.
	- *Planned*: Harpoon Missiles' Targeting/Painter Mode enemy lock on range increased.
	- *Planned*: Engineer has a 10% chance to receive the OnKillEnemy Proc from his turrets (IE: Soulbound, Topaz Brooch, Fresh Meat, etc).
	
**Integrations**
- *Planned*: Item Stats Mod: Will reflect mod QoL changes.
- *Planned*: Item Stats Mod: Data will be taken from in-game sources instead of the current lookup Dictionary.