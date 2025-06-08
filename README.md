# 🕊️ BuoyantFeatherCape - Soar and Sprint Through Valheim! 🕊️

[![Build Status](https://github.com/shivam13juna/BuoyantFeatherCape/actions/workflows/release.yml/badge.svg)](https://github.com/shivam13juna/BuoyantFeatherCape/actions/workflows/release.yml) 

**Tired of mundane jumps? Wish your Feather Cape offered more than just a slow fall? Prepare to elevate your Valheim experience with BuoyantFeatherCape!**

This mod enhances the vanilla Feather Cape, transforming it into a tool of true aerial agility and optionally ground speed and magical regeneration. Leap higher by default, and optionally run faster or regenerate Eitr faster to explore the world like never before!

# Link to nexusmod
[![Link to Nexus Mod](https://img.shields.io/badge/Nexus%20Mods-BuoyantFeatherCape-blue.svg)](https://www.nexusmods.com/valheim/mods/3062)

---

## ✨ What does this mod do? ✨

BuoyantFeatherCape enhances the player's abilities when a Feather Cape is equipped. Experience:

*   **Enhanced Jump Height:** Reach new heights and overcome obstacles with ease. The jump height multiplier is configurable and enabled by default (20% higher).
*   **Optional Increased Run Speed:** Sprint faster across the lands if desired. By default, this feature is disabled (multiplier set to 1.0) but can be configured up to 3x speed.
*   **Optional Enhanced Eitr Regeneration:** Regenerate your magical energy faster when enabled. By default, this feature is disabled (multiplier set to 1.0) but can be configured up to 5x regeneration rate.
*   **Configurable Settings:** Tailor all multipliers to your liking via a configuration file (`BepInEx/config/BuoyantFeatherCape.Official.cfg`). 
    *   Jump multiplier can be set between `1.0` (no bonus) and `10.0` (10x jump height). **Default: 1.20 (20% bonus)**
    *   Run speed multiplier can be set between `1.0` (no bonus) and `3.0` (3x run speed). **Default: 1.0 (no bonus)**
    *   Eitr regeneration multiplier can be set between `1.0` (no bonus) and `5.0` (5x regen rate). **Default: 1.0 (no bonus)**

---

## 🚀 Features 🚀

*   **Seamless Integration:** Works flawlessly with the existing Feather Cape.
*   **Lightweight:** Designed to be performance-friendly.
*   **Easy to Use:** Simply equip your Feather Cape and start jumping with enhanced capabilities! Optionally configure speed and Eitr bonuses.
*   **Enhanced Jump Height:** Enabled by default with 20% higher jumps. Adjust the `JumpHeightMultiplier` in the config file.
*   **Optional Run Speed Boost:** Disabled by default. Set `RunSpeedMultiplier` above 1.0 to enable faster running.
*   **Optional Eitr Regeneration Boost:** Disabled by default. Set `EitrRegenMultiplier` above 1.0 to enable faster magical energy regeneration.

---

## 🛠️ Installation 🛠️

1.  **Install BepInEx:** If you haven't already, download and install BepInEx for Valheim.
2.  **Download the Mod:** Grab the latest `BuoyantFeatherCape.dll` from the [Releases page](https://github.com/shivam13juna/BuoyantFeatherCape/releases). 
3.  **Extract:** Place the `BuoyantFeatherCape.dll` file into your `Valheim/BepInEx/plugins` folder.
4.  **Launch Valheim:** The mod should now be active! A configuration file will be generated at `BepInEx/config/BuoyantFeatherCape.Official.cfg` after the first run.

---

## 🤸 How to Use 🤸

1.  Craft or find a Feather Cape in Valheim.
2.  Equip the Feather Cape.
3.  Jump! Enjoy your enhanced aerial prowess (20% higher jumps by default).
4.  **Optional:** Configure the mod by editing `BepInEx/config/BuoyantFeatherCape.Official.cfg` to:
    *   Adjust jump height multiplier (default: 1.20)
    *   Enable and adjust run speed multiplier (default: 1.0 - disabled)
    *   Enable and adjust Eitr regeneration multiplier (default: 1.0 - disabled)

---

## 🏗️ Building from Source 🏗️

If you'd like to build the mod yourself:

1.  **Clone this repository.**
2.  **Set up Game Libraries:**
    *   The project references game and BepInEx DLLs from a local folder named `_GameOriginalLibs`.
    *   Run the `update_local_libs.ps1` PowerShell script located in the root of the repository. This script will attempt to copy the necessary DLLs from a standard Valheim installation path (`C:\Program Files (x86)\Steam\steamapps\common\Valheim`).
    *   If your Valheim installation is in a different location, you'll need to edit the `$valheimBasePath` variable at the top of `update_local_libs.ps1` before running it.
    *   The script copies files like `BepInEx.dll`, `0Harmony.dll`, `assembly_valheim.dll`, `UnityEngine.dll`, and `UnityEngine.CoreModule.dll` into the `_GameOriginalLibs` directory.
3.  **Open the Solution:** Open `BuoyantFeatherCape.sln` in Visual Studio.
4.  **Install .NET SDK:** Ensure you have the .NET SDK (as specified in the project file, likely .NET Framework 4.7.2) installed.
5.  **Restore NuGet Packages:** Visual Studio should do this automatically, or you can do it manually.
6.  **Build the Solution:** Build the solution in `Release` configuration. The `BuoyantFeatherCape.dll` will be located in `bin/Release/net472/`.

**Build Workflow:**
The `update_local_libs.ps1` script is crucial for local development. It ensures that the project has access to the required game assemblies. When you build the project in Visual Studio, it compiles `Class1.cs` (and any other C# files) against these libraries, producing `BuoyantFeatherCape.dll`.

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
