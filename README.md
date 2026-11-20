# Holly Travels

## Requirements

- Node.js `22.12+` (or `20.19+`) and npm
- .NET 9 SDK
- PostgreSQL running locally or reachable from your machine

Check your installed versions with:

```powershell
node --version
npm --version
dotnet --version
```

## Configure the database

The API requires a PostgreSQL connection string named
`ConnectionStrings:DefaultConnection`. Set it with .NET user secrets from the
repository root. This PowerShell example prompts for your local PostgreSQL
credentials instead of putting the password directly in the command:

```powershell
Set-Location 'C:\Users\hmkzx\Documents\holly-travels'
$dbUser = Read-Host 'PostgreSQL username'
$dbPassword = Read-Host 'PostgreSQL password' -AsSecureString
$plainPassword = [System.Net.NetworkCredential]::new('', $dbPassword).Password
$connectionString = "Host=localhost;Port=5432;Database=hollytravels;Username=$dbUser;Password=$plainPassword"
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' $connectionString --project .\backend\backend.csproj
Remove-Variable dbUser, dbPassword, plainPassword, connectionString
```

User secrets are stored outside the repository. Do not put database credentials
in source code or commit them. Run the same command again to update the value.

Apply the database migrations once the database is available:

```powershell
dotnet ef database update --project .\backend\backend.csproj
```

If `dotnet ef` is not installed, install the EF Core 9 tool once:

```powershell
dotnet tool install --global dotnet-ef --version 9.0.9
```

## Run the API and frontend

Run each command group in a separate PowerShell terminal.

### Terminal 1: API server

From the repository root, start the backend with the npm shortcut:

```powershell
Set-Location 'C:\Users\hmkzx\Documents\holly-travels'
npm run server
```

The API listens at `http://localhost:5258`. With the database configured and
migrated, check the trips endpoint in another terminal:

```powershell
Invoke-RestMethod http://localhost:5258/api/trips
```

### Terminal 2: frontend app

```powershell
Set-Location 'C:\Users\hmkzx\Documents\holly-travels\frontend'
npm ci
npm run dev
```

Open the local URL printed by Vite (by default, `http://localhost:5173`). Keep
both terminals running while using the app. Press `Ctrl+C` in each terminal to
stop its process.

The frontend currently runs independently and does not yet call the API.

## Useful commands

Run these from `frontend`:

```powershell
npm run build
npm run lint
```

`npm run server` is defined in the root [`package.json`](./package.json). The
frontend commands `npm run dev`, `npm run build`, `npm run lint`, and
`npm run preview` are defined in [`frontend/package.json`](./frontend/package.json).

## Where to change configuration

- [`.gitignore`](./.gitignore): files Git should ignore, including local
  environment files and dependencies.
- [`backend/Program.cs`](./backend/Program.cs): database connection-string key,
  CORS allowed origins, and API services.
- [`backend/Properties/launchSettings.json`](./backend/Properties/launchSettings.json):
  local API URLs and development launch profiles.
- [`backend/backend.csproj`](./backend/backend.csproj): .NET target framework
  and backend package references.
- [`backend/Migrations/`](./backend/Migrations/): database schema migrations.
- [`frontend/vite.config.ts`](./frontend/vite.config.ts): Vite development
  server settings. If you change its port, update the local frontend origin in
  the backend CORS policy in `backend/Program.cs` as well.

The trips API is implemented in
[`backend/Controllers/TripsController.cs`](./backend/Controllers/TripsController.cs):
`GET /api/trips` lists trips and `POST /api/trips` creates one.
