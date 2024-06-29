# Function to get file extensions from user input
function Get-FileExtensions {
    $extensions = Read-Host "Enter file extension(s) to copy (e.g., .cs, .xaml, .js)"
    return $extensions.Split(',').Trim() | ForEach-Object { $_.TrimStart('.') }
}

# Get the directory where the script is located
$scriptDir = $PSScriptRoot

# Destination directory
$destDir = "C:\Users\Admin\OneDrive - Politechnika Morska w Szczecinie\Desktop\FilesScript"

# Get file extensions from user
$fileExtensions = Get-FileExtensions

# Create destination directory if it doesn't exist
if (-not (Test-Path $destDir)) {
    New-Item -ItemType Directory -Path $destDir | Out-Null
}

# Delete existing files in the destination directory
Remove-Item "$destDir\*" -Recurse -Force

# Copy files with specified extensions
foreach ($ext in $fileExtensions) {
    Get-ChildItem -Path $scriptDir -Recurse -File | Where-Object { $_.Extension -eq ".$ext" } | ForEach-Object {
        $destPath = Join-Path $destDir $_.Name
        Copy-Item $_.FullName -Destination $destPath -Force
    }
}

Write-Host "File copy completed successfully."