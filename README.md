### Hello!

This is my pet project.It's a game prototype work-named "Unsealed".

In short,"Unsealed" is a metroidvania survival with roguelike segments which allow player as engeneer, go down to BlackMesa-type facility which has enters to generated levels to loot and return to Hub, to be able discover secrets and story behind the facility and go deeper.Interaction with facility's systems,diegetic ui,immersive and hardcore game mechanics should make it memorable and intersting experience.

# Current State
Right now I'm almost ready for game testing, I need to build first blockout version of Hub level, make some infrastructional changes and add some more key-mechanics.
After that, if prototype shows good feedback, I need to add enemies and will expand existing features and mechanics. 
## Glimpse on Features:
#### Inventory
![image](https://www.linkpicture.com/q/inventory_3.png)

Inventory is grid-based and inspired by Escape From Tarkov. It's fully customizeable and each module can be configurable via config SO.

+ Items can be interact via context menu(drop,use,equip etc.) ![contextmenu](https://www.linkpicture.com/q/contextmenu.png)
+ Tooltip
+ Different containers have different features and mechanics.
+ Swapable items
+ Drop by drag and realese 

*Not implemented yet:*
+ Item rotation
+ Hotkes
+ Custom cursor^^

------

#### Player Costume
Game character wearing advanced hazard suit with variaty of systems on board:
+ Vitals control
+ Air supply
+ Power supply
+ Networking



All HUD is displayed on costume visor's mesh via RenderTexture.

------

#### Interaction

World interactions appear to player via *OptionMenu*, *Dynamic Crosshair* and *Tooltip*.*OptionMenu* shows possible actions with object considering of conditions(current equipment,items in inventory,state of interactable object and player).Every specific requirement can be manualy added by gamedesigner without code or can be part of interactable object. 
Player can hold to interact, hold and after use equipment to interact, or just fit the requirements.Menu hides unavailable interactions so player never shure that this is only way he can interact with object. 
![optionMenu](https://www.linkpicture.com/q/contextmenu_1.png)

*OptionMenu* is responsive, it blinks green on succesful interaction and red to opposite, option's sprite fills on hold.Also interactions can be hidden and added on runtime.
*Dynamic Crosshair* changes accordingly to type of chosen option in OptionMenu and current equipment. *Tooltip* displays object name and can show messages to player regarding curent action.

------

#### Equipment
Player has two slot for Equipment. There's three types of player's equipment.Common, Weapon and PowerEquipment.Player controls equipmnent via mouse.
PowerEquipment consumes costume's energy but provides more interaction options if enabled.
*Weapons not implemented yet.*

------

#### Level Systems
All levels contains systems which Player have to interact to survive and explore.
+ Power system - controls rooms, doors, light and other systems so player sometimes have to power up some part of the level to go further and explore.
+ Air Supply system defines whether need player use his Air to keep going.
+ Security systems controls entities such Cameras and alarms which can block or change player's path.
+ Level Nentwork system defines what entities on level player can interact via costume console and allows to explore level in different way.

I would like to share more, and I will soon.
