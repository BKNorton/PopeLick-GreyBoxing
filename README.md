<h1>
   Description
</h1>
<p>
   This game is still in the early stages of development. It is a side project right now with my efforts and time going into other projects. Our team of two (my partner Anthony and myself), have worked together to write the story and are still discussing potential game mechanics.  

   This is a detective mystery game with horror elements. You play as a detective new to the job and are sent to help locate a missing teenager in the town of Pope Lick. You must investigate, discover clues, and solve the mystery that goes deeper than one missing kid.
</p>

<h2>
   Roles
</h2>
<p>
   It is just the two of us so we are taking on several roles and those roles ares still evolving.
   <h3>
	Anthony
   </h3>
   <p>
	In charge of all graphic assets and world building. He works in Blender. He has also taken the lead on story development. 
   </p>
   <h3>
	Kirk (me)
   </h3>
   <p>
	In charge of all programming, lighting, and other Unity specific tasks. I also contribute to story development.
   </p>
</p>

<h2>
   Key Highlights
</h2>
<ul>
   <li>Player Movement</li>
   <li>Interaction System</li>
   <li>Camera System</li>
   <li>Inventory System</li>
   <li>UI</li>
   <li>Animations</li>
   <li>Lighting</li>
   <li>Input System</li>
</ul>

<h2>
   Progress
</h2>
<p>
	I built essentially what will be the first draft of the player and player controller. Anthony built the first scene (Office Scene) and I added all the textures and did all the lighting design for it. All doors and drawers were made interactable so that the player could open them.
</p>
<p>
   The storyline is still in process but there is a script that maps out the key elements to the story.

   Anthony is building assets and I am still grey-boxing or testing out the player controller and all the assets and actions a player has. 
</p>

<h3>
       Player Movement
</h3>
<p>
	All game objects use Vectors that allow you to track object locations and to move and apply other forces on the Player Controller.
	<ul>
		<li>Walk left, right, forward, and backwards</li>
		<li>Sprint</li>
		<li>Jump</li>
		<li>Crouch</li>
		<li>Quickturn</li>
		<li>Look</li>
	</ul>
	
</p>
<h3>
        Interaction System
</h3>
<p>
	An interactable abstract class was created using the Template Method Pattern that all interactable object scripts inherit from. 
	
The player casts a ray from the center of the cameras FOV. Data from the hitCollider is used with the input system and events to allow the player to interact with different objects with different outcomes.
	<ul>
		   <li>Turn on lights</li>
		   <li>Opening doors</li>
		   <li>Picking up items</li>
		   <li>Talking to another character</li>
	</ul>
</p>

<h3>
	   Camera System
</h3>
<p>
	Allows player to use a camera in-game. 
	
   They can take pictures with the camera and it saves the image in an array in a Film object that stores 5 pictures at a time.

   These pictures can then be used to develop and print the images onto a photo that will be used to help solve the mystery.

   Uses a ray to  detect an object that inherits from the abstract class Photographable, and marks the item so that clues can be tracked as either photographed or notPhotographed.

   The camera system is currently not working, but was working previously.
</p>
   

<h3>
	Inventory System
</h3>
<p>
	A class system that allows items to be stored in containers. The player has its own container (inventory) that is represented in the UI for the player menu.

 Player can use interaction system to pick up items and store them in the inventory.

When finished the player will have more options to interact with items in the inventory like, drop, or place in another container from the inventory.
</p>

<h3>
	UI
</h3>
<ul>
	<li>Crosshairs</li>
	<li>Interactable Item Text (Interactable items will prompt the user with a message displayed in the Player HUD to describe the object or the interaction type.</li>
	<li>Menu (Currently just allows player to pause the game, veiw the menu, and return to the game.</li>
	<li>Player menu (Displays Inventory, Camera with slot for Film, and slots for items to equip, one for each hand)</li>
	<ul><li>Currently only the inventory is interactable. Items can be selected and moved to different inventory slots.</li></ul>
</ul>

<h3>
	Animations
</h3>
<p>
	I believe right now the only animations being used are the ones to simulate the players head bobbing as the walk or run.
</p>

<h3>
	Lighting
</h3>
<p>
	I have been learning how to improve the lighting in the game using Post Processing to add a grainy look, bloom, and other effects.

I also light map baking to create llight maps for the current scenes.
</p>

<p>
	When I first started this project, I did not know about version control. Unfortunately for me that means after breaking my camera system and so far unsuccessfuly trying to fix it, I am not able to revert back to a previous commit.
</p>

<h2>
	Notes
</h2>
</p>
	 I have now placed all current code of this project on GitHub but that is all this repository contains and cannot be ran.
	
   We are now using the version control that is built into Unity (Plastic SCM).

   We are currently rebuilding everything we've made so far.
	
   We are rebuilding because a lot of what we did previously turned out to not be optimized. My code was a mess and was not following good coding practices. Things were not properly decoupled which has lead to the current state of the camera system. 
   
   All assets that Anthony was building were not optimized with low poli counts or properly 
   oriented UVs which was causing Texture issues.
	
   Now that I know how to work with version control and Anthony and I are able to work on the same repository, redefine our workflow and rebuild our game, I will no longer be making any commits to this repository. 
   
   Until I have a better way of including this porject in my portfolio, I will use this repository as a reference for others to 
   explore the code that I wrote. 
   
   I plan to make a website to display screenshots and videos to showcase all the work that we did and plan to recreate in a more optimal and organized way.
<p>
<h2>
	Instructions
</h2>
<p>
	This repository only contains the scripts for this game. All other assets are not included and therfore this game can not be ran from this repository. This is just to include in my portfolio and more details will be added soon.
</p>
 
<h2>
	Updates
</h2>
<p>
	Check back soon for a link to the website to showcase all of our work so far. I also plan to start a social media account and a dev blog for this project as soon as we start it back up.
</p>

 <h2>
	 Office Scene
 </h2>
 <p>
	 This is the first scene. Anthony created all the assets and I added the textures and did the lighting design.
 </p>
 
![OfficeScene](https://github.com/BKNorton/PopeLick-GreyBoxing/assets/112774855/6c738d9e-4d3a-4a0a-84d0-a2d92eb90044)

<h2>
	Pause Menu
</h2>

![Screenshot (3)](https://github.com/BKNorton/PopeLick-GreyBoxing/assets/112774855/28ede830-8131-4c25-acdc-b73fbbfca0ca)
![Screenshot (2)](https://github.com/BKNorton/PopeLick-GreyBoxing/assets/112774855/e3979793-a0de-44e8-bbd3-641eda2f123b)

<h2>
	Camera System
</h2>
<p>
	Player takes a photo and it prints to this gameobject.
</p>

![Screenshot (5)](https://github.com/BKNorton/PopeLick-GreyBoxing/assets/112774855/286359a1-778b-425e-9706-8bf53f1a85c1)
