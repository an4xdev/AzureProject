# Simple app that's using Azure resources

## Used resources

- SQL Database
- Service Bus
- Storage account
- Container registry
- App Services
- Container instances

## Features

- Add photos with description and in real time send notifications to another users.
- Toggle emotes.
- Add comments to photo.

## Local development

For local development use `docker-compose` where you have:
- SQL database image
- API image
- Client image

You also must have:
- Service bus with topic and subscriptions:
  - aa_example.com
  - bb_example.com
  - cc_example.com
  - dd_example.com
  - ee_example.com
  - (look for migration and Notifications.razor files)
  - (`@` symbol is forbidden in subscription name)
- Azure storage account with container `test` (look for `AzureFileService` and lines `19, 33, 46`) or implement missing functions and swap `IFileService` implementation to `LocalFileService`

Set variables in `.env` and in Shared project in DoNotGit -> `DoNotCommit.cs`

## My deployment experience

1. Create resource group with specific location.
2. Create database.
3. Create container registry.
4. Change in container registry -> settings -> access keys -> check Administrator.
5. Push API image to registry.
6. Get connection string to database like:
   1. Server=tcp:X.database.windows.net,1433;Initial Catalog=Y;Persist Security Info=False;User ID=W;Password=Z;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
7. Run migration to remote database.
8. Create web app from App Services from API container.
9. Set:
   1.  DB_CONNECTION_STRING
   2.  STORAGE_KEY
   3.  TOPIC_KEY
   4.  TOPIC_NAME
10. In Shared project -> DoNotGit -> DontCommit.cs change `API_URL` to existing API web app.
11. Build docker-compose.
12. Tag and push to container registry Client container.
13. Create instance of Client container from registry with port 80 (by default is added to configuration).
14. Add FRONTED_URL environmental variable to API web app like:
    1. http://X.Y.W.Z
15. Restart API web app manually and wait for all to startup up.

## Places to improve

Places that could have been better designed, but unfortunately were implemented in a non-professional way due to lack of time and experience:

- Better login system (in this state user is stored in LocalStorage of browser).
- Better understanding of CORS, so I don't have to fight and use `FRONTEND_URL` as environmental variable.
- Pass environmental variables to Client project (I was fighting with nginx, but maybe I don't need to use it?).
