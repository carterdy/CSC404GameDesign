# CSC404GameDesign
Repo for Tandem - https://madrash.itch.io/tandem

What works (as of my initial commit):

* Players can move and jump with a controller
  * If you want to use a keyboard again, you'll have to go into the input manager and switch the axis inputs to keyboard/mouse
* The active player can be toggled with the Y button on an Xbox controller (triangle on ps4 assuming it goes by position?)
* If the active player is blue (player 1), they can jump on the blue (ice) blocks.  Similarly, orange (player 2) can jump on the orange (fire) blocks when they're active

As of 29/02/16:

* Players share movement with steering from the top player.
* Can no longer switch while in mid-air


## PlayerSwitching branch

This branch is for implementing the players 'riding' eachother and functionality for switching between who is top and who is bottom

## ArcherBasicAttack Branch

This Branch Relates to this Trello ticket:
https://trello.com/c/sWwxjPPu/2-controls-fire-archer-basic-attack

Description:
This branch will be use to implement basic archer shots.
- First will be a Straight Shot
- Followed by an Arced Shot

In addition, the archer will have an aim restriction of a 180 cone. This may be changed later for balance and feel.

## SimpleAI-Navmesh Branch

Simple AI that follows player in set threshold and maintain distance from the player
