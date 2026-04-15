@echo off
setlocal

:: ── Set version here before running ──────────────────────────
set VERSION=1.10.2

:: ─────────────────────────────────────────────────────────────

set BUILD_DIR=%~dp0build
set RELEASE_DIR=%~dp0releases
set ZIP_NAME=DiffusionToolkit-%VERSION%.zip

echo ============================================================
echo   Diffusion Toolkit Release Build -- %VERSION%
echo ============================================================
echo.

:: Clean and build
del /q "%BUILD_DIR%\*" 2>nul
dotnet publish Diffusion.Toolkit\Diffusion.Toolkit.csproj -c Release -r win-x64 -o .\build --no-self-contained /p:PublishSingleFile=true /p:PublishReadyToRun=false /p:DebugType=None /p:DebugSymbols=false
dotnet publish Diffusion.Updater\Diffusion.Updater.csproj -c Release -r win-x64 -o .\build --no-self-contained /p:PublishSingleFile=true /p:PublishReadyToRun=false /p:DebugType=None /p:DebugSymbols=false

:: Write version.txt
echo v%VERSION%> "%BUILD_DIR%\version.txt"

:: Create releases folder if needed
if not exist "%RELEASE_DIR%" mkdir "%RELEASE_DIR%"

:: Zip build output
echo.
echo Zipping build output...
powershell -Command "Compress-Archive -Path '%BUILD_DIR%\*' -DestinationPath '%RELEASE_DIR%\%ZIP_NAME%' -Force"

echo.
echo ============================================================
echo   Release zip ready: releases\%ZIP_NAME%
echo.
echo   Next steps:
echo     1. git tag %VERSION%
echo     2. git push origin %VERSION%
echo     3. Create GitHub Release tagged %VERSION%
echo     4. Upload: releases\%ZIP_NAME%
echo ============================================================
echo.
pause
