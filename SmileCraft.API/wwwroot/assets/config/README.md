# SmileCraft Configuration Guide

This directory contains the dynamic configuration file for the SmileCraft Dental Clinic website.

## 📁 Files

- `app-config.json` - Main configuration file (edit this to update site content)
- `README.md` - This documentation file

## 🔧 How to Edit

1. Open `app-config.json` in any text editor
2. Modify the values as needed
3. Save the file
4. Refresh the browser to see changes

**No rebuild or redeploy needed!** The config is loaded at runtime.

## 📋 Field Reference

### Clinic
| Field | Description | Example |
|-------|-------------|---------|
| `name` | Clinic name displayed throughout the site | "SmileCraft Dental Clinic" |
| `tagline` | Short tagline for the hero section | "Your Smile, Our Priority" |
| `description` | Detailed clinic description | "Premium dental care..." |
| `logo` | Path to logo image | "assets/images/logo.png" |
| `favicon` | Path to favicon | "favicon.ico" |

### Contact
| Field | Description | Example |
|-------|-------------|---------|
| `address` | Full clinic address | "123 Dental Street..." |
| `phone` | Primary phone number | "+91 98765 43210" |
| `phoneSecondary` | Secondary phone number | "+91 22 4890 1234" |
| `email` | Contact email address | "hello@smilecraft.in" |
| `emergencyPhone` | Emergency contact number | "+91 98765 43211" |

### Social Media
| Field | Description | Example |
|-------|-------------|---------|
| `facebook` | Facebook page URL | "https://facebook.com/smilecraft" |
| `instagram` | Instagram profile URL | "https://instagram.com/smilecraft" |
| `twitter` | Twitter/X profile URL | "https://twitter.com/smilecraft" |
| `linkedin` | LinkedIn company URL | "https://linkedin.com/company/smilecraft" |
| `youtube` | YouTube channel URL | "https://youtube.com/@smilecraft" |
| `whatsapp` | WhatsApp chat link | "https://wa.me/919876543210" |

### Working Hours
Array of objects with `days` and `time` fields:
```json
{ "days": "Monday - Friday", "time": "9:00 AM - 8:00 PM" }
```

### Map
| Field | Description | Example |
|-------|-------------|---------|
| `embedUrl` | Google Maps embed URL | "https://www.google.com/maps/embed?..." |
| `link` | Google Maps directions link | "https://maps.google.com/?q=..." |

### SEO
| Field | Description | Example |
|-------|-------------|---------|
| `title` | Page title for search engines | "SmileCraft Dental Clinic \| Best Dentist in Mumbai" |
| `description` | Meta description | "SmileCraft Dental Clinic offers..." |
| `keywords` | SEO keywords (comma-separated) | "dental clinic mumbai, best dentist..." |

## 🔗 Adding New Social Media Links

To add a new social media platform:

1. Add a new field in the `social` section of `app-config.json`:
   ```json
   "social": {
     "existing": "...",
     "newplatform": "https://newplatform.com/yourpage"
   }
   ```

2. Update the footer component to display the new icon

## 🗺️ Updating the Map Embed URL

1. Go to Google Maps and search for your clinic location
2. Click "Share" → "Embed a map"
3. Copy the iframe src URL
4. Paste it into the `map.embedUrl` field

## ⚠️ Important Notes

- Keep the JSON format valid (use a JSON validator if unsure)
- All fields are optional - missing fields use default values
- Changes take effect immediately on browser refresh
- The config file is served as a static asset from `src/assets/config/`
