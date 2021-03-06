This plugin is a collection of Quality of Life changes for Risk of Rain 2's 1.0 release update. All options are configurable via the configuration file.

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
	- Commando's Grenades' damage multiplier changed from 400% to 800%.

Note: This plugin requires the following modding libraries:
- [BepInEx](https://github.com/BepInEx/BepInEx) v5.3.x or higher
- [BepInEx.Extensions](https://github.com/MapleWheels/BepInEx_Extensions) (**Included**) v1.0.0.4 or higher.
- [R2API](https://github.com/risk-of-thunder/R2API) (Risk of Rain 2 build 1.0.0.5 or higher)


**Feature-Set:**

**Items, Procs & Mechanics:**

- Item - Fresh Meat:
	- *Completed*: Now heals for 2HP (increases with level) + 0.5% (+0.25%/stack) Max HP per second.
	
- Item - Gorag's Opus:
	- *Planned*: Buff time increased from 7 to 8 seconds (to match War Horn).

- Item - Squid Polyp:
	- *Completed*: Squid Polyp's health no longer decays.

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
	- *Completed*: Special: Healing Station Beacon's % Max HP healing is increased by +0.75% Max HP/level.
	- *Completed*: Special: Healing Station Beacon increases all healing based on % missing HP.
	- *Completed*: Special: Healing Station Beacon radius increases by 1.25 per level (vanilla size = 10).
	- *Planned*: Special: Shock Field Beacon buffs (TBA).
	- *Planned*: M2/Secondary: Tazer Gun buffs (TBA).
	
- Commando:
	- *Completed*: Commando's Grenades (R/Special) now stick when they land instead of bouncing.
	- *Completed*: Commando's Grenades' damage multiplier changed **from 400% to 800%**.

- Engineer: The Best Class
	- *Completed*: Carbonizer Turrets walking speed is set at Engineer's sprinting speed. **Note** Sprinting is used conservatively by the AI Director to help the turrets catch up to the player and cannot attack while sprinting.
	- *Completed*: Harpoon Missiles' Targeting/Painter Mode enemy lock on range increased.
	- *Completed*: Engineer has a 10% chance to receive the *OnKill-Proc Effect* from his turrets (IE: Soulbound, Topaz Brooch, Fresh Meat, etc).
	
**Integrations**
- *Planned*: Item Stats Mod: In-Game stats reflect QoL changes.
- *Planned*: Item Stats Mod: Data taken from in-game sources instead of the current lookup Dictionary.

