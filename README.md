# AngularCoreApp
Angular 4 and .Net Core 2.0 Architecture

Step 1:
dotnet restore 
 
Step 2: create migration (from Web folder CLI):

dotnet ef migrations add InitialApplicationModel --context applicationdbcontext -p ../Application.Infrastructure/Application.Infrastructure.csproj -s Application.Web.csproj -o Data/Migrations

dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../Application.Infrastructure/Application.Infrastructure.csproj -s Application.Web.csproj -o Identity/Migrations
 
Step 3: add migrations (from Web folder CLI)
dotnet ef database update -c applicationdbcontext -p ../Application.Infrastructure/Application.Infrastructure.csproj -s Application.Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../Application.Infrastructure/Application.Infrastructure.csproj -s Application.Web.csproj
