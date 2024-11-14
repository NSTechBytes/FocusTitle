

# FocusTitle Rainmeter Plugin

This Rainmeter plugin retrieves the name of the currently focused application on your Windows machine. It provides a measure that you can use in your Rainmeter skins to dynamically display the active application's name, such as "Chrome", "Notepad", "Explorer", etc.

## Features
- Retrieves the name of the currently focused application (e.g., "chrome", "notepad").
- No window title, only the application name.
- Easy integration into your Rainmeter skins.

## Requirements
- **Rainmeter**: Make sure you have Rainmeter installed on your computer.
- This plugin works on **Windows** (tested on Windows 10 and 11).

## Installation
1. Download the latest release of the **FocusAppName** plugin from the releases section.
2. Extract the contents of the `.zip` file.
3. Copy the `FocusTitle.dll` file to the `Plugins` directory in your Rainmeter installation folder:
   - **Default path**: `C:\Program Files\Rainmeter\Plugins\`
4. Create a Rainmeter skin or use an existing one to display the focused application name.

## Usage

### Example Skin

Here is an example of how to use this plugin in a Rainmeter skin to display the focused application:

```ini
[Rainmeter]
Update=1000
DynamicWindowSize=1  ; Adjusts the skin size based on the text length
solidColor = 000000
Backgroundmode=2

[FocusedWindowMeasure]
Measure=Plugin
Plugin=FocusTitle
UpdateDivider=1  


[FocusedWindow]
Meter=String
MeasureName=FocusedWindowMeasure
FontColor=255,255,255
FontSize=14
FontFace=Arial
Text="Focused Window: %1"
AntiAlias=1
DynamicVariables=1
X=10
Y=10
W=800

```

### Explanation:
- **FocusedWindowMeasure**: The measure that calls the plugin and retrieves the focused application's name.
- **FocusedApp**: A text meter that displays the focused application's name. It updates every second (1000ms).
- The skin will display the focused application like `chrome`, `notepad`, `explorer`, etc.

## Methods

### Measure
- **GetAppName**: Returns the name of the focused application (e.g., `chrome`, `notepad`, etc.).
- **Update**: Returns `1.0` if an application is in focus and `0.0` otherwise.
- **GetString**: Returns a string with the application's name.

### Plugin Functions

- **Initialize**: Initializes the plugin and allocates resources.
- **Finalize**: Cleans up and frees resources when the plugin is no longer in use.
- **Reload**: Reloads any dynamic configuration (e.g., if Rainmeter variables change).
- **Update**: Called by Rainmeter to get the current value of the measure (focused application).
- **GetString**: Called by Rainmeter to get the string value of the measure.

## How It Works
- This plugin uses Windows API calls (`user32.dll`, `psapi.dll`, and `kernel32.dll`) to get the currently focused window.
- It retrieves the process ID of the window, opens the process, and fetches the executable's name.
- The result is displayed as the application name in your Rainmeter skin.

## Troubleshooting

- If the plugin is not working, ensure that Rainmeter has the necessary permissions to interact with the system and retrieve window information.
- Make sure you are running Rainmeter with administrator privileges if needed.
- If no application is focused, the plugin will return an empty string.

## License
This project is licensed under the Apache - see the [LICENSE](LICENSE) file for details.

## Contributing
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Create a pull request.

