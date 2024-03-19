# operation-modulation
Operation Modulation is an arcade style game where players decode pixel art images sent over waveforms and encoded using modulation techniques.

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