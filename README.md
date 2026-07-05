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

The project follows a clean‑architecture layout:

```text
NotifyHub/
├─ src/
│  ├─ NotifyHub.sln                # Solution file
│  ├─ NotifyHub.Api/                # ASP.NET Core Minimal API project
│  │   ├─ Program.cs
│  │   └─ ... (controllers, etc.)
│  ├─ NotifyHub.Application/        # Application layer (CQRS, MediatR, etc.)
│  ├─ NotifyHub.Domain/             # Domain entities, enums, events
│  ├─ NotifyHub.Infrastructure/     # EF Core, Hangfire, SignalR implementations
│  ├─ README.md                     # This README
│  └─ .gitignore                    # Git ignore file
├─ docker-compose.yml                # Docker compose for multi‑container dev
└─ Dockerfile                         # Dockerfile for API container
```

## 📦 Installation

```bash
git clone https://github.com/shahdsolliman/NotifyHub.git
cd NotifyHub/src
dotnet restore
dotnet build
```

## ▶️ Running

```bash
dotnet run --project NotifyHub.Api/NotifyHub.Api.csproj
# API will be available at https://localhost:7097 (Swagger UI opens automatically)
```

## 🐳 Docker

```bash
docker compose up --build
```

## 📄 License

MIT License (or appropriate license).

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

The project follows a clean‑architecture layout:

```text
NotifyHub/
├─ src/
│  ├─ NotifyHub.sln                # Solution file
│  ├─ NotifyHub.Api/                # ASP.NET Core Minimal API project
│  │   ├─ Program.cs
│  │   └─ ... (controllers, etc.)
│  ├─ NotifyHub.Application/        # Application layer (CQRS, MediatR, etc.)
│  ├─ NotifyHub.Domain/             # Domain entities, enums, events
│  ├─ NotifyHub.Infrastructure/     # EF Core, Hangfire, SignalR implementations
│  ├─ README.md                     # This README
│  └─ .gitignore                    # Git ignore file
├─ docker-compose.yml                # Docker compose for multi‑container dev
└─ Dockerfile                         # Dockerfile for API container
```

## 📦 Installation

```bash
git clone https://github.com/shahdsolliman/NotifyHub.git
cd NotifyHub/src
dotnet restore
dotnet build
```

## ▶️ Running

```bash
dotnet run --project NotifyHub.Api/NotifyHub.Api.csproj
# API will be available at https://localhost:7097 (Swagger UI opens automatically)
```

## 🐳 Docker

```bash
docker compose up --build
```


