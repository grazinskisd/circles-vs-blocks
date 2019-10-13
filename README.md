# Circles vs Blocks

Unity version: 2018.4.0f1

# App

Link to apk: https://drive.google.com/file/d/1eqTu5c_5z-vFlHDqVckAPkP_ctcTJ2mO/view?usp=sharing

* Clicking on the red square in the middle of the screen gives gold
* Current gold ammount is displayed on the top of the screen
* There are 2 buttons on the bottom panel, left one is for upgrading your level and right one is for purchasing helpers (circles).
After 5 circles have been purchased, the right button will disappear. The buttons are only active if a purchase can be made.
* After purchasing a helper, it will appear on the screen. It displays its level and clicking on the helper will upgrade its level
if enough gold has been gathered.
* Whenever a purchase is made or some resources are earned, a text element appears on the screen and shows the ammount.
* Since large numbers are used, for more pleasant display a truncated version of the number is used, for example 1000000 is 
displayed as 1M.
* Scaling coefficients are downloaded and parsed from a file in my google drive, if they can not be reached for some reason, the app
will not go past "Loading..." screen. However, it is possible to enable debug mode in the editor and not rely of loading this file.
* I used this generated color pallete: http://paletton.com/#uid=75z0u0krZuehqDamtvNuGozzjjr
