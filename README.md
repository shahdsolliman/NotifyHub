# NotifyHub

**GitHub Description:**
A .NET 9 real‑time notification hub built with clean architecture, exposing API endpoints via Swagger.

**CV Description:**
Developed a full‑stack notification platform using C# and .NET 9, employing clean‑architecture principles. The solution includes:

- **Domain Layer:** Core entities and domain events.
- **Application Layer:** MediatR for CQRS, FluentValidation, AutoMapper for object mapping.
- **Infrastructure Layer:** EF Core (SQL Server) with design‑time DbContext factory, Hangfire for background job processing, and dependency injection.
- **API Layer:** ASP.NET Core minimal API, Swagger UI auto‑opened, HTTPS configuration.

Implemented Docker‑ready configuration, CI/CD scripts, and comprehensive logging.

## GitHub Upload Commands
```bash
cd e:/NotifyHub/src
git init
git add .
git commit -m "Initial commit - NotifyHub project"
git remote add origin https://github.com/shahdsolliman/NotifyHub.git
git branch -M main
git push -u origin main
```
