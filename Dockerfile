# ============================================
# SmileCraft - Multi-stage Docker Build
# Stage 1: Build Angular frontend
# Stage 2: Build .NET API
# Stage 3: Serve everything from .NET on port 8080
# ============================================

# ---- Stage 1: Build Angular ----
FROM node:20-alpine AS angular-build
WORKDIR /src

# Copy package files
COPY SmileCraft.UI/smilecraft-ui/package*.json ./
RUN npm ci

# Copy Angular source
COPY SmileCraft.UI/smilecraft-ui/ .

# Build Angular app (production)
RUN npm run build

# ---- Stage 2: Build .NET API ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
WORKDIR /src

# Copy .NET project
COPY SmileCraft.API/ ./SmileCraft.API/

# Restore and build .NET API
WORKDIR /src/SmileCraft.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ---- Stage 3: Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install Node.js for any runtime needs (optional)
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

# Copy .NET published output
COPY --from=dotnet-build /app/publish .

# Copy Angular build output to wwwroot
COPY --from=angular-build /src/dist/smilecraft-ui ./wwwroot

# Expose port 8080
EXPOSE 8080

# Set environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD curl -f http://localhost:8080/api/clinicinfo || exit 1

ENTRYPOINT ["dotnet", "SmileCraft.API.dll"]
