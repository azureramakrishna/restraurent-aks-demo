# Restaurant Menu Application

A simple .NET 8 web application displaying a restaurant menu.

## Local Development

```bash
dotnet run
```

Visit `http://localhost:5000` to view the menu.

## Azure Deployment Setup

1. Create an Azure Web App (Linux, .NET 8)
2. Download the publish profile from Azure Portal
3. Add it as a GitHub secret named `AZURE_WEBAPP_PUBLISH_PROFILE`
4. Update `AZURE_WEBAPP_NAME` in `.github/workflows/azure-deploy.yml`
5. Push to main branch to trigger deployment
