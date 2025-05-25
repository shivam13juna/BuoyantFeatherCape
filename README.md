# 🕊️ BuoyantFeatherCape - Soar and Sprint Through Valheim! 🕊️

[![Build Status](https://github.com/shivam13juna/BuoyantFeatherCape/actions/workflows/release.yml/badge.svg)](https://github.com/shivam13juna/BuoyantFeatherCape/actions/workflows/release.yml) 

**Tired of mundane jumps? Wish your Feather Cape offered more than just a slow fall? Prepare to elevate your Valheim experience with FeatherCapeJump!**

This mod enhances the vanilla Feather Cape, transforming it into a tool of true aerial and ground agility. Leap higher and run faster to explore the world like never before!

# Link to nexusmod !
[![Link to Nexus Mod](https://img.shields.io/badge/Nexus%20Mods-FeatherCapeJump-blue.svg)](https://www.nexusmods.com/valheim/mods/3062)

---

## ✨ What does this mod do? ✨

FeatherCapeJump enhances the player's abilities when a Feather Cape is equipped. Experience:

*   **Enhanced Jump Height:** Reach new heights and overcome obstacles with ease. The jump height multiplier is configurable.
*   **Increased Run Speed:** Sprint faster across the lands (By default, the speed increment is 1.0 but it can be configured in the configuration manager).
*   **Configurable Settings:** Tailor the jump height and run speed multipliers to your liking via a configuration file (`BepInEx/config/FeatherCapeJump.Official.cfg`). 
    *   Jump multiplier can be set between `1.0` (no bonus) and `10.0` (10x jump height).
    *   Run speed multiplier can be set between `1.0` (no bonus) and `3.0` (3x run speed).

---

## 🚀 Features 🚀

*   **Seamless Integration:** Works flawlessly with the existing Feather Cape.
*   **Lightweight:** Designed to be performance-friendly.
*   **Easy to Use:** Simply equip your Feather Cape and start jumping and running with enhanced capabilities!
*   **Configurable Jump Height:** Adjust the `JumpHeightMultiplier` in the config file (default is `1.20`, i.e., 20% higher).
*   **Configurable Run Speed:** Adjust the `RunSpeedMultiplier` in the config file (default is `1.0`, i.e., no change to normal run speed).

---

## 🛠️ Installation 🛠️

1.  **Install BepInEx:** If you haven't already, download and install BepInEx for Valheim.
2.  **Download the Mod:** Grab the latest `FeatherCapeJump.dll` from the [Releases page](https://github.com/shivam13juna/BuoyantFeatherCape/releases). 
3.  **Extract:** Place the `FeatherCapeJump.dll` file into your `Valheim/BepInEx/plugins` folder.
4.  **Launch Valheim:** The mod should now be active! A configuration file will be generated at `BepInEx/config/FeatherCapeJump.Official.cfg` after the first run.

---

## 🤸 How to Use 🤸

1.  Craft or find a Feather Cape in Valheim.
2.  Equip the Feather Cape.
3.  Jump and Run! Enjoy your newfound aerial and terrestrial prowess.

---

## 🏗️ Building from Source 🏗️

If you'd like to build the mod yourself:

1.  **Clone this repository.**
2.  **Set up Game Libraries:**
    *   The project references game and BepInEx DLLs from a local folder named `_GameOriginalLibs`.
    *   Run the `update_local_libs.ps1` PowerShell script located in the root of the repository. This script will attempt to copy the necessary DLLs from a standard Valheim installation path (`C:\Program Files (x86)\Steam\steamapps\common\Valheim`).
    *   If your Valheim installation is in a different location, you'll need to edit the `$valheimBasePath` variable at the top of `update_local_libs.ps1` before running it.
    *   The script copies files like `BepInEx.dll`, `0Harmony.dll`, `assembly_valheim.dll`, `UnityEngine.dll`, and `UnityEngine.CoreModule.dll` into the `_GameOriginalLibs` directory.
3.  **Open the Solution:** Open `FeatherCapeJump.sln` in Visual Studio.
4.  **Install .NET SDK:** Ensure you have the .NET SDK (as specified in the project file, likely .NET Framework 4.7.2) installed.
5.  **Restore NuGet Packages:** Visual Studio should do this automatically, or you can do it manually.
6.  **Build the Solution:** Build the solution in `Release` configuration. The `FeatherCapeJump.dll` will be located in `bin/Release/net472/`.

**Build Workflow:**
The `update_local_libs.ps1` script is crucial for local development. It ensures that the project has access to the required game assemblies. When you build the project in Visual Studio, it compiles `Class1.cs` (and any other C# files) against these libraries, producing `FeatherCapeJump.dll`.

The project also includes a GitHub Actions workflow (`.github/workflows/release.yml`) to automatically build and create releases on pushes to the `master` branch that are tagged (e.g., `v1.0.0`). This automated workflow handles the build process in a clean environment.

---

## ❤️ Contributing ❤️

Contributions are welcome! If you have ideas for improvements, new features, or bug fixes, feel free to:

1.  Fork the repository.
2.  Create a new branch for your feature (`git checkout -b feature/AmazingFeature`).
3.  Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4.  Push to the branch (`git push origin feature/AmazingFeature`).
5.  Open a Pull Request.

---

## 📜 License 📜

This project is licensed under the MIT License - see the `LICENSE` file for details.

---

**Fly high, Viking!**
