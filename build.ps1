# SmileCraft Build Script
# Builds Angular frontend, copies to .NET wwwroot, builds .NET API, and runs the app

param(
    [switch]$SkipAngular,
    [switch]$SkipDotNet,
    [switch]$Run
)

$ErrorActionPreference = "Stop"
$rootDir = $PSScriptRoot
$angularDir = Join-Path $rootDir "SmileCraft.UI\smilecraft-ui"
$apiDir = Join-Path $rootDir "SmileCraft.API"
$wwwrootDir = Join-Path $apiDir "wwwroot"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  SmileCraft Build Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# ---- Step 1: Build Angular ----
if (-not $SkipAngular) {
    Write-Host "[1/4] Building Angular frontend..." -ForegroundColor Yellow
    
    Push-Location $angularDir
    
    # Install dependencies if node_modules doesn't exist
    if (-not (Test-Path "node_modules")) {
        Write-Host "  Installing npm dependencies..." -ForegroundColor Gray
        npm ci
    }
    
    # Build Angular app
    Write-Host "  Running ng build..." -ForegroundColor Gray
    npm run build
    
    if ($LASTEXITCODE -ne 0) {
        Pop-Location
        Write-Host "Angular build failed!" -ForegroundColor Red
        exit 1
    }
    
    Pop-Location
    Write-Host "  Angular build completed successfully!" -ForegroundColor Green
} else {
    Write-Host "[1/4] Skipping Angular build..." -ForegroundColor DarkGray
}

# ---- Step 2: Copy Angular output to wwwroot ----
if (-not $SkipAngular) {
    Write-Host "[2/4] Copying Angular build to wwwroot..." -ForegroundColor Yellow
    
    $distDir = Join-Path $angularDir "dist\smilecraft-ui\browser"
    
    if (-not (Test-Path $distDir)) {
        Write-Host "  ERROR: Angular build output not found at $distDir" -ForegroundColor Red
        exit 1
    }
    
    # Clean and create wwwroot
    if (Test-Path $wwwrootDir) {
        Remove-Item -Path "$wwwrootDir\*" -Recurse -Force
    } else {
        New-Item -ItemType Directory -Path $wwwrootDir -Force | Out-Null
    }
    
    # Copy files
    Copy-Item -Path "$distDir\*" -Destination $wwwrootDir -Recurse -Force
    
    Write-Host "  Files copied to wwwroot!" -ForegroundColor Green
} else {
    Write-Host "[2/4] Skipping copy to wwwroot..." -ForegroundColor DarkGray
}

# ---- Step 3: Build .NET API ----
if (-not $SkipDotNet) {
    Write-Host "[3/4] Building .NET API..." -ForegroundColor Yellow
    
    Push-Location $apiDir
    
    dotnet build -c Release
    
    if ($LASTEXITCODE -ne 0) {
        Pop-Location
        Write-Host ".NET build failed!" -ForegroundColor Red
        exit 1
    }
    
    Pop-Location
    Write-Host "  .NET build completed successfully!" -ForegroundColor Green
} else {
    Write-Host "[3/4] Skipping .NET build..." -ForegroundColor DarkGray
}

# ---- Step 4: Run the application ----
if ($Run) {
    Write-Host "[4/4] Starting application..." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  SmileCraft is starting..." -ForegroundColor Cyan
    Write-Host "  API: http://localhost:5253" -ForegroundColor Cyan
    Write-Host "  Swagger: http://localhost:5253/swagger" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    
    Push-Location $apiDir
    dotnet run -c Release
    Pop-Location
} else {
    Write-Host "[4/4] Build complete! Use -Run flag to start the application." -ForegroundColor Green
    Write-Host ""
    Write-Host "To run manually:" -ForegroundColor Cyan
    Write-Host "  cd $apiDir" -ForegroundColor Gray
    Write-Host "  dotnet run" -ForegroundColor Gray
}
