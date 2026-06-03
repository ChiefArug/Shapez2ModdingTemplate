# The Overcomplicated Linux Shapez 2 Modding Template
This is an overcomplicated template for modding the game [Shapez 2](https://store.steampowered.com/app/2162800/shapez_2__Factory/). The template only works on Linux.

It utilises MSBuild to the best of mine and its abilities, and bash for the rest of it.
The primary reason for this complicated build script is to be able to run the game as a development build and 
let me attach a debugger to it for debugging Shapez2 and my mods.

## What?
The build script included with this project will automatically:
- Download and extract DepotDownloader
- Download Shapez 2 through DepotDownloader
- Download and extract the debug executables from Unity
- Patch Shapez 2 with aforementioned debug executables
- Tell your IDE all about the local Shapez 2 installation just created
- Automatically update Shapez 2 (and Unity, if needed) whenever an update is available
- Provides Rider run configurations for running the game directly through Rider. Note this **requires steam running in the background**.
- Automatically track the player.log file when run through Rider - click on the player.log tab in the run tabs next to Console
- Automatically download any workshop dependencies you specify (comes with Shapez Shifter and its dependents by default)
  - Also note that this doesn't apply them to the game, you need to subscribe to them in the workshop for that.
- Fixes the Assimp mod not loading on linux.
- Sets up a publishing script


## Should I use it?
Do you use Linux? Do you know a bit of bash? Do you have a line of contact for ChiefArug? Do you have 7gb free disk space?

If you answered Yes to all of the above questions, then consider it. If you really want to.
Just make sure to read the [How to use it](#how-to-use-it) section!

## How to use it
Due to the limitations of Rider and MSBuild there are some caveats with this system, especially on the first setup.
- When you first open the project in Rider it will attempt to Sync. This will fail as you are not authenticated with Steam.
- Cancel the sync by killing the MSBuild process: `pkill -f MSBuild.dll`.
- Manually run build using the hammer icon. This will start showing stuff in your terminal as it downloads and runs DepotDownlaoder.
- When a QR code shows up, scan it with the Steam app on your phone. This will authenticate you with Steam.
- Make sure to also create a `.steamuser` file in the root of the project that contains your steam username, this will cause the build process to use your cached login details instead of asking for a new login each time.
- The game will then download. After the game is downloaded it will figure out the Unity version the game uses and download the installer for that
- Next the longest process begins, that of extracting the few files we need from the ~4gb Unity tarball. This will take a while.
- After this it should finish near instantly, with a lot of big red errors.
- These errors are because we only just downloaded the game dlls. A Project Sync is required to make the IDE recognise these files, so restart the IDE and it should resync.

## Publishing
To publish run the Publish Rider run, or run `dotnet msbuild ./QuickPlay.csproj -t:SteamPublish -v:diag` in a terminal.
If it doesn't do anything in about 60 seconds, kill it and see what went wrong (the output only shows up after you kill the process, dotnet is dumb).
Likely you aren't logged into SteamCMD.


## Troubleshooting
#### Stuck on Syncing:
- Run `pkill -f MSBuild.dll`, then manually run build so you get terminal output and can see what is happening.
#### The build is failing with lots of big red angry errors saying it can't find anything
- Restart Rider so that it syncs the project. If the sync isn't completing within a minute kill it (see above) and manually run build with the hammer so you get terminal output. Wait for that to finish then restart Rider.
#### Failing to build due to DepotDownloader timing out
- Edit `Timeout="300000"` in the .csproj file to a bigger number, the default is only 5 minutes.
#### I keep having to scan the QR code
- Make sure to create the `.steamuser` file with your username. Here is a command for doing that: `echo 'chiefarug' > .steamuser`. Make sure to replace `chiefarug` with your username!
#### It's asking for a password, but I cannot enter one (it says it's a read-only view)
This is a limitation of MSBuild and Rider and why this project uses qr code based authentication.
Provided you have not edited the .csproj file this will only show up when you entered the wrong username in the `.steamuser` file, or haven't run build **without** a `.steamuser` file present to set up authentication via the qr code.
#### Crashing/freezing/black screen due to some random error in a native executable
The direct run configuration provided here run it slightly differently to how Steam runs it.
Specifically this doesn't launch it through the Steam Runtime, which provides copies of common native executables so games can run in a predictable environment
If you have a wacky setup then this could be causing issues.
- In theory, you can solve it by running Rider through Steam by adding it as a Non-Steam game and forcing it to run with the Steam Runtime
- Add the Rider executable (located in the `bin` folder of your Rider install location) as a Non-Steam game
- Then right click it and go Properties -> Compatibility -> Force the use of a specific Steam Play compatibility tool -> Steam Linux Runtime 3.0 (sniper)
#### System.Exception: SteamApi_Init returned false. Steam isn't running, couldn't find Steam, App ID is ureleased, Don't own App ID.
This means that Steam isn't running in the background in the same user as Rider and the game, so open Steam.
Otherwise, it's possibly a random error in a native executable, see above.
### Unhandled exception. System.ArgumentException: LogOn requires a username and password or access token to be set in 'details'.
You need to log in to steam again, rename `.steamuser` to something else, run build and scan the qr, then rename it back for next time.


#### Something else
- Contact ChiefArug for assistance, or figure it out yourself then let them know how you fixed it.


## Credits
Thanks to khyperia who made the orignal template (for Haste) this is based on, though I have modified this very heavily from the source material:
https://github.com/Haste-Team/HastePlugins/tree/main/HelloWorld

Thank you to SteamRE who make DepotDownloader that this project uses.

Also check out my Haste template that this is based on: https://github.com/ChiefArug/HasteModdingTemplate

## Contributing
Contributions would be welcome, especially to clean up my messy MSBuild script.
In particular getting this to work on Windows (or Mac, if you are that sort of person) would be awesome,
although would require rewriting a lot of the `.csproj` file.

Additional Troubleshooting steps are always welcome too.
    
