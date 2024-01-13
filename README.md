About
=========
Command Canvas is a simple Windows .net CLI application that renders an image file inside a Windows console using the 16 default console colors.
It is not currently compatible with Linux or MacOS.

Usage
=========
To use CommandCanvas, run it with the following syntax:
`CommandCanvas.exe <path>`

Accepted file formats are: BMP, GIF, EXIF, JPG, PNG and TIFF.

Use WASD to move the image around. 

How it Works
=========
The 16 console colors are mapped to their respective RGB values.

Other colours are defined as a background colour and a foreground colour, with the foreground being a unicode shade character: ░, ▒, or ▓. 
The RGB value is lerped from the background to the foreground at a weight determined by the shade character's pixel coverage.

Example Images
=========
![image](https://github.com/JadenSchulz/CommandCanvas/assets/154372172/8a3e9e8a-3ca3-4025-8b68-e68024588ff7)
![image](https://github.com/JadenSchulz/CommandCanvas/assets/154372172/8d8ff342-7930-4441-8d39-89c726003601)
