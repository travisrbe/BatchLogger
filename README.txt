This is an API created as an exercise to explore what is new or different in ASP.NET Core 8. It provides the backend for a React SPA frontend aimed at helping home winemakers plan and log batches of wine, and optionally share their records.

This repository has been made public as a code sample, and is not the active repository for this project, so pardon the sparse commit history. This is a work in progress.

The active repository is hosted in Team Foundation Version Control via Azure DevOps, which I am also leveraging to track development tasks and run build pipelines.

As this is a code sample, I do not expect anyone to attempt to run this API, but there is no reason you cannot!

To run this project:

1. Download the code from GitHub.
2. Install your preferred version of Sql Server. I used Express.
3. In appsettings.json, update the ConnectionStrings:Development value to point to your server, if necessary.
4. The first time you build, it should attempt to restore NuGet packages. If it does not, right click on the Solution file and select "Restore Nuget Packages."
5. Open the Package Manager Console, point the Default project to "Persistence" and run `Update-Database` to generate tables. There is currently no script to generate seed data.
  5.1. If you have problems with this, you can delete all of the migrations, then run `Add-Migration InitialCreate`, then run `Update-Database`. This should work but I haven't tested it.
6. Run the application. You will have to use Postman or Swagger to hit the account/register and account/login endpoints. Be sure you opt to use Browser Cookies - it's not currently configured to use Bearer Tokens. 
  6.1 All other controller actions require authorization.

Organization:

This API roughly follows the Onion/Clean Architecture design pattern, using dependency injection, and is relatively typical of how I like to structure an API.

I have opted for clean Controllers. Controllers call Services, and the most business logic performed by a Controller is usually to pass the current user id from a private member. Controllers only know about DTOs.

Services perform the business logic. Here DTOs are mapped to database Entities, and any work or required checks are performed. The Service layer calls down to the Repository layer to persist changes to the database.

The Repository layer handles the minutiae of retrieving data and updating the database.

Core:
  Contracts: Here you will find DTOs.
  Domain: Entities, Exceptions, Repository Interfaces
  Service.Abstractions: Service Interfaces
  Services: Unsurprisingly, services.
Infrastructure:
  Persistence: Configurations (FluentApi), Migrations, and Repository classes.
  Presentation: Controllers
ApiServer:
  Program.cs, Service Extensions, Middleware, and json properties.