param(
    [string]$ProfileName = "FKKTOTST"
)

# Add required assemblies
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type @"
    using System;
    using System.Runtime.InteropServices;
    public class MouseHelper {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int SW_RESTORE = 9;
        public const int SW_MAXIMIZE = 3;
        
        public static void LeftClick() {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        
        public static void DoubleClick() {
            LeftClick();
            System.Threading.Thread.Sleep(100);
            LeftClick();
        }
    }
"@

function Take-Screenshot {
    param([string]$Name, [string]$Description)
    
    $dir = ".\Screenshots"
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    
    $timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
    $path = Join-Path $dir "${timestamp}_${Name}.png"
    
    Write-Host "📸 $Description" -ForegroundColor Cyan
    
    try {
        $bounds = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds
        $bitmap = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
        $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
        $graphics.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
        $bitmap.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
        $graphics.Dispose()
        $bitmap.Dispose()
        Write-Host "   ✅ Saved: $path" -ForegroundColor Green
        return $path
    }
    catch {
        Write-Host "   ❌ Failed: $_" -ForegroundColor Red
        return $null
    }
}

function Get-AppWindow {
    $process = Get-Process -Name "WindowsDb2Editor" -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($process) {
        return $process.MainWindowHandle
    }
    return [IntPtr]::Zero
}

function Bring-AppToFront {
    $hwnd = Get-AppWindow
    if ($hwnd -ne [IntPtr]::Zero) {
        # Maximize the window first
        [MouseHelper]::ShowWindow($hwnd, [MouseHelper]::SW_MAXIMIZE) | Out-Null
        Start-Sleep -Milliseconds 300
        [MouseHelper]::SetForegroundWindow($hwnd) | Out-Null
        Start-Sleep -Milliseconds 500
        return $true
    }
    return $false
}

Write-Host "🎯 Precise FKKTOTST Click Automation (Visual-Guided)" -ForegroundColor Yellow
Write-Host "=" * 70 -ForegroundColor Yellow
Write-Host ""

# Clean up
Write-Host "🧹 Cleaning up..." -ForegroundColor Cyan
taskkill /F /IM WindowsDb2Editor.exe 2>$null | Out-Null
Start-Sleep -Seconds 1

# Build
Write-Host "🔨 Building..." -ForegroundColor Cyan
dotnet build --verbosity quiet 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "   ✅ Build successful" -ForegroundColor Green
Write-Host ""

# Start app
Write-Host "🎯 Starting application..." -ForegroundColor Cyan
$exePath = ".\bin\Debug\net10.0-windows\WindowsDb2Editor.exe"
Start-Process $exePath
Start-Sleep -Seconds 3

# Take initial screenshot
Take-Screenshot "01_startup" "Application started"
Start-Sleep -Seconds 1

# Bring app to front and maximize
if (Bring-AppToFront) {
    Write-Host "   ✅ Window maximized and focused" -ForegroundColor Green
    
    Take-Screenshot "02_maximized" "Window maximized"
    Start-Sleep -Seconds 1
    
    # Get screen dimensions
    $screenWidth = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds.Width
    $screenHeight = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds.Height
    
    Write-Host "📐 Screen resolution: ${screenWidth}x${screenHeight}" -ForegroundColor Cyan
    Write-Host ""
    
    # Based on visual analysis of the provided screenshot:
    # FKKTOTST is located at approximately:
    # - X: 30% from left (accounting for the icon and padding in the list)
    # - Y: 58% from top (in the Recent Connections list, first item)
    
    $targetX = [int]($screenWidth * 0.30)
    $targetY = [int]($screenHeight * 0.58)
    
    Write-Host "🎯 Calculated FKKTOTST position (based on visual analysis):" -ForegroundColor Cyan
    Write-Host "   X: $targetX ($([int]($screenWidth * 0.30 / $screenWidth * 100))% of screen width)" -ForegroundColor Gray
    Write-Host "   Y: $targetY ($([int]($screenHeight * 0.58 / $screenHeight * 100))% of screen height)" -ForegroundColor Gray
    Write-Host ""
    
    # Move cursor to position
    Write-Host "🖱️  Step 1: Moving cursor to FKKTOTST..." -ForegroundColor Cyan
    [MouseHelper]::SetCursorPos($targetX, $targetY)
    Start-Sleep -Milliseconds 800
    
    Take-Screenshot "03_cursor_on_fkktotst" "Cursor positioned on FKKTOTST"
    Start-Sleep -Seconds 1
    
    # Double-click
    Write-Host "🖱️  Step 2: Double-clicking FKKTOTST..." -ForegroundColor Cyan
    [MouseHelper]::DoubleClick()
    
    Write-Host "⏳ Waiting for connection attempt..." -ForegroundColor Cyan
    Start-Sleep -Seconds 3
    
    Take-Screenshot "04_after_doubleclick" "After double-clicking FKKTOTST"
    Start-Sleep -Seconds 2
    
    Take-Screenshot "05_connection_result" "Connection result (should show connected tab or error)"
    Start-Sleep -Seconds 1
    
    # If the above didn't work, try slight variations
    Write-Host ""
    Write-Host "🔍 Trying alternative Y positions (in case of slight offset)..." -ForegroundColor Yellow
    
    $alternativeYPositions = @(
        [int]($screenHeight * 0.55),  # Slightly higher
        [int]($screenHeight * 0.60),  # Slightly lower
        [int]($screenHeight * 0.57),  # Just above
        [int]($screenHeight * 0.59)   # Just below
    )
    
    $attemptNum = 6
    foreach ($altY in $alternativeYPositions) {
        Write-Host "   Trying Y position: $altY..." -ForegroundColor Gray
        
        [MouseHelper]::SetCursorPos($targetX, $altY)
        Start-Sleep -Milliseconds 500
        
        Take-Screenshot "${attemptNum}a_cursor_alt_y" "Cursor at alternative Y: $altY"
        
        [MouseHelper]::DoubleClick()
        Start-Sleep -Seconds 2
        
        Take-Screenshot "${attemptNum}b_after_alt_click" "After clicking at Y: $altY"
        $attemptNum++
        
        Start-Sleep -Seconds 1
    }
    
    Take-Screenshot "99_final" "Final state"
}
else {
    Write-Host "   ❌ Could not find window" -ForegroundColor Red
}

Write-Host ""
Write-Host "=" * 70 -ForegroundColor Yellow
Write-Host "✅ Automation complete!" -ForegroundColor Green
Write-Host ""
Write-Host "📊 Analysis:" -ForegroundColor Cyan
Write-Host "   Based on the visual screenshot provided, FKKTOTST should be at:" -ForegroundColor Gray
Write-Host "   - X: ~30% of screen width (accounting for icon and padding)" -ForegroundColor Gray
Write-Host "   - Y: ~58% of screen height (in Recent Connections list)" -ForegroundColor Gray
Write-Host ""
Write-Host "📁 Screenshots saved in: .\Screenshots\" -ForegroundColor Cyan
Write-Host "💡 Review screenshots to verify cursor position" -ForegroundColor Yellow
Write-Host ""
Write-Host "📋 Latest screenshots:" -ForegroundColor Cyan
Get-ChildItem ".\Screenshots\*.png" | Sort-Object LastWriteTime -Descending | Select-Object -First 12 | ForEach-Object {
    Write-Host "   - $($_.Name)" -ForegroundColor Gray
}

