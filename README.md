# operation-modulation
Operation Modulation is an fast-paced arcade game where players decode emoji images sent via modulation encoded radio signals.

## Modulation
Modulation is used to transmit information over radio waves. Information like text and images is first converted to binary numbers (0s and 1s). Binary numbers are then represented in the radio waves using different graph shapes. The shape of the graph determines if a 0 or a 1 is being sent.

### Modulation Types
- Amplitude shift keying is a type of modulation where 0's are represented with a low amplitude signal and 1's are represented with a high amplitude signal.
- Frequency shift keying is a type of modulation where 0's are represented with a low frequency signal and 1's are represented with a high frequency signal.

## Gameplay
In each level an emoji image is converted into binary values (0's and 1's) and then displayed as a modulated radio signal. The player must decode the signal to reveal the emoji image. As the radio signal moves across the screen, the player decides if each chunk of the signal is a 0 or a 1. At the end of the level, the image decoded by the player is compared to the actual image.

![Operation Modulation gameplay](https://github.com/mklewandowski/operation-modulation/blob/main/operation-modulation-gameplay.gif?raw=true)

## Supported Platforms
Operation Modulation is designed for use on multiple platforms including:
- Web

## Running Locally
Use the following steps to run locally:
1. Clone this repo
2. Open repo folder using Unity 2021.3.35f1
3. Install Text Mesh Pro

## Building the Project

### WebGL Build
For embedding within itch.io and other web pages, we use the `better-minimal-webgl-template` seen here:
https://seansleblanc.itch.io/better-minimal-webgl-template

Setup of the `better-minimal-webgl-template` is as follows:
1. Download and unzip the template.
2. Copy the `WebGLTemplates` folder into the `Assets` folder.
3. File -> Build Settings... -> WebGL -> Player Settings... -> Select the "BetterMinimal" template.
4. Enter color in the "Background" field.
5. Enter "false" in the "Scale to fit" field to disable scaling.
6. Enter "true" in the "Optimize for pixel art" field to use CSS more appropriate for pixel art.

### Running a Unity WebGL Build
1. Install the "Live Server" VS Code extension.
2. Open the WebGL build output directory with VS Code.
3. Right-click `index.html`, and select "Open with Live Server".

## Development Tools
- Created using Unity
- Code edited using Visual Studio Code