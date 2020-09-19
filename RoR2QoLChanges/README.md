This plugin is a collection of Quality of Life changes for Risk of Rain 2's 1.0 release update. All options are configurable via the configuration file.

**BUG REPORTS AND SUGGESTIONS**: Please submit an Issue Ticket at https://github.com/MapleWheels/RoR2_QoLChanges/issues/

**Changelog**

0.2.1: 
	- First Thunderstore published build.
	
**!0.2.2: Fixed bugs from Risk of Risk of 2 v1.0.1.1 update.**

0.2.3: 
	- Quick Readme fix. No need to update.
	
0.2.4: 
	- Added OnKill-Proc Chance feature to Engineer's turrets.
	
0.2.5: 
	- Description fixes.
	
0.2.6: 
	- Squid Polyp: Health no longer decays over time. 
	
0.2.7:
	- Commando Alt_Special/R: Grenades now are now Sticky/stick to the target.
	- Commando's Grenades' damage multiplier changed from 700% to 1000%.
	
0.2.8
	- HOTFIX: Commando's grenade damage properly set to 1000% at the centre.
	
0.3.0: 
	- **IMPORTANT!!: Please delete your configuration file for this mod in ~BepInEx/config/!!**
	- **New**: Warbanner Buffs:
		- Added new Warbanner Buffs: Attack speed bonus now scales with warbanner item count at +3%/level.
		- The scaling portion of Warbanner's attack speed buff is stackable from overlapping war banners.
	- Fixed a bug with Artificer's M1 cooldown scaling calculation.
	- Captain:
		- Reduced the size scaling on Captain's healing beacom from 1.25m/level to 1m/level.
		- Reduced the scaling healing amount from 0.75%HP/level to 0.5%HP/level.
	- Fixed a bug with Bleed where the damage multiplier from the Configuration file was not being applied.

0.3.1:
	- *Bugfix edition!*
	- Re-applied the fix to Artificer's M1 that got undone by poor version control practices.
	- Fixed an interaction bug between Fresh Meat and debuffs that stop natural regen.

0.3.2:
	- *Bugfix edition!*
	- **Now compatible with Rein's Sniper Mod**

Note: This plugin requires the following modding libraries:
- [BepInEx](https://github.com/BepInEx/BepInEx) v5.3.x or higher
- [BepInEx.Extensions](https://github.com/MapleWheels/BepInEx_Extensions) v1.0.1.2 or higher.
- [R2API](https://github.com/risk-of-thunder/R2API) (Risk of Rain 2 build 1.0.0.5 or higher)


**Feature-Set:**

**Items, Procs & Mechanics:**

- Item - Fresh Meat:
	- *Completed*: Now heals for 2HP (increases with level) + 0.5% (+0.25%/stack) Max HP per second for 3 (+3/stack) seconds.
	
- Item - Gorag's Opus:
	- *Planned*: Buff time increased from 7 to 8 seconds (to match War Horn).

- Item - Squid Polyp:
	- *Completed*: Squid Polyp's health no longer decays.
	
- Item - Warbanner:
	- *Completed*: Warbanner now grants 30% (+3%/stack) attack speed.
	- *Completed*: Warbanner's per-stack attack speed buffs will stack from overlapping Warbanner areas.

- Mechanic - Bleeding:
	- *Completed*: Bleed damage reduced from 240% to 180% base damage per stack.
	- *Completed*: Tri-tip Dagger proc chance reduced from 15%/stack to 7%/stack.
	- *Completed*: Tri-Tip Bleeding Debuff time increased from 3 seconds to 4 seconds.
	- *Completed*: Shatterspleen Bleeding Debuff time reduced from 3 seconds to 2 second.
	
**Survivors & Entities**

- Artificer:
	- *Completed*: Fire Bolt (M1) gains +1 max charge per 10 levels (capped at 99).
	- *Completed*: Fire Bolt (M1) gains +1 max charge per 3 Backup Magazines (capped at 99).
	- *Completed*: Fire Bolt (M1)'s cooldown is reduced by 1% per 10% bonus attack speed (capped at 45%). **Note**: This is applied **multiplicatively after** all other cooldown reduction has been applied.

- Captain:
	- **Vanilla BUGFIX**: Captain can now properly use Equipment Item *Gorag's Opus*.
	- *Completed*: Special: Healing Station Beacon's % Max HP healing is increased by +0.5% Max HP/level.
	- *Completed*: Special: Healing Station Beacon increases all healing based on % missing HP, up to 50%.
	- *Completed*: Special: Healing Station Beacon radius increases by 1m per level (vanilla size = 10m).
	- *Planned*: Special: Shock Field Beacon buffs (TBA).
	- *Planned*: M2/Secondary: Tazer Gun buffs (TBA).
	
- Commando:
	- *Completed*: Commando's Grenades (R/Special) now stick when they land instead of bouncing.
	- *Completed*: Commando's Grenades' damage multiplier changed **from 700% to 1000%**.

- Engineer: The Best Class
	- *Completed*: Carbonizer Turrets walking speed is set at Engineer's sprinting speed. **Note** Sprinting is used conservatively by the AI Director to help the turrets catch up to the player and cannot attack while sprinting.
	- *Completed*: Harpoon Missiles' Targeting/Painter Mode enemy lock on range increased.
	- *Completed*: Engineer has a 10% chance to receive the *OnKill-Proc Effect* from his turrets (IE: Soulbound, Topaz Brooch, Fresh Meat, etc).
	
**Integrations**
- *Planned*: Item Stats Mod: In-Game stats reflect QoL changes.
- *Planned*: Item Stats Mod: Data taken from in-game sources instead of the current lookup Dictionary.

