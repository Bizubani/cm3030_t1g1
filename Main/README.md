# GameDevGame
Updates
Added Real Time Baking of Levels(Maybe I like saying that too much, RealTime)
Added Post Processing Effects
Added TEST Maps
Added Some Smaller Assets...More on the way

Roberto's Prototype - Project URZA Notes
Instructions:
- Get a copy of the project From the branch, Download & Unzip
- Open it via Unity Hub (It will take some time to load the first time)
- You may notice that after some time a plugin may pop up, don't close it just leave it be, the project will load with no problem.
- The default project after the project is loaded should be removed AFTER you add a scene from the "scene/s" file (There should be a main menu and The actual game)

Stuff To check if Materials are not Loading:
- Go to package manager in Windows and Install URP, TextMeshPro, and Shader Graph.

Stuff that needs to be added to the Project:
- There may be some blender files missing they were left out due to their size, however, I Imported FBX versions of them.
- Drag and Drop the weapon bullet Logic onto the game objects Weapons 1 and 2 and Adjust Some values.

Stuff to ignore:
- You'll Notice an error about Enemy Controller, It's a false flag for now as nothing is assigned to it.
- There may be errors regarding Nav mesh, Since it's baking in real time it's important to note that the mesh doesn't exist before the project is run, so the script for generating the NavMeshes checks every frame if there are meshes to be baked. (again a false flag)

Stuff You might want to change for development
- You may notice that in the game it is excessively bright, this is due to the camera post-processing effects. (This is useful when there are environments, and looks great)
* To turn them off, Find the Cinemachine Camera (not the main camera game object, the one below it) in the player game object, scroll down in the object inspector and you'll find the post-processing settings and untick the box.
- For lighting the game is using a directional light located in the world game object, adjust it for your own needs...

//////////////////////
<IMPORTANT>
*THERE ARE A LOT OF CHANGES IN THIS PROJECT SO I WOULD ADVISE YOU TO SIT DOWN AND LOOK AT ALL THE GAME OBJECTS IN THE SCENE TO UNDERSTAND, ASWELL AS THE SCRIPTS AND SETTINGS THEY MAY USE.

*If you would like to effectively add changes or improvements, to this project but you are afraid that it might be discarded in future versions that may be committed, then do this:
The Changes that occurred in a scene should be copied and labelled in the scene folder...What do I mean
- Where you have made final changes copy that scene and save it as an original scene.

So when a new version of the project is uploaded by someone all you have to do is upload the scene folder to the latest project on git (instead of making those changes again and again and again)

eg. 
- You made changes to the main menu scene, boom copy that boy and make an original folder (with your name or the new variants name)
- Someone uploaded a new version of the project, boom, all you have to do is upload your file to git and you're done.
- Then re-download/clone the project and continue developing.