# FocusTitle Rainmeter Plugin

The **FocusTitle** Rainmeter plugin allows you to display the title of the currently focused window or the application name in Rainmeter. It supports two types of information display: `WindowTitle` for the full title of the focused window and `ApplicationTitle` for the name of the focused application.

## Features

- **Display Full Window Title**: Shows the complete title of the currently focused window.
- **Display Application Name**: Shows only the name of the focused application (e.g., `notepad`, `chrome`).

## Installation

1. Download the `FocusTitle.dll` file from the [Releases](https://github.com/NSTechBytes/FocusTitle/releases) section.
2. Place `FocusTitle.dll` in your Rainmeter `Plugins` folder, typically located at:
   ```
   C:\Program Files\Rainmeter\Plugins\
   ```
3. Restart Rainmeter to load the plugin.

## Usage

In your Rainmeter skin, add a measure using `FocusTitle.dll`, and set the `Type` parameter to either `WindowTitle` or `ApplicationTitle` depending on what you want to display.

### Example Skin

```ini
[Rainmeter]
Update=1000
BackgroundMode=2
SolidColor=FFFFFF
DynamicWindowSize=1

[FocusedWindow]
Measure=Plugin
Plugin=FocusTitle
Type=WindowTitle

[FocusedApp]
Measure=Plugin
Plugin=FocusTitle
Type=ApplicationTitle

[WindowText]
Meter=STRING
MeasureName=FocusedWindow
X=5
Y=5
FontColor=000000
Text="Window Title: %1"
Antialias=1

[AppText]
Meter=STRING
MeasureName=FocusedApp
X=5
Y=25
FontColor=000000
Text="Application Name: %1"
Antialias=1
```

### Parameters

- **Type**: Defines the type of title to display.
  - `WindowTitle`: Displays the full title of the currently focused window.
  - `ApplicationTitle`: Displays only the name of the focused application.

## Building the Plugin

If you want to build this plugin from the source, clone this repository and build it in Visual Studio. Make sure you have the Rainmeter SDK configured.

1. Clone the repository:
   ```bash
   git clone https://github.com/NSTechBytes/FocusTitle.git
   ```
2. Open the solution in Visual Studio.
3. Build the project to generate `FocusTitle.dll`.

## License

This project is licensed under the Apache License v2.0. See the [LICENSE](LICENSE) file for details.
