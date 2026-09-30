# SmileCraft - Detailed Deployment Guide

## Free Deployment Platforms Comparison

| Platform | Free Tier | Best For | SSL | Custom Domain | Difficulty |
|----------|-----------|----------|-----|---------------|------------|
| **Render** | 750 hrs/month (auto-sleep) | Full-stack Docker | Yes | Yes | Easy |
| **Railway** | $5/month credit | .NET apps | Yes | Yes | Easy |
| **Azure (F1)** | 60 min/day, 1GB storage | Enterprise | Yes | Yes (limited) | Medium |
| **Vercel + Render** | Unlimited* + 750 hrs | Separate scaling | Yes | Yes | Medium |
| **GitHub Pages** | 100 GB/month bandwidth | Frontend only | Yes | Yes | Easy |
| **Netlify** | 100 GB/month | Frontend only | Yes | Yes | Easy |
| **Fly.io** | 3 shared VMs | Full-stack | Yes | Yes | Medium |
| **Koyeb** | 1 web service | Full-stack | Yes | Yes | Easy |

*All recommended options support Docker, which means one deploy runs both frontend + backend.*

---

## Option 1: Render (Recommended - Easiest Full-Stack)

### Why Render?
- Docker support (frontend + backend in one service)
- Auto-deploy from GitHub
- Free SSL + custom domain
- Zero config needed

### Prerequisites
- GitHub account
- Code pushed to GitHub repo

### Step-by-Step Deployment

#### Step 1: Push Code to GitHub
```bash
cd D:\Practice\SmileCraft
git init
git add .
git commit -m "SmileCraft - Dental Clinic Website"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/smilecraft.git
git push -u origin main
```

#### Step 2: Create Render Account
1. Go to https://render.com
2. Click "Sign Up" → Choose "Sign up with GitHub"
3. Authorize Render to access your GitHub

#### Step 3: Create Web Service
1. Click "New +" button (top right)
2. Select "Web Service"
3. Connect your GitHub repository:
   - Click "Connect" next to your `smilecraft` repo
   - If repo not visible, click "Configure Account" → "All Repositories"

#### Step 4: Configure Service
| Field | Value |
|-------|-------|
| Name | `smilecraft` |
| Region | `Oregon` (or closest to you) |
| Branch | `main` |
| Runtime | `Docker` (auto-detected) |
| Plan | `Free` |

#### Step 5: Deploy
1. Click "Create Web Service"
2. Render will automatically:
   - Build Docker image (Node.js → .NET 8 → runtime)
   - Deploy to production
   - Provide HTTPS URL

#### Step 6: Access Your Site
- **URL:** `https://smilecraft.onrender.com`
- **API Docs:** `https://smilecraft.onrender.com/swagger`

#### Step 7: Add Custom Domain (Optional)
1. Go to your service → Settings → Custom Domains
2. Click "Add Custom Domain"
3. Enter your domain (e.g., `smilecraft.in`)
4. Update DNS records as shown:
   - Type: `CNAME`
   - Name: `www`
   - Value: `smilecraft.onrender.com`
5. Wait for SSL provisioning (up to 24 hours)

### Render Environment Variables
Go to your service → Environment:
| Key | Value |
|-----|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ASPNETCORE_URLS` | `http://+:8080` |

---

## Option 2: Railway (Best for .NET)

### Why Railway?
- Native .NET support
- $5 free credit every month
- Generous free tier
- Instant deployments

### Step-by-Step Deployment

#### Step 1: Push Code to GitHub
```bash
git init
git add .
git commit -m "SmileCraft - Dental Clinic Website"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/smilecraft.git
git push -u origin main
```

#### Step 2: Create Railway Account
1. Go to https://railway.app
2. Click "Start a New Project"
3. Choose "Deploy from GitHub repo"
4. Sign in with GitHub and authorize

#### Step 3: Deploy from GitHub
1. Select your `smilecraft` repository
2. Railway auto-detects Dockerfile
3. Click "Deploy"

#### Step 4: Configure Domain
1. Go to your service → Settings → Networking
2. Click "Generate Domain"
3. Your URL: `https://smilecraft-production.up.railway.app`

#### Step 5: Add Custom Domain (Optional)
1. Settings → Networking → Custom Domains
2. Add your domain
3. Update DNS:
   - Type: `CNAME`
   - Value: `smilecraft-production.up.railway.app`

### Railway Environment Variables
| Key | Value |
|-----|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `PORT` | `8080` |

---

## Option 3: Azure App Service (Free F1 Tier)

### Why Azure?
- Microsoft ecosystem integration
- Free F1 tier (60 min/day)
- Enterprise-grade reliability
- Easy scaling path

### Prerequisites
- Azure account (free at https://azure.com/free)
- Azure CLI installed

### Step-by-Step Deployment

#### Step 1: Install Azure CLI
```powershell
winget install Microsoft.AzureCLI
```

#### Step 2: Login to Azure
```bash
az login
```

#### Step 3: Create Resource Group
```bash
az group create --name SmileCraftRG --location eastus
```

#### Step 4: Create App Service Plan (Free)
```bash
az appservice plan create \
  --name SmileCraftPlan \
  --resource-group SmileCraftRG \
  --sku F1 \
  --is-linux
```

#### Step 5: Create Web App
```bash
az webapp create \
  --name smilecraft-app \
  --resource-group SmileCraftRG \
  --plan SmileCraftPlan \
  --runtime "dotnet:8"
```

#### Step 6: Build and Publish
```bash
cd D:\Practice\SmileCraft\SmileCraft.API
dotnet publish -c Release -o ./publish
```

#### Step 7: Create Deployment ZIP
```powershell
Compress-Archive -Path ./publish/* -DestinationPath ./deploy.zip -Force
```

#### Step 8: Deploy to Azure
```bash
az webapp deploy \
  --name smilecraft-app \
  --resource-group SmileCraftRG \
  --src-path ./deploy.zip
```

#### Step 9: Configure Environment Variables
```bash
az webapp config appsettings set \
  --name smilecraft-app \
  --resource-group SmileCraftRG \
  --settings ASPNETCORE_ENVIRONMENT=Production
```

#### Step 10: Access Your Site
- **URL:** `https://smilecraft-app.azurewebsites.net`

#### Step 11: Add Custom Domain (Optional)
1. Go to Azure Portal → Your Web App → Custom Domains
2. Add domain and verify ownership
3. Upload SSL certificate (free with managed certificates)

---

## Option 4: Vercel + Render (Separate Frontend/Backend)

### Why Separate?
- Frontend scales independently
- Better performance (CDN)
- More flexible

### Part A: Deploy Frontend to Vercel

#### Step 1: Push Code to GitHub
```bash
git init
git add .
git commit -m "SmileCraft - Dental Clinic Website"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/smilecraft.git
git push -u origin main
```

#### Step 2: Create Vercel Account
1. Go to https://vercel.com
2. Sign up with GitHub

#### Step 3: Import Project
1. Click "Add New" → "Project"
2. Select your `smilecraft` repo
3. Configure:
   - **Framework Preset:** Angular
   - **Root Directory:** `SmileCraft.UI/smilecraft-ui`
   - **Build Command:** `npm run build`
   - **Output Directory:** `dist/smilecraft-ui`

#### Step 4: Environment Variables
| Key | Value |
|-----|-------|
| `API_URL` | `https://your-render-app.onrender.com/api` |

#### Step 5: Deploy
1. Click "Deploy"
2. Your frontend: `https://smilecraft.vercel.app`

### Part B: Deploy Backend to Render
Follow Option 1, but update CORS in `Program.cs`:
```csharp
policy.WithOrigins("https://smilecraft.vercel.app")
```

---

## Option 5: GitHub Pages (Frontend Only)

### Step-by-Step Deployment

#### Step 1: Update Angular Configuration
```typescript
// angular.json - add base href
"architect": {
  "build": {
    "options": {
      "baseHref": "/smilecraft/"
    }
  }
}
```

#### Step 2: Build for GitHub Pages
```bash
cd SmileCraft.UI/smilecraft-ui
npm run build -- --base-href /smilecraft/
```

#### Step 3: Push to GitHub
```bash
git init
git add .
git commit -m "Deploy to GitHub Pages"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/smilecraft.git
git push -u origin main
```

#### Step 4: Enable GitHub Pages
1. Go to your repo on GitHub
2. Settings → Pages
3. Source: `main` branch, `/ (root)` folder
4. Click Save

#### Step 5: Access Your Site
- **URL:** `https://YOUR_USERNAME.github.io/smilecraft/`

---

## Post-Deployment Checklist

- [ ] Test all pages load correctly
- [ ] Test contact form submission
- [ ] Test responsive design (mobile/tablet/desktop)
- [ ] Verify all images load
- [ ] Check SSL certificate is active
- [ ] Test on different browsers
- [ ] Update `app-config.json` with real clinic info
- [ ] Set up Google Analytics (optional)
- [ ] Submit to Google Search Console (optional)

---

## Troubleshooting

### CORS Errors
**Problem:** `Access to fetch at '...' from origin '...' has been blocked by CORS policy`

**Solution:** Update `Program.cs` CORS policy:
```csharp
policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
```

### 404 on Page Refresh
**Problem:** Refreshing `/about` shows 404

**Solution:** Ensure `Program.cs` has:
```csharp
app.MapFallbackToFile("index.html");
```

### Docker Build Fails
**Problem:** `npm ci` fails in Docker

**Solution:** Delete `package-lock.json` and retry, or use `npm install` instead

### Site Not Loading
**Problem:** Blank page after deployment

**Solution:**
1. Check browser console for errors
2. Verify API URL in Angular environment files
3. Check network tab for failed requests

---

## Cost Optimization Tips

1. **Render:** Auto-sleeps after 15 min inactivity (free tier)
2. **Railway:** $5 credit resets monthly, use it wisely
3. **Azure F1:** 60 min/day is enough for low-traffic sites
4. **All platforms:** Use CDN for static assets to reduce bandwidth

---

## Next Steps After Deployment

1. **Update Config:** Edit `src/assets/config/app-config.json` with real clinic data
2. **Add Analytics:** Google Analytics or Plausible
3. **SEO:** Submit sitemap to Google Search Console
4. **Monitor:** Set up uptime monitoring (UptimeRobot free tier)
5. **Backup:** Regular Git commits for code backup
