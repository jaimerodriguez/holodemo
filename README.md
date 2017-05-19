# HoloLens, basic Unity demonstrator 

This is a brief demonstrator/script for showing HoloLens to a developer audience. 

It builds a scene from scratch (within a few minutes) and demonstrates:
- How to quickly create a gaze cursor (and all head tracking that get for free w/ HoloLens camera and Unity).  
- Adding & Handling Voice commands in HoloLens.   
- Adding Spatial Mapping to a scene. Demonstrating how you get occlusion and collision detection for free with Unity.   
- Object and/or Image Recognition using Vuforia.

You can see a (very rough) recording of this demo [here](https://dl.dropboxusercontent.com/u/6892570/BasicHoloDemo.mp4). 

# Showing the demo 


## Preparation/Setup for the demo. 

1. Get the unity project. 
If you want a demo that is pre-built, just pull the master branch from this repo. 
If you want to build the interesting parts of the demo on stage, pull the 'startup' branch from this repo.  

1. Setup HoloLens'  [Mixed Reality Capture](https://developer.microsoft.com/en-us/windows/mixed-reality/mixed_reality_capture) for Live Preview.  I recommend you do it from the [Windows device portal](https://developer.microsoft.com/en-us/windows/mixed-reality/using_the_windows_device_portal). 

2. Position the BB8, and the Yoda model in the unity scene. Here are a few tips for placement:  
	1. When HoloLens app comes up, it will establish your origin (0,0,0), so you should be wearing HoloLens and looking forward (usually towards the audience). 
	2. You want the BB8 to be some where that it can walk laterally towards your podium (or desk) and collided with something physical. Here is default position for BB8: 
 **Transform.Position:X=4** (4 meters to your right),**Y=1** (floating in space so we can demo gravity later), and **Z=0 or Z<1** (no depth from where you are at, or a little in front).  
The 4 meters was selected so you can walk towards BB8 and walk around it at < 5 meters, which is further than any roomscale VR that is tethered. This is a unique HoloLens feature you should highlight. 
	3. You want the Yoda some where near the BB8 so it is clear when it appears in the scene (**X=5** is 5 meters to your right). You also want it floating in space (so **1<Y<2** ) so you can show gravity.  Z (deep can still be 0, or a bit behind BB8, so -1<Z<0).
	4. If you are using image recognition w/ Vuforia, print the "YodaStretched.jpg" in the 3rdParty/Vuforia_Database folder). Print this w/ color in 8.5"x11" page. 

	5. If you are not using Vuforia, uncomment out the #define FAKESCAN in line 0 in HoloLensDemo/Scripts/SceneManager.cs, and use the *"Toggle Laser"* command -which was designed for when recognizing objects, as shown in the demo video-.
	
	   With FAKESCAN defined, within a few seconds of you toggling the laser, it will act as if some object had been recognized and the rest of the workflow will trigger. This removes the need for a print out or a 3D object. 



## Commands within the app & outline (aka The DEMO SCRIPT) 

1. Launch the app. 
2. Explain the cursor. It is your gaze. HoloLens sensors are doing all the tracking, and cursor is just positioned in the forward direction. The cursor uses Unity's RayCast to detect collisions. 

   Cursor is blue when it does not collide with anything and green when it collides with something. When it collides with something, it positions itself at same depth than object it collided with, so you might see cursor increase/decrease in size. When it is not colliding with anything, we position it at 1.5m in direction you are gazing. 
Cursor code is in SimplestCursor.cs script. 

   Do explain that HoloToolkit has better cursors with stabilization. This simple one is for illustrative purposes only.   

2. Look around the room, and have your cursor collide with the BB8 model. 
3. Show the BB8 model. Explain is a standard, free3D model (fbx format).  Nothing special, a hologram is just any 3D object in Unity. 
4. Show that there is no Yoda next to the BB8. We will summon the Yoda with Vuforia.  
5. Recognizing an object w/ Vuforia to summon the Yoda. 
	1. If you are using image recognition, just look (through HoloLens) at the image you printed earlier. Within a couple seconds it should be recognized.  
	2. If you don't have a prop (image or object), use the *"Laser"* voice command with FAKESCAN defined (see setup).  
    Note: to hide the laser, just say *"Laser"* again. 
3. See Yoda appear. Walk to it,and around it (to show its three-dimensionality). This is just another free FBX. Emphasize the untethered aspect of HoloLens.  You are likely 6m away from where you started, walking around the yoda.
4. Use the *"Toggle Scan"* voice command to start scanning the floor & walls.  
 
   [You should hear the 'ding' accepting the command but nothing changes visually]. 
5. Use the *"Toggle Renderer"* voice command to show how HoloLens is mapping the walls and floors. The renderer command will show the  meshes the spatial mapping component is finding.
   You can call Toggle Renderer whenever to toggle (show/hide)these meshes. I like to show them then hiding during next part of the demo.  
6. Once you know that enough of the floor has been mapped (so your holograms don't fall through) use the *"Gravity"* command to turn on gravity on your holograms. See them fall and collide with the floor. If they fall and are not standing up, tap them, this marks them as selected, and resets their transform (making them stand-up).     
7. Tap on the BB8 to select it, showing the hand gesture of "Tap". Script to handle Tap is in TapListener.cs. 
8. With BB8 selected, use the *"Walk"* voice command and see it animate around the floor. Explain how this is all free (we don't have to write code) due to Spatial mapping and Unity collision. 
9. Repeat the *"Walk"* command until BB8 hits something physical and collides with it. You just had a virtual hologram collide with a physical object!! 
10. If BB8 falls over, when it collides, "Tap it" to have it stand-up.  
11. [Optional]. If there is walls around you, have the BB8 walk past a wall. Notice how it literally disappears from the scene. Unity occludes it, the way a physical wall would in real life. 
11. Give the "Toggle Scan" command, which will turn-off Unity's spatial mapping, making the floor mesh disappear. With this command, our holograms will disappear into abyss.  That is spatial mapping in action.

That is it. In just a few  minutes you demonstrated the convergence of digital and physical objects in mixed reality, voice and gestures in  HoloLens, and the benefits of an untethered MR device. 


# Building the demo to showcase features to developers 

To avoid boring your audience with tiny details, use a pre-configured project in the "startup" branch of this repo. 

That setup takes care of:  
1. Setting up Vuforia 
2. Importing the models, audio, materials & prefabs assets into the project, but not into the scene.  
3. Importing the scripts. You should still explain them, but no need to drag them in or type them.  

The steps that are useful for developers to understand (and see) how easy it is to build this scene are:   
1. Adding the objects to the scene.  Add at least one of the holograms. Show them it is a standard 3d model. Nothing special. 
2. Adding SpatialMapping. It is a simple Create Empty Game object. Add the two components:  SpatialMappingCollider, SpatialMapping Renderer.
3. Adding voice commands. You can just add the SceneManager script and walk audience through it. 
4. Cursor. Just add the SimplestCursor script and walk them through it. 
5. Gestures. Just add TapListener and walk them through it. 

Each of these steps is detailed thoroughly below.  Some basic familiarity with Unity is required. 


## Step-by Step script for creating the demo

### Creating the project.  Skip this step if using startup branch.  
1. Create a new project in Unity. Make sure you are using Unity 5.6 or later.  
	1. Name does not matter. Make it a 3D project. You do not need Unity analytics so feel free to 'disable' that option.  

 
### Setting up Vuforia. Skip this step if using startup branch.  
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
	2. Click **Import** in the Import Dialog to import all the assets.  This should import all Vuforia assets to the *Streaming Assets* folder and the *Editor/QCAR* folder. 


1. Configure your Vuforia Key.
	1. In the Vuforia menu, go to configuration. 
	2. In the App License Key, enter your license key obtained from Vuforia's license manager (https://developer.vuforia.com/targetmanager/licenseManager/licenseListing).  

The license is 'free' for testing/demo purposes, but it is restricted to 1000 recognitions per month, and a limited number of targets & VuMarks.  


1. Configure Vuforia default settings in property Inspector: 
	1. Max simultaneous tracked Images = **2** 
	2. Max simultaneous tracked Objects = **2**
	3. Digital EyeWear - Type = **Optical See-Through** 
	4. Digital Eyewear See Through Config = **HoloLens** 
	5. Load YodaImages Database = **Checked**
	6. Activate Database = **Checked** 
	7. Enable Video Background = **Unchecked** 
	8. Webcam -> Disable Vuforia Play Mode  = **Checked** 
	9. Overflow Geometry = **Stencil** (already the default) 
	10. Matte Shader = **Clipping mas**k ( already the default)
 

### Creating our HoloLensDemo folder
When I originally built the demo, i created everything inside a **HoloLensDemo** folder. This helps me keep my objects separate from all the Vuforia assets. This step is optional, but most steps below that bring assets into the project will recommend you do it into that folder, so either create this folder for consistency, or please reinterpret instructions below and copy then to *Assets *folder wherever it says *Assets/HoloLensDemo*. 
1.- Create HoloLensDemo in Unity project's *Assets* folder. 

### Importing our models and assets into the project (not needed when using Startup Branch). 
1. Drag & Drop the *3rdParty/Models* folder into the *Assets/HoloLensDemo* folder in Unity. [Ending up with a hierarchy of Assets/HololensDemo/Models/BB8, Assets/HololensDemo/Models/Yoda, etc.]
2. Copy the *3rdParty/Sounds* folder onto the *Assets/HololensDemo* folder in our Unity project. 
3. Create a *Materials* folder in our HoloLensDemo folder. 
4. Add three  Materials (created with Standard shader). Call them Red, Green, Blue. Set the Albedo accordingly so the name of each material matches its albedo. 
4. Copy the *3rdParty/Scripts* folder into our *Assets/HoloLensDemo* folder in Unity. This will bring all our scripts, which we will use and explain in the latter sections 
	
### Creating our startup scene
1. Save your Scene into a new folder -called **Scenes**- in Unity. Call your scene *"main"* 
2. Configure the Main Camera as a HoloLens camera: 
	1.Transform.Position = **0,0,0**
	2.Clear Flags = **Solid Color**
	3. Background = **Black = RGBA (0,0,0,255)**
4. Add an **ARCamera** to our scene.  You can drag it from Vuforia/Prefabs/ArCamera.prefab
   Configure the following properties in the ARCamera GameObject.  
	1. Transform.Position = **(0,0,0)**
	2. World Center Mode = **CAMERA**
	3. Central Anchor Point = **MainCamera**  [Drag our scene's MainCamera here]
	4. AudioListener = **Unchecked**
 
3. Save scene again. 

### Preparing our scene (FINALLY!! BEGIN HERE WHEN USING STARTUP BRANCH). 

#### Adding the Vuforia Image Target 
1. Drag the Vuforia ImageTarget prefab into the scene hierarchy. Call the GameObject added *"Vuforia_Image_Yoda"*. 
   Configure the following properties for this object's ImageTargetBehaviour component 
	1. Type = **Predefined** 
	2. Database = **YodaImages** 
	3. ImageTarget = **YodaStretched** 
	4. Leave width and height untouched (0.28 width = 11 inches for printed paper). 
	

#### Adding our models into the scene. 
1. Adding the BB8 
	1. Drag the BB8 Model into the scene hierarchy. 
	2. Position for your stage. Recommendation is: 
	   Transform.Position = **(4,1,0.5)**, if your room allows, see setup above for explanation. 
       Scale = **.005, 0.005, 0.005**
	3. Add a RigidBody component to the BB8.
		1. Mass = **10** 
		2. Drag = **0**
		2. Gravity = **Unchecked**. We will turn this on later from script via voice command. 
	1. Add a **Box Collider** to the BB8. 
		1.  Ensure Istrigger is **Unchecked**. 
		1.  Size **X=180, Y=135, Z=116.1**; //these are so large due to the scale of our free model. 
	1.  Add a **Demo Interactable**Component to the BB8. This script is what helps us with Tapping the component, and having it walk around the scene. 
		1.  Tap Force Strength **=10** (the default) 
	3. Drag the Yoda model into the scene hierarchy. 
 		1. Position it accordingly (see setup above). Recommended is **X=5, Y=1, Z= 0**
		2. Scale =  **.25,.25, .25** 
		3. Rotation= **0, -90,0**

	4. Add a **RigidBody **component to Yoda GameObject, so it collides with floor. 
 		5. Mass = **60**,  
 		6. Use Gravity = **unchecked** 

	6. Add a **MeshCollider ** component to the Yoda game object
		6. Convex = **checked **
		7. Mesh =  **Mesh1**.  To find Mesh1: In the Project pane go to *Assets/HololensDemo/Models/Yoda/* and expand the Yoda model. You should see a Mesh1 mesh and a Jedi_Masters_ mesh.Drag Mesh1 to the collider. 

	4. Drag the **Cursor** prefab (in *HololensDemo/Prefabs*) into the scene. 
       The default property values should work.  Confirm they look like this:    
		1. Transform.Position = **0, 0, 1.5**   (1.5 meters in front of us). 
		2. Transform.Scale **X=0.05, Y=0.025, Z=0.05**
		2.  **SimplestCursor **script component is already attached and has a Radius**=15** (this is how far we will RayCast, 15 meters in gaze direction), and an **On/Off material (Green & Blue respectively)**. 
 
### Adding the behaviors and scripts to our scene 
This part outlines the scripts that do most of the work. it is little code, but still worth explaining to developers: 

#### Spatial Mapping 
1. Create Empty GameObject and add it to scene. Call it *SpatialMappingManager*. 
    Name is not critical, but I  will refer to it by this name in steps below, so i recommend you use same name.
	1. Add a **SpatialMappingCollider** component to this object.  
       SpatialMappingCollider is a Unity component; it will allow us to collide with objects that HoloLens 'scans' using Spatial Mapping. 
       Leave all the defaults for the object. Ensure "Enable collisions" is checked.
	2. Add a **SpatialMappingRenderer** component to our SpatialMappingManager. 
     This again is a Unity component. This will render meshes as the room is getting scanned, giving us a visual indicator of the spatial mapping coverage. 
	
That is all we need for spatial mapping. We do not need any code at all. Magic!!
 
### Add a scene manager (that orchestrates our whole scene) 
2. Create Empty GameObject in Scene Hierarchy. Call it *SceneManager*. 
	1. Add the **SceneManager **script component to our SceneManager GameObject. (Sorry for dupe name).   
SceneManager has most of our logic. It is centralized in one place to make it easier to demo & tell the story.  In a few cases, this meant coupling other elements in the scene to our manager, but it makes demoing easier.  
3. Configure the **SceneManager** script component like this: 
	1. Interactive Elements, Size = **2**. 
    Interactive Elements list has the objects we want to interact with. For example when you say "Gravity" we enumerate through this collection and turn Gravity on for these Game Objects. 
	2. Element 0 == **BB8**.   Drag BB8 GameObject from scene hierarchy here. 
	3. Element 1 == **Yoda**.  Drag Yoda GameObject from scene hierarchy. 
	5. Cursor == **Cursor**. Drag Cursor GameObject from scene hierarchy to this property. We use this reference to hide/show cursor when laser is on.   

1. Add an **AudioSource **component to our SceneManager GameObject.  
We will use this to play a sound for the demoer (you!) when a voice command is interpreted and handled. 
It prevents you from having to repeat commands unnecessarily (or having the repeats conflict w/ each other). 
Set the following properties in our AudioSource:    
	1. Use the "**ding**" in HoloLensDemo/Sounds folder as the clip.  
	2. **Uncheck** the "play on Awake" option.  
	3. Configure your volume levels as desired. 

4. [Optional]. Add a reticle for our laser as a child of SceneManager.  We are keeping it simple and just using a LineRenderer (with two points at 0,0,0 and 0,0,1) as a reticle.  
    The HoloLensDemo/Prefabs folder already has prefab for this, so i recommend you use that. It is not polished but it works.  
	1. Once you create this GameObject set the **ObjectTrackingReticle** property in our SceneManager script to this GameObject.

### Configuring our Vuforia Image recognition. 
We added our *Vuforia_Image_Yoda* into the scene earlier, but here we get to configure it. 
1. Uncheck (or remove) the **DefaultTrackableEventHandler** script from our *Vuforia_Image_Yoda* GameObject.
	We won't be using this because by default what Vuforia does when the image is recognized it enables the children of the GameObject.  In our case, we do want to enable our Yoda, but it is not a child of our Vuforia image, so we won't use the default behavior.  We will use the one in this next step. 

3. Add a  **VuforiaTrackingBehavior** component to the *Vuforia_Image_Yoda* GameObject. 
   This script does two things: 
	1. When an object (our image) is recognized by Vuforia, this script calls its **OnTrackingFound() ** method, which in-turn calls **SceneManager.Instance.VuforiaObjectDetected() **which lets our SceneManager know it needs to do the work (to show our Yoda, hide laser, etc.) 

	2. The script takes some visual cues that are mostly aimed at demoing it in "laser mode". Such that when an object is identified we change the Highlight color of that object so user knows we found it].   
To configure these cues, set the following properties in the VuforiaTargetBehavior component: 
		1. Target Highlight = **Green** [the green matrial in HololensDemo\Resources\Materials folder].  
		2. Target Normal = **Red** [Red material, in HololensDemo\Resources\Materials folder]. 
		3. TargetRenderer = **Reticle**  [The GameObject we added as a child to SceneManager] 
		4. Target = **Cursor** [our Cursor GameObject from the scene].  

### Voice commands 
6.  Voice Commands are implemented in SceneManager script. Explain the **StartVoiceRecognizer** method and the **KeywordRecognizer_OnPhraseRecognized** method. This is all it takes to do voice recognition in HoloLens. 


That is it! Now just run the demo (with the script outlined at the earlier) & have fun. 


  
# Useful references:
- [Vuforia Image Targets in Unity, Video Tutorial](https://library.vuforia.com/articles/Training/Image-Targets-in-Unity)
- 
- 
