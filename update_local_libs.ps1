# PowerShell script to update _GameOriginalLibs from your local Valheim installation

# --- Configuration ---
# Adjust this path if your Valheim installation is different
$valheimBasePath = "C:\Program Files (x86)\Steam\steamapps\common\Valheim"

# Project's root directory (where this script is located)
$projectRoot = $PSScriptRoot

# Destination folder for the DLLs within your project
$destinationLibsFolder = Join-Path -Path $projectRoot -ChildPath "_GameOriginalLibs"

# --- DLLs to copy ---
# Format: @{ "SourceSubPath" = "FileName" }
# SourceSubPath is relative to $valheimBasePath
$dllsToCopy = @{
    "BepInEx\core" = @(
        "BepInEx.dll",
        "0Harmony.dll"
        # Add other files from BepInEx\core if needed, e.g., BepInEx.Harmony.dll
    );
    "valheim_Data\Managed" = @(
        "assembly_valheim.dll",
        "UnityEngine.dll",
        "UnityEngine.CoreModule.dll"
        # Add other UnityEngine.*.dll files if your mod specifically needs them
        # e.g., "UnityEngine.InputLegacyModule.dll", "UnityEngine.PhysicsModule.dll", etc.
    )
}

# --- Script Logic ---

# Ensure the destination folder exists
if (-not (Test-Path $destinationLibsFolder)) {
    Write-Host "Creating destination folder: $destinationLibsFolder"
    New-Item -ItemType Directory -Path $destinationLibsFolder -Force | Out-Null
}

Write-Host "Starting DLL copy process..."

$dllsToCopy.GetEnumerator() | ForEach-Object {
    $sourceFolderName = $_.Name
    $fileNames = $_.Value
    $fullSourcePath = Join-Path -Path $valheimBasePath -ChildPath $sourceFolderName

    if (-not (Test-Path $fullSourcePath)) {
        Write-Warning "Source folder not found: $fullSourcePath. Skipping files from this location."
        return # Skips to the next entry in $dllsToCopy
    }

    foreach ($fileName in $fileNames) {
        $sourceFile = Join-Path -Path $fullSourcePath -ChildPath $fileName
        $destinationFile = Join-Path -Path $destinationLibsFolder -ChildPath $fileName

        if (Test-Path $sourceFile) {
            Write-Host "Copying '$fileName' from '$fullSourcePath' to '$destinationLibsFolder'"
            Copy-Item -Path $sourceFile -Destination $destinationFile -Force
        } else {
            Write-Warning "Source file not found: $sourceFile. Cannot copy."
        }
    }
}

Write-Host "DLL copy process completed."
Write-Host "Remember to 'git add _GameOriginalLibs' and commit the changes if DLLs were updated."
