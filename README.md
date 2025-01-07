# HotCornersWin
Add hot corners function to Windows 10/11, similar to such of macOS, Gnome and KDE.

![Settings window and the notification area icon](https://github.com/flexits/HotCornersWin/assets/86118729/b9f6b1ee-2f83-4766-a061-7bd705913496)

This simple application resides in the notification area and allows user to choose from a list of various actions to be triggered when the mouse cursor hits a corner of the screen. It was developed for Windows 10, but works in Windows 11 too, except some visual style inconsistencies. It's expected to run in previous OS'es down to Windows 7 (only x64) as well, however these old versions aren't supported. 

### Features

You can choose an action for different corners independently or leave some without an action at all. There's a number of pre-defined actions as well as the Action Editor for creation of **custom actions** – you can assign anything suitable for the Windows Run dialog (Win+R): that is, an executable file, a [URI](https://learn.microsoft.com/en-us/windows/uwp/launch-resume/launch-settings-app?WT.mc_id=WD-MVP-5000693#ms-settings-uri-scheme-reference), or a [CLSID-pointed folder](https://www.autohotkey.com/docs/v1/misc/CLSID-List.htm).

An adjustable **delay** before triggering an action is associated with each hot corner. It's useful if the action has to be executed not immediately on the cursor entering the sensitive area, but when the cursor stays in the sensitive area for a certain time. The sensitive area **size** is customizable too.

**Multi-monitor** environments are fully supported. Hot corners may be placed on the primary display only, on the virtual display (it contains all the monitors combined in one), or repeated on each monitor.

Minimal interference: the application can **autodetect a full-screen application** (a game, for example) launch and exit and automatically turn itself off and on respectively (this feature may not work correctly with certain games and applications and is disabled by default). 

It **ignores dragging** (basically, any mouse gestures performed with a button held down) and won't trigger actions in this case.

Manual turn on and off is as simple as a single click on the app's icon in the notification area (tray icon). A double click brings the Settings window up.

**Dark and light themes** are supported with manual or system-wide switching. However, this feature is [experimental](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/whats-new/net90?view=netdesktop-9.0#dark-mode) until .NET 10, and there are some visual inconsistencies when the dark theme is on.

**Small size** – around 1.5 Mb installation footprint!

### Installation and usage
Download the latest version `*.msi` installer package from the [Releases](https://github.com/flexits/HotCornersWin/releases) page and run the installer. The app will be added to the current user's StartUp folder in Start->Programs. 

There's a **portable version** available as well. Download `*.zip`, unpack to any folder you like and run _HotCornersWin.exe_. The settings will be stored in the `data` subfolder (will be created on first run).

In both cases you're required to have the [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed on the PC.

The application does not require any specific settings or fine-tuning. Set the desired actions and basically you're good to go! 

The operation is based on the cyclic polling of the mouse cursor position. It's performed with a specified time interval. The less is the interval value, the faster movements will be captured and more CPU consumed; otherwise, with a big interval some rapid mouse movements will be missed out. The default setting is 75ms and usually it's good enough and needs no adjustment. 

### Available actions:
* Start menu
* Task View
* Switch to the Virtual Desktop on the left
* Switch to the Virtual Desktop on the right
* Show/Hide Desktop
* a new Exporer window
* Lock PC
* Project Settings
* Action Center
* Quick Link menu
* Snip & Sketch
* Windows Ink Workspace
* Widgets Board (Windows 11)
* *add yours!*

### TODO:
See [Issues](https://github.com/flexits/HotCornersWin/issues) and feel free to add yours.

### Contributions

Any contributions are welcomed, not limited to the code but including feature requests, opinions on the app's operation, UI/UX etc. Feel free to open an issue, leave your note in the existing ones, or join the [discussion](https://github.com/flexits/HotCornersWin/discussions/16).

### Attributions:

Icons by <a target="_blank" href="https://github.com/Lisa24Jackson">Lisa Jackson</a>
