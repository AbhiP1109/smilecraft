
# Email Setup Guide

This guide explains how to configure Gmail SMTP for the SmileCraft Dental Clinic contact form email notifications.

## Prerequisites

- A Gmail account (e.g., `your-clinic-email@gmail.com`)
- Access to the Gmail account settings

---

## Step 1: Enable 2-Step Verification

App Passwords require 2-Step Verification to be enabled on your Google account.

1. Go to [myaccount.google.com](https://myaccount.google.com)
2. Click on **Security** in the left sidebar
3. Under "Signing in to Google", click **2-Step Verification**
4. Follow the prompts to enable it (you'll need a phone number for verification)
5. Once enabled, you'll see "2-Step Verification: On"

---

## Step 2: Create an App Password

1. Go to [myaccount.google.com](https://myaccount.google.com)
2. Click on **Security** in the left sidebar
3. Under "Signing in to Google", click **App passwords**
   - If you don't see this option, 2-Step Verification may not be enabled, or your account may not support it (e.g., workspace accounts with admin restrictions)
4. You may be asked to sign in again
5. In the "App name" field, enter something like `SmileCraft API`
6. Click **Create**
7. Google will generate a 16-character password (e.g., `abcd efgh ijkl mnop`)
8. **Copy this password immediately** — you won't be able to see it again

---

## Step 3: Configure the Settings

### Update `appsettings.json`

Open `appsettings.json` and update the `SmtpSettings` section:

```json
{
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-clinic-email@gmail.com",
    "Password": "your-16-character-app-password",
    "FromEmail": "your-clinic-email@gmail.com",
    "FromName": "SmileCraft Dental Clinic",
    "ToEmail": "your-clinic-email@gmail.com",
    "ToName": "Clinic Manager",
    "EnableSsl": true
  }
}
```

### Update `appsettings.Development.json`

Add the same `SmtpSettings` section to `appsettings.Development.json` for local development:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-clinic-email@gmail.com",
    "Password": "your-16-character-app-password",
    "FromEmail": "your-clinic-email@gmail.com",
    "FromName": "SmileCraft Dental Clinic",
    "ToEmail": "your-clinic-email@gmail.com",
    "ToName": "Clinic Manager",
    "EnableSsl": true
  }
}
```

> **Important:** Replace `your-clinic-email@gmail.com` with your actual Gmail address and `your-16-character-app-password` with the App Password you generated in Step 2.

---

## Step 4: Test the Configuration

1. Run the API:
   ```bash
   dotnet run
   ```

2. Submit a test contact form via the frontend or using a tool like Postman:
   ```bash
   curl -X POST https://localhost:7001/api/contact \
     -H "Content-Type: application/json" \
     -d '{
       "name": "Test Patient",
       "email": "test@example.com",
       "phone": "+91 98765 43210",
       "message": "This is a test message."
     }'
   ```

3. Check your Gmail inbox for the notification email
4. Check the application logs for any errors

---

## Troubleshooting

### "Authentication failed" or "Username and Password not accepted"

- Make sure you're using the **App Password**, not your regular Gmail password
- Make sure 2-Step Verification is enabled
- Make sure there are no extra spaces in the password in `appsettings.json`
- Try generating a new App Password

### "The SMTP server requires a secure connection"

- Make sure `EnableSsl` is set to `true`
- Make sure the port is `587` (not `25` or `465`)

### Email not arriving

- Check the spam/junk folder in Gmail
- Check the application logs for errors
- Make sure the `ToEmail` address is correct
- Verify that the Gmail account is not locked or restricted

### Emails going to spam

- Add the sender address to your contacts
- Set up a filter in Gmail to never send emails from this address to spam
- Consider setting up SPF/DKIM records if using a custom domain

---

## Security Notes

- **Never commit your App Password to version control.** Add `appsettings.Development.json` to your `.gitignore` if it contains sensitive data.
- For production, consider using **Azure Key Vault**, **AWS Secrets Manager**, or **environment variables** to store the SMTP password.
- The App Password should be treated like a regular password — keep it secure and rotate it periodically.

---

## Alternative: Using Environment Variables

Instead of storing the password in `appsettings.json`, you can use environment variables:

```bash
# Windows (PowerShell)
$env:SmtpSettings__Password="your-app-password"

# Linux/macOS
export SmtpSettings__Password="your-app-password"
```

This is the recommended approach for production deployments.
