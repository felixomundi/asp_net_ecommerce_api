Create New Project
- dotnet new web -o NameOfProject -f net8.0
Run Project
- dotnet run
Add Swagger API
- dotnet add package Swashbuckle.AspNetCore --version 6.5.0
Watch by Swagger 
- dotnet watch run
## Start on Specified Swagger Port
- http://localhost:{PORT}/swagger.
## Add EF to project
- dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0


## git commit -m "added mvc pattern to minimal api"
return types/IActionResult

## add mysql entity framework
- dotnet add package MySql.EntityFrameworkCore --version 8.0.2
## add mysql connector
- dotnet add package MySqlConnector


########################
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0


check project errors
- dotnet build
## create migrations
- dotnet ef migrations add init
## migrate 
- dotnet ef database update
## remove migrations 
- dotnet ef migrations remove