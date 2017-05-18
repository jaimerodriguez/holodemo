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

## Preparation/Setup 

1. Setup HoloLens'  [Mixed Reality Capture](https://developer.microsoft.com/en-us/windows/mixed-reality/mixed_reality_capture) for Live Preview.  I recommend you do it from the [Windows device portal](https://developer.microsoft.com/en-us/windows/mixed-reality/using_the_windows_device_portal)

2. Position the BB8, and the Yoda model in the unity scene. Here are a few tips: 
	1. When HoloLens app comes up, you should be looking forward (usually towards the audience) at your height. 
	2. You want the BB8 to be some where that it can walk laterally towards your podium (or desk) and collided with something physical. Here is default position for BB8: 
		1. X=4 ( 4 meters to your right), Y = 1 (floating in space so we can demo gravity), and Z = 0 or Z < 1 (no depth from where you are standing so audience can see holograms as they see you).  
The 4 meters was selected so you can walk towards BB8 and walk around it at ~5 meters, which is further than any roomscale VR that is tethered. This is a unique HoloLens feature you should highlight. 
 
	3. You want the Yoda some where near the BB8 so it is clear when it appears in the scene. 
X = 5  (5 meters to your right); floating in space (1<Y<2) so you can show gravity but not hit ceiling; Z = 0 (deep can still be 0, or a bit behind BB8, so -1<Z<0).

	4. If you are using image recognition w/ Vuforia, print the "YodaStretched.jpg" in the 3rdParty/Vuforia_Database folder). Print this w/ color in 8.5"x11" page. 

	5. If you are not using Vuforia, remove the // that comments out the #define FAKESCAN in line 0 in 
HoloLensDemo/Scripts/SceneManager.cs, and use the "Toggle Laser" command -which was designed for when scanning 3D objects-. With FAKESCAN defined, within a few seconds of you toggling the laser, it will act as if some object had been scanned. 



## Commands within the app & outline for a demo

1. Launch the app. 
2. Explain the cursor. It is your gaze. HoloLens sensors are doing all the tracking, and cursor uses Unity's RayCast to detect collisions. Cursor is blue when it does not collide with anything and green when it collides with something. When it collides with something, it positions itself at same depth than object it collided with, so you might see cursor increase/decrease in size. When it is not colliding with anything, we position it at 1.5m in direction you are gazing. 
Cursor code is in SimplestCursor.cs script. Do explain that HoloToolkit has better cursors with stabilization. This simple one is for illustrative purposes only.   

2. Look around the room, and have your cursor collide with the BB8 model. 
3. Show the BB8 model. This is a standard, free fbx.  Nothing special, a hologram is just any 3D object in Unity. 

4. Show that there is no Yoda next to the BB8. We will summon the Yoda w/ Vuforia.  
5. Scanning an object w/ Vuforia to summon the Yoda. 
	1. If you are using image recognition, scan the image you printed earlier by just looking at it. 
	2. If you don't have a prop (image or object), use the *"Laser"* voice command with FAKESCAN defined (see setup).  Note: to hide the laser when done, just say "Laser" again. 
3. See Yoda appear. Walk to it, look around it. Just another free FBX. Emphasize the untethered aspect of HoloLens.  
4. Use the *"Toggle Scan"* voice command to start scanning the floor & walls. 
5. Use the *"Toggle Renderer"* voice command to show how HoloLens is scanning and the meshes it is finding.
You can call Toggle Renderer whenever to toggle these meshes. I like to show them then hiding during walk part of the demo (next steps).  
6. Once you know enough of the floor has been scannned (so your holograms don't fall through) use the *"Gravity"* command to turn on gravity on your holograms. See them fall and collide with the floor. If they fall and are not standing up, tap them, this marks them as selected, and resets their transform.     
7. Tap on the BB8 to select it, showing the hand gesture of "Tap". Script is in TapListener.cs 
8. With BB8 selected, use the *"Walk"* command and see it animate around the scanned floor. Emphasize this is all free due to Spatial mapping. 
9. Repeat the "walk" command until BB8 hits something physical and collides with it. 
10. When BB8 falls over, "Tap it" to have it stand-up.  
11. Give the "Toggle Scan" command, which will turn-off Unity's spatial mapping, making the floor mesh disappear. With this command, our holograms will disappear into abyss.  That is spatial mapping in action. 



#Building the demo to showcase developers features 

The following steps outline every step in building the demo from scratch. You can have a few of the steps "pre-configured" to not bore your audience too much.  

You can see a pre-configured project in the "short" branch of this repo. That setup takes care of:  
1. Setting up Vuforia 
2. Importing the models into the project, but not into the scene.  
3. Importing the scripts. You should still explain them, but no need to drag them in. 


The following steps are useful for developers to see, so they understand how easy it is to build this scene.  
2. Adding the holograms to the scene.  Add one of the holograms. Show them it is a standard 3d model.  
3. Adding SpatialMapping. It is a simple Create Empty Game object. Add the two components:  SpatialMappingCollider, SpatialMapping Renderer.
2. Adding voice commands. You can just add the SceneManager script and walk audience through it. 
3. Cursor. Just add the SimplestCursor script and walk them through it. 
4. Gestures. Just add TapListener and walk them through it. 




## Step-by Step building script

### Creating the project 
1. Create a new project in Unity. Make sure you are using Unity 5.6 or later.  
	1. Name does not matter. Make it a 3D project. You do not need Unity analytics so feel free to 'disable' that option.  

###Setting up Vuforia 
2. Import the Vuforia SDK into Unity.  Here are the detailed steps: 
	1. Downloading the Unity SDK from https://developer.vuforia.com/downloads/sdk  
	2. Import the Vuforia SDK 
		1. In Unity, go to *Assets-> Import Package-> Custom Package*
		1. Select the **Vuforia<version>.UnityPackage** you downloaded, then click **Open**
		2. On the Import dialog that comes up, you can ensure everything is selected and just click **Import** to import everything; or, you could also be selective and exclude iOS/Android files by unchecking these within the plugins folder. 
		3. If you get a warning asking you to allow Unity to "Upgrade your scripts so they don't reference obsolete APIs" then **click "I made a backup, go Ahead"**. 
1. Import your Vuforia Assets database
	Note: If you want to use the one included with this demo, it is in the 3rdParty folder at the root of this repo. 
	1. Go to *Assets->Import Package->Custom Package*. Select **YodaImages.UnityPackage** to import the images database. 
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

1. Drag the Vuforia ImageTarget prefab into the scene hierarchy. Call it "Vuforia_Image_Yoda". Configure the following properties in the inspector: 
	1. Type = Predefined 
	2. Database = YodaImages 
	3. ImageTarget = Yoda Stretched 
	4. Leave width and height untouched ( 0.28 width = 11 inches for printed paper). 
	

1. Importing our models into the project. 
	1. Drag & Drop the *3rdParty/Models* folder into the *Assets* folder in Unity. [Ending up with a hierarchy of Assets/Models/BB8, Assets/Models/Yoda, Assets/Models/Cursor]

2. Adding our models into the scene.  
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
		1. Position it accordingly (see setup above). Recommended is X=5, Y=1, Z= 0
		2. Scale =  .25,.25, .25, 
		3. Rotation= 0, -90,0

	4. Add a rigid body to Yoda so it collides with floor. 
		5. Mass =60,  UseGravity = unchecked 

	6. Add a Mesh collider to Yoda
		6. Convex = checked 
		7. Mesh =  Drag Mesh1 from the Yoda Model (in  to the Mesh property of the collider). To find Mesh1,in Project pane go to Assets/HololensDemo/Models/Yoda/ and expand the Yoda model. You should see a Mesh1 mesh and a Jedi_Masters_ mesh.   

	4. Drag the Cursor Prefab (in HololensDemo/Prefabs) into the scene. The default property values should work.  Confirm they look like this:    
		1. Transform.Position = 0, 0, 1.5   (1.5 meters in front of us). 
		2. Transform.Scale X=0.05, Y=0.025, Z=0.05
		2.  SimplestCursor component is already attached and has a radius of 15 (this is how far we will RayCast, 15 meters in gaze direction), and an On/Off material (Green & Blue respectively). 
		
 
### Adding the behaviors and scripts to our scene 
This part outlines the scripts that do most of the work. it is little code, but still worth explaining to developers: 

#### Spatial Mapping 
1. Create Empty GameObject and add it to scene. Call it *SpatialMappingManager*. Name is not critical, but I  will refer to it by this name in steps below, so i recommend you use same name.
	1. Add a **SpatialMappingCollider** component to this object.  SpatialMappingCollider is a Unity component; it will allow us to collide with objects that HoloLens 'scans' using Spatial Mapping. Leave all the defaults for the object. Ensure "Enable collisions" is checked.
	2. Add a **SpatialMappingRenderer** component to our SpatialMappingManager.This again is a Unity component. This will render meshes as the room is getting scanned, giving us a visual indicator of the spatial mapping coverage. 
	
That is all we need for spatial mapping. We do not need any code at all. Magic!!
 
### Scene Manager (that orchestrates our scene) 
2. Create Empty GameObject in Scene Hierarchy. Call it *SceneManager*. 
	1.    Add the **SceneManager **script component to our SceneManager GameObject. (Sorry for dupe name).   
SceneManager has most of our logic. It is centralized in one place to make it easier to demo & tell the story.  In a few cases, this meant coupling other elements in the scene to our manager. Don't take what it does as a 'best practice'; it does make our scene easier to maintain & explain. 


Configure the SceneManager script component like this: 
	2.    Interactive Elements, Size = 2.  
Interactive Elements list has the objects we want to interact with. For example when you say "Gravity" we enumerate through this collection and turn Gravity on for these Game Objects. 
	3.    Element 0 == BB8.   Drag BB8 GameObject from scene hierarchy here.  
	4.    Element 1 == Yoda.  Drag Yoda GameObject from scene hierarchy. 

	5.    Cursor == Cursor. Drag Cursor GameObject from scene hierarchy to this property. 
We use this reference to hide/show cursor when laser is on.   

1. [Optional] Add an Audio Source component to our SceneManager GameObject.  
We will use this to play a sound for the demoer (you!) when a voice command is interpreted and handled. 
It prevents you from having to repeat commands unnecessarily (or having the repeats conflict w/ each other). 
Set the following properties in our AudioSource:    
	1. Use the "ding" in HoloLensDemo/Sounds folder as the clip.  
	2. Uncheck the "play on Awake" option.  
	3. Configure your volumea accordingly. 

4. [Optional]. Add a reticle for our laser as a child of SceneManager.  We are keeping it simple and just using a LineRenderer ( with two points at 0,0,0 and 0,0,1) as a reticle.  The HoloLensDemo/Prefabs folder already has prefab for this, so i recommend you use that. It is not polished but it works.  
	1. Once you create this GameObject set the **ObjectTrackingReticle** property in our SceneManager script to this GameObject.

  
### Configuring our Vuforia Image recognition. 

2. Uncheck (or remove) the *DefaultTrackableEventHandler* script from our *Vuforia_Image_Yoda*. 
We won't be using this because by default what Vuforia does when the image is recognized it enables the children of the GameObject.  In our case, we do want to enable our Yoda, but it is not a child of our Vuforia image, so we won't use the default behavior.  We will use the one in this next step. 

3. Add a  *VuforiaTrackingBehavior *component to the *Vuforia_Image_Yoda* we added earlier to our scene. 
This script does very little: When an object (our image), this script calls its OnTrackingFoundMethod() which calls **SceneManager.Instance.VuforiaObjectDetected() **which lets our SceneManager know it needs to do the work (to show our Yoda, hide laser, etc.) 


### Voice commands 
6.  Voice Commands are implemented in SceneManager script. Explain the StartVoiceRecognizer method and the KeywordRecognizer_OnPhraseRecognized method. This is all it takes to do voice recognition in HoloLens. 


That is it! Now just run the demo & have fun. 



#References:
- [Vuforia Image Targets in Unity, Video Tutorial](https://library.vuforia.com/articles/Training/Image-Targets-in-Unity)
- 
- 