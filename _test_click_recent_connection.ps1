param(
    [string]$ProfileName = "FKKTOTST"
)

# Add required assemblies
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes

# Screenshot function
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

# Find and click element using UI Automation
function Find-AndClick-Element {
    param(
        [System.Windows.Automation.AutomationElement]$Root,
        [string]$Name,
        [string]$ControlType = "Button"
    )
    
    try {
        $condition = New-Object System.Windows.Automation.PropertyCondition(
            [System.Windows.Automation.AutomationElement]::NameProperty, 
            $Name
        )
        
        $element = $Root.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $condition)
        
        if ($element) {
            Write-Host "   ✅ Found element: $Name" -ForegroundColor Green
            
            # Get clickable point
            if ($element.TryGetClickablePoint([ref]$null)) {
                $invokePattern = $element.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern)
                if ($invokePattern) {
                    $invokePattern.Invoke()
                    Write-Host "   ✅ Clicked: $Name" -ForegroundColor Green
                    return $true
                }
            }
        }
        
        Write-Host "   ⚠️ Element not found or not clickable: $Name" -ForegroundColor Yellow
        return $false
    }
    catch {
        Write-Host "   ❌ Error clicking $Name : $_" -ForegroundColor Red
        return $false
    }
}

Write-Host "🚀 WindowsDb2Editor - Recent Connection Clicker" -ForegroundColor Yellow
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

# Find the app window using UI Automation
Write-Host "🔍 Finding application window..." -ForegroundColor Cyan
$process = Get-Process -Name "WindowsDb2Editor" -ErrorAction SilentlyContinue | Select-Object -First 1

if ($process) {
    $hwnd = $process.MainWindowHandle
    if ($hwnd -ne [IntPtr]::Zero) {
        Write-Host "   ✅ Found window handle: $hwnd" -ForegroundColor Green
        
        # Get AutomationElement for the window
        $rootElement = [System.Windows.Automation.AutomationElement]::FromHandle($hwnd)
        
        if ($rootElement) {
            Write-Host "   ✅ Got UI Automation root element" -ForegroundColor Green
            
            Take-Screenshot "02_before_click" "Before clicking recent connection"
            Start-Sleep -Seconds 1
            
            # Try to find and click the FKKTOTST recent connection
            Write-Host "🖱️  Attempting to click recent connection: $ProfileName..." -ForegroundColor Cyan
            
            # Search for text containing the profile name
            $nameCondition = New-Object System.Windows.Automation.PropertyCondition(
                [System.Windows.Automation.AutomationElement]::NameProperty,
                $ProfileName
            )
            
            $elements = $rootElement.FindAll(
                [System.Windows.Automation.TreeScope]::Descendants,
                $nameCondition
            )
            
            if ($elements.Count -gt 0) {
                Write-Host "   ✅ Found $($elements.Count) element(s) with name: $ProfileName" -ForegroundColor Green
                
                foreach ($elem in $elements) {
                    $controlType = $elem.Current.ControlType.ProgrammaticName
                    Write-Host "   📝 Element: $($elem.Current.Name) | Type: $controlType" -ForegroundColor Gray
                    
                    # Try to click it
                    try {
                        $invokePattern = $elem.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern)
                        if ($invokePattern) {
                            $invokePattern.Invoke()
                            Write-Host "   ✅ Clicked element!" -ForegroundColor Green
                            Start-Sleep -Seconds 2
                            Take-Screenshot "03_after_click" "After clicking $ProfileName"
                            Start-Sleep -Seconds 2
                            Take-Screenshot "04_connection_result" "Connection result"
                            break
                        }
                    }
                    catch {
                        Write-Host "   ⚠️ Could not invoke: $_" -ForegroundColor Yellow
                    }
                }
            }
            else {
                Write-Host "   ⚠️ No elements found with name: $ProfileName" -ForegroundColor Yellow
                Write-Host "   💡 Listing all clickable elements..." -ForegroundColor Cyan
                
                $allElements = $rootElement.FindAll(
                    [System.Windows.Automation.TreeScope]::Descendants,
                    [System.Windows.Automation.Condition]::TrueCondition
                )
                
                Write-Host "   📋 Found $($allElements.Count) total elements" -ForegroundColor Gray
                
                foreach ($elem in $allElements) {
                    if ($elem.Current.Name -match $ProfileName) {
                        Write-Host "   🎯 Match: $($elem.Current.Name) | $($elem.Current.ControlType.ProgrammaticName)" -ForegroundColor Green
                    }
                }
            }
            
            Take-Screenshot "05_final" "Final state"
        }
        else {
            Write-Host "   ❌ Could not get UI Automation element" -ForegroundColor Red
        }
    }
    else {
        Write-Host "   ❌ Window handle is zero" -ForegroundColor Red
    }
}
else {
    Write-Host "   ❌ Process not found" -ForegroundColor Red
}

Write-Host ""
Write-Host "=" * 70 -ForegroundColor Yellow
Write-Host "✅ Test complete!" -ForegroundColor Green
Write-Host "📁 Screenshots in: .\Screenshots\" -ForegroundColor Cyan
Write-Host ""
Write-Host "💡 Application is still running. Review and close manually." -ForegroundColor Yellow

