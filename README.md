# NotifyHub

## 🚀 Project Overview
NotifyHub is a **real‑time notification platform** built on **.NET 9**.  
It follows **clean‑architecture** principles and exposes a fully documented REST API (Swagger) for sending notifications via Email, SMS, or FCM.  

## 📦 Key Features
- **Domain‑Driven Design** – Core entities and domain events isolated in the `Domain` layer.  
- **CQRS & MediatR** – Commands & queries separated for clear responsibilities.  
- **FluentValidation** – Request validation with automatic pipeline behavior.  
- **AutoMapper** – Object‑object mapping between DTOs and domain models.  
- **EF Core (SQL Server)** – Design‑time `DbContextFactory`, migrations, and a repository pattern.  
- **Hangfire** – Background job processing & reliable retry logic.  
- **SignalR Hub** – Real‑time push of notifications to connected clients.  
- **Swagger UI** – Auto‑generated API docs, opened on launch.  
- **Docker‑ready** – `Dockerfile` and `docker‑compose.yml` provided for containerised deployment.  

## 🛠️ Tech Stack
| Layer          | Technology / Package                              |
|----------------|---------------------------------------------------|
| API            | ASP.NET Core Minimal API, Swagger, HTTPS          |
| Application    | MediatR, FluentValidation, AutoMapper            |
| Domain         | Plain C# classes, Enums, Domain Events            |
| Infrastructure | EF Core 9, Hangfire.SqlServer, SignalR, AutoMapper.Extensions.Microsoft.DependencyInjection |
| CI/CD          | GitHub Actions (example workflow provided)        |

## 📁 Repository Structure
