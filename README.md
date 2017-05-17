# HoloLens, basic Unity demonstrator 

This is a brief demonstrator/script for showing HoloLens to a developer audience. 

It builds a scene from scratch (within a few minutes) and demonstrates:
- How to quickly create a gaze cursor (and all you get for free w/ HoloLens camera and Unity).  
- Voice commands in HoloLens.   
- How to add Spatial Mapping to a scene. Demonstrating how you get occlusion and collision detection for free.  
- Object or Image Recognition using Vuforia.
- 

You can see a (very rough) recording of this demo here. 


# Showing the demo 

##Preparation/Setup 

1. Setup HoloLens'  Mixed Reality Capture for Live Preview. 
2. Pre-position the BB8, and the Yoda model in the unity scene. Here are a few tips: 
	1. When HoloLens app comes up, you should be looking forward (usually towards the audience) at your height. 
	2. You want the BB8 to be some where that it can walk laterally towards your podium (or desk) and collided with something physical. Here is default position for BB8: 
		1. X=4 ( 4 meters to your right), Y = 1 (floating in space so we can demo gravity), and Z = 0 or Z < 1 (no depth from where you are at).  The 4 meters was selected so you can walk towards BB8 and walk around it at < 5 meters, which is further than any roomscale VR that is tethered. 
	3. You want the Yoda some where near the BB8 so it is clear it appears in the scene (X = 5 is 5 meters to your right). You also want it floating in space ( so 1<Y<2 ) so you can show gravity.  Z (deep can still be 0, or a bit behind BB8, so -1<Z<0).
	4. If you are using image recognition w/ Vuforia, print the "YodaStretched.jpg" in the 3rdParty/Vuforia_Database folder). Print this w/ color in 8.5"x11" page. 
	5. If you are not using Vuforia, tweak the #define FAKESCAN in line 0 in scene manager, and just use the "Toggle Laser" command -which was designed for when scanning 3D objects -. With FAKESCAN defined, within a few seconds of you toggling, it will act as if some object had been scanned. 


## Commands & Script

1. Launch the app. 
2. Explain the cursor. it is your gaze. HoloLens is doing all the tracking, and cursor uses Unity's RayCast to detect collisions. Cursor is blue when it does not collide with anything and green when it collides with something.  
2. Look around the room, and have your cursor collide with the BB8 model. 
3. Show the BB8 model. This is a standard, free fbx. Nothing special, a hologram is just any 3D object in Unity. 
4. Show that there is no Yoda next to the BB8. 
5. Scanning an object w/ Vuforia to "summon the Yoda". 
	1. If you are using image recognition, scan the image. 
	2. If you don't have a prop (image or object), just call "Toggle Lase" with FAKESCAN define
3. See Yoda appear.  Walk to it, look around it. Emphasize the "untethered aspect of HoloLens" 
4. Use the "Toggle Scan" voice command to start scanning the floor & walls. 
5. Use the "Toggle Renderer" voice command to show how HoloLens is scanning. The meshes it is finding. 
6. Once you know there is a floor scanned, use the "Gravity" command to turn on gravity on your objects. See them fall and collide with the floor.     
7. Tap on the BB8 to select it, showing the hand gesture of "Tap" .  
8. With BB8 selected, use the "Walk" command and see it animate around the scanned floor.  Emphasize Spatial mapping. 
9. Repeat the "walk" command until BB8 hits something physical and falls over. 
10. When BB8 falls over, "Tap it" to have it stand-up.  
11. Give the "Toggle Scan" command, which will turn-off Unity's spatial mapping, making the floor mesh disappear. With this command, our holograms will disappear into abyss.  That is spatial mapping in action. 




#Building the demo to showcase developers features 

The following steps outline every step in building the demo from scratch. You can have a few of the steps "pre-configured" to not bore your audience too much. 

I suggest the following steps be pre-configured:
1. Setting up Vuforia 
2. Importing the models 
3. 

The following steps are useful for developers to see, so they understand how easy it is to build this scene.  
2. Adding the objects to the scene.  Add one of the holograms. Show them it is a standard 3d model. No magic. 
3. Adding SpatialMapping. It is a simple Create Empty Game object. Add the two components:  SpatialMappingCollider, SpatialMapping Renderer.
2. Adding voice commands. You can just add the SceneManager script and walk audience through it. 
3. Cursor. Just add the SimplestCursor script and walk them through it. 
4. Gestures. Just add TapListener and walk them through it. 





### Creating the project 

1. Create a new project in Unity. 
	1. Name does not matter. Make it a 3D project. You do not need Unity analytics so feel free to 'disable' that option.  


###Setting up Vuforia 
2. Import the Vuforia SDK into Unity.  Here are the detailed steps: 
	1. Downloading the Unity SDK from https://developer.vuforia.com/downloads/sdk  
	2. Import the Vuforia SDK 
		1. In Unity, go to *Assets-> Import Package-> Custom Package*
		1. Select the Vuforia<version>.UnityPackage, then click **Open**
		2. On the Import dialog that comes up, you can ensure everything is selected and just click **Import** to import everything; or, you could also be selective and exclude iOS/Android files by unchecking these within the plugins folder. 
		3. If you get a warning asking you to allow Unity to "Upgrade your scripts so they don't reference obsolete APIs" then **click "I made a backup, go Ahead"**. 



1. Import your Vuforia Assets database
	Note: If you want to use the one included with this demo, it is in the 3rdParty folder at the root of this repo. 
	1. Go to *Assets -> Import Package -> Custom Package*. Select YodaImages.UnityPackage to import the images database. 
	2. Click **Import** in the Import Dialog to import all the assets.  This should import assets to the Streaming Assets folder and the Editor/QCAR folder. 


1. Configure your Vuforia Key. 
		1. In the Vuforia menu, go to configuration. 
		2. In the App License Key, enter your license key obtained from Vuforia's license manager (https://developer.vuforia.com/targetmanager/licenseManager/licenseListing).  

The license is 'free' for testing/demo purposes, but it is restricted to 1000 recognitions per month, and a limited number of targets & VuMarks.  


1. Configure Vuforia default settings in property Inspector: 
		1. Max simultaneous tracked Images = 2 
		2. Max simultaneous tracked Objects = 2
		3. Digital EyeWear - Type = Optical See-Through 
		4. Digital Eyewear See Through Config = HoloLens 
		5. Load YodaImages Database = Checked
		6. Activate Database = Checked 
		7. Enable Video Background = Unchecked 
		8. Webcam -> Disable Vuforia Play Mode  = Checked 
		9. Overflow Geometry = Stencil (already the default) 
		10. Matte Shader = Clipping mask ( already the default)
 


### Preparing our scene

1. Drag the Vuforia ImageTarget prefab into the scene hierarchy. Configure the following properties in the inspector: 
	1. Type = Predefined 
	2. Database = YodaImages 
	3. ImageTarget = Yoda Stretched. 
	4. Leave width and height untouched ( 0.28 width = 11 inches for printed paper). 
	

1. Importing and adding our models to the scene. 
	1. Drag & Drop the *3rdParty/Models* folder into the *Assets* folder in Unity. [Ending up with a hierarchy of Assets/Models/BB8, Assets/Models/Yoda, Assets/Models/Cursor] 
	2. Drag the BB8 Model into the scene hierarchy.  
		1. Position it accordingly. Default is X=4, Y=1, Z=0.5
		2. Scale = .005, 0.005, 0.005
  
	3. Add a RigidBody component to the BB8.
		1. Mass = 10. 
		2. Gravity = Unchecked ; // we will turn this on later from script via voice command 

	3. Add a Box Collider onto our BB8 
		1.  Ensure Istrigger is unchecked. 
		2.  Size X=180, Y=135, Z=116.1 ; //these are so large due to the scale

	
	3. Drag the Yoda model into the scene hierarchy. 
		1. Position it accordingly. Default is X=5, Y=1, Z= 0
		2. Scale =  .25,.25, .25, 
		3. Rotation  = 0, -90,0
	4. Add a rigid body to Yoda so it collides 
		5. Mass =60,  UseGravity = unchecked 

	6. Add a Mesh collider to Yoda
		6. Convex = checked 
		7. Mesh =  Drag Mesh1 from the Yoda Model to the Mesh property of the collider. 

	4. Drag the Cursor model into the scene.   
		1. Position = 0, 0, 1.5   (1.5 meters in front of us). Scale cursor to X=0.05, Y=0.025, Z=0.5
		2.  
 
### Adding the behaviours and scripts to our scene 




#Useful references:
- [Vuforia Image Targets in Unity, Video Tutorial](https://library.vuforia.com/articles/Training/Image-Targets-in-Unity)
- 
- 