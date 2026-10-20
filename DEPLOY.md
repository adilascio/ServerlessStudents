# Deploying ServerlessStudents to Azure (GitHub Actions)

## 1. Create the Function App (Azure Portal)

1. **Create a resource → Function App**.
2. Hosting plan: **Consumption** (or Flex Consumption).
3. Runtime stack: **.NET**, version **10 (isolated worker)**.
4. Region: **Canada Central**.
5. Name it e.g. `serverless-students-adl` (must be globally unique) and create it.

## 2. Add the database connection string

Function App → **Settings → Environment variables → App settings → + Add**:

| Name | Value |
| --- | --- |
| `DATABASE_CONNECTION_STRING` | *(the connection string from the lab handout)* |

Click **Apply**. Do not commit the connection string to the repo.

## 3. Push this project to your own GitHub repo

The cloned `origin` points at the instructor's repo, so point it at yours:

```bash
git remote set-url origin https://github.com/<your-username>/ServerlessStudents.git
git add .
git commit -m "Add WhoAmI endpoint and GitHub Actions deployment"
git push -u origin main
```

## 4. Connect GitHub Actions to Azure

1. Function App → **Settings → Configuration → General settings**: turn **SCM Basic Auth Publishing Credentials** **On** and save.
2. Function App → **Overview → Get publish profile**; open the downloaded file and copy its full contents.
3. GitHub repo → **Settings → Secrets and variables → Actions → New repository secret**:
   - Name: `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`
   - Value: the publish profile contents
4. In `.github/workflows/deploy-azure-functions.yml`, set `AZURE_FUNCTIONAPP_NAME` to your Function App's name and push.

Every push to `main` now builds and deploys. Watch the run under the repo's **Actions** tab.

## 5. Verify

```
https://<app-name>.azurewebsites.net/api/WhoAmI
https://<app-name>.azurewebsites.net/api/students
```

If the Azure SQL server rejects the connection, it needs a firewall rule allowing Azure services.
