# hyperx-mute-on-taskbar

Show the microphone mute state of a HyperX Cloud Flight headset (and possibly others) on the taskbar.

## How to build

Use the following command to clone the repo with all its submodules:  
`git clone --recurse-submodules https://github.com/sanraith/hyperx-mute-on-taskbar`

Build `SharpLibHid\Project\SharpLibHid.sln` in `Release` mode.

Build `HyperXMuteTaskbar.sln` in `Release` mode.

## SharpLibHid usage

This project uses a fork of the SharpLidHid library. The fork contains minor fixes, so that the library can handle 'unknown' devices.  
Original source is available here: <https://github.com/Slion/SharpLibHid>  
Fork is available here: <https://github.com/sanraith/SharpLibHid>
