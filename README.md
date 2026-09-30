# SmileCraft - Dental Clinic Management System

A full-stack dental clinic management system built with **Angular 19** (frontend) and **.NET 8** (backend API).

## Architecture

```
┌─────────────────────────────────────────────────────────┐
│                    SmileCraft                            │
│                                                          │
│  ┌──────────────────┐    ┌──────────────────────────┐   │
│  │   Angular 19     │    │      .NET 8 API          │   │
│  │   (Frontend)     │───▶│      (Backend)           │   │
│  │                  │    │                          │   │
│  │  - Components    │    │  - Controllers           │   │
│  │  - Services      │    │  - Models                │   │
│  │  - Routing       │    │  - Data Store            │   │
│  └──────────────────┘    └──────────────────────────┘   │
│           │                        │                     │
│           ▼                        ▼                     │
│  ┌──────────────────┐    ┌──────────────────────────┐   │
│  │  HttpClient      │    │  Static Files (wwwroot)  │   │
│  │  (API calls)     │    │  (Serves Angular build)  │   │
│  └──────────────────┘    └──────────────────────────┘   │
│                                                          │
│  Deployment: .NET serves both API + Angular static files │
└─────────────────────────────────────────────────────────┘
```

## Project Structure

```
SmileCraft/
├── SmileCraft.API/              # .NET 8 Web API
│   ├── Controllers/             # API controllers
│   │   ├── ClinicInfoController.cs
│   │   ├── ServicesController.cs
│   │   ├── DoctorsController.cs
│   │   ├── StatsController.cs
│   │   ├── TestimonialsController.cs
│   │   └── ContactController.cs
│   ├── Data/                    # Data layer
│   │   └── ClinicDataStore.cs
│   ├── Models/                  # Data models
│   │   ├── ClinicInfo.cs
│   │   ├── Service.cs
│   │   ├── Doctor.cs
│   │   ├── Stat.cs
│   │   ├── Testimonial.cs
│   │   └── ContactMessage.cs
│   ├── Properties/
│   ├── wwwroot/                 # Angular build output (auto-generated)
│   ├── Program.cs
│   ├── appsettings.json
│   └── SmileCraft.API.csproj
│
├── SmileCraft.UI/
│   └── smilecraft-ui/           # Angular 19 application
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/  # Reusable components
│       │   │   ├── pages/       # Page components
│       │   │   ├── shared/      # Shared services & models
│       │   │   │   ├── models/
│       │   │   │   └── services/
│       │   │   │       └── api.service.ts
│       │   │   ├── app.config.ts
│       │   │   └── app.routes.ts
│       │   ├── environments/
│       │   │   ├── environment.ts
│       │   │   └── environment.development.ts
│       │   ├── index.html
│       │   ├── main.ts
│       │   └── styles.css
│       ├── angular.json
│       ├── package.json
│       └── proxy.conf.json
│
├── Dockerfile                   # Multi-stage Docker build
├── .dockerignore
├── nginx.conf                   # Nginx reverse proxy config
├── build.ps1                    # PowerShell build script
├── build.sh                     # Bash build script
├── SmileCraft.sln               # Solution file
└── README.md
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/) (LTS recommended)
- [Angular CLI](https://angular.dev/) (`npm install -g @angular/cli`)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

## Getting Started

### Quick Start (Development Mode)

#### 1. Start the .NET API

```bash
cd SmileCraft.API
dotnet run
```

The API will be available at `http://localhost:5253`

#### 2. Start the Angular Development Server

```bash
cd SmileCraft.UI/smilecraft-ui
npm install
ng serve --proxy-config proxy.conf.json
```

The Angular app will be available at `http://localhost:4200`

### Production Build (Combined)

#### Using Build Script (Recommended)

**PowerShell:**
```powershell
.\build.ps1 -Run
```

**Bash:**
```bash
chmod +x build.sh
./build.sh --run
```

This will:
1. Build the Angular app
2. Copy the output to `SmileCraft.API/wwwroot`
3. Build the .NET API
4. Start the application

#### Manual Build

```bash
# 1. Build Angular
cd SmileCraft.UI/smilecraft-ui
npm run build

# 2. Copy to wwwroot
xcopy /E /I dist\smilecraft-ui\* ..\..\SmileCraft.API\wwwroot\

# 3. Build and run .NET
cd ..\..\SmileCraft.API
dotnet run
```

## API Documentation

Once the API is running, Swagger UI is available at:
- `http://localhost:5253/swagger`

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/clinicinfo` | Get clinic general information |
| GET | `/api/services` | Get all dental services |
| GET | `/api/services/{id}` | Get a specific service |
| GET | `/api/doctors` | Get all doctors |
| GET | `/api/doctors/{id}` | Get a specific doctor |
| GET | `/api/stats` | Get clinic statistics |
| GET | `/api/testimonials` | Get all testimonials |
| GET | `/api/testimonials/{id}` | Get a specific testimonial |
| POST | `/api/contact` | Submit a contact form |

## Deployment

### Docker (Recommended)

```bash
# Build the Docker image
docker build -t smilecraft .

# Run the container
docker run -p 8080:8080 smilecraft
```

The application will be available at `http://localhost:8080`

### Azure App Service

1. **Create an App Service:**
   - Runtime stack: `.NET 8`
   - OS: Linux (recommended) or Windows

2. **Deploy using Azure CLI:**
   ```bash
   az webapp up --name smilecraft-app --resource-group myResourceGroup --runtime "dotnet:8"
   ```

3. **Or deploy from local:**
   ```bash
   cd SmileCraft.API
   dotnet publish -c Release -o ./publish
   # Deploy the publish folder to Azure App Service
   ```

4. **Configure the App Service:**
   - Set `ASPNETCORE_ENVIRONMENT` to `Production`
   - Ensure the Angular build output is in `wwwroot`

### Nginx Reverse Proxy

If you prefer to serve the Angular app separately with Nginx:

```bash
# Copy nginx.conf to your Nginx config directory
sudo cp nginx.conf /etc/nginx/conf.d/smilecraft.conf

# Reload Nginx
sudo nginx -s reload
```

## Environment Configuration

### Angular Environment Files

- `src/environments/environment.ts` - Production (uses relative `/api` path)
- `src/environments/environment.development.ts` - Development (uses `http://localhost:5253/api`)

### .NET Configuration

- `appsettings.json` - Production settings
- `appsettings.Development.json` - Development settings

## Features

- **Modern Angular 19** with standalone components
- **.NET 8 Web API** with clean architecture
- **Responsive design** with Bootstrap 5
- **Font Awesome** icons
- **Docker** support for easy deployment
- **Swagger/OpenAPI** documentation
- **CORS** enabled for cross-origin requests
- **Static file serving** from .NET (no separate web server needed)

## License

MIT
