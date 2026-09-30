#!/bin/bash
# SmileCraft Build Script (Bash version)
# Builds Angular frontend, copies to .NET wwwroot, builds .NET API, and runs the app

set -e

ROOT_DIR="$(cd "$(dirname "$0")" && pwd)"
ANGULAR_DIR="$ROOT_DIR/SmileCraft.UI/smilecraft-ui"
API_DIR="$ROOT_DIR/SmileCraft.API"
WWWROOT_DIR="$API_DIR/wwwroot"

echo "========================================"
echo "  SmileCraft Build Script"
echo "========================================"
echo ""

# Step 1: Build Angular
echo "[1/4] Building Angular frontend..."
cd "$ANGULAR_DIR"

if [ ! -d "node_modules" ]; then
    echo "  Installing npm dependencies..."
    npm ci
fi

echo "  Running ng build..."
npm run build

if [ $? -ne 0 ]; then
    echo "Angular build failed!"
    exit 1
fi

echo "  Angular build completed successfully!"

# Step 2: Copy Angular output to wwwroot
echo "[2/4] Copying Angular build to wwwroot..."
DIST_DIR="$ANGULAR_DIR/dist/smilecraft-ui/browser"

if [ ! -d "$DIST_DIR" ]; then
    echo "  ERROR: Angular build output not found at $DIST_DIR"
    exit 1
fi

# Clean and create wwwroot
rm -rf "$WWWROOT_DIR"
mkdir -p "$WWWROOT_DIR"

# Copy files
cp -r "$DIST_DIR"/* "$WWWROOT_DIR/"

echo "  Files copied to wwwroot!"

# Step 3: Build .NET API
echo "[3/4] Building .NET API..."
cd "$API_DIR"

dotnet build -c Release

if [ $? -ne 0 ]; then
    echo ".NET build failed!"
    exit 1
fi

echo "  .NET build completed successfully!"

# Step 4: Run the application (optional)
if [ "$1" == "--run" ]; then
    echo "[4/4] Starting application..."
    echo ""
    echo "========================================"
    echo "  SmileCraft is starting..."
    echo "  API: http://localhost:5253"
    echo "  Swagger: http://localhost:5253/swagger"
    echo "========================================"
    echo ""
    cd "$API_DIR"
    dotnet run -c Release
else
    echo "[4/4] Build complete! Use --run flag to start the application."
    echo ""
    echo "To run manually:"
    echo "  cd $API_DIR"
    echo "  dotnet run"
fi
