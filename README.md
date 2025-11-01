# Homemade

A modern recipe management application built with Blazor and .NET Aspire, designed for managing recipes, meal planning, and discovering new culinary ideas.

## Overview

Homemade is a microservices-based recipe management platform that provides:

- **Recipe Management**: Store, organise, and browse your recipe collection
- **Recipe Search**: Fast, gRPC-based search API for finding recipes
- **Meal Planning**: Plan your meals ahead of time (coming soon)
- **Recipe Import**: Import recipes from various sources (coming soon)
- **Secure Authentication**: OpenID Connect authentication via Keycloak
- **Modern UI**: Blazor Server with custom TailwindCSS v4 design system

## Technology Stack

- **.NET 9**: Modern C# with nullable reference types
- **Blazor Server**: Interactive server-side rendering
- **TailwindCSS v4**: Custom OKLCH colour palette designed for food applications
- **.NET Aspire**: Service orchestration and observability
- **Keycloak**: Authentication and authorisation
- **PostgreSQL**: Recipe and ingredient storage
- **gRPC**: High-performance inter-service communication
- **Garnet**: Redis-compatible distributed caching
- **Entity Framework Core**: Database access with migrations

## Features

### Current

- ✅ User authentication with Keycloak
- ✅ Custom themed login experience
- ✅ Recipe database with ingredients and instructions
- ✅ gRPC-based search API with streaming
- ✅ Distributed caching
- ✅ Health checks and observability
- ✅ Automatic database migrations

### Planned

- 🔄 Advanced recipe search and filtering
- 🔄 Meal planning calendar
- 🔄 Recipe import from URLs
- 🔄 Recipe ratings and reviews
- 🔄 Shopping list generation
- 🔄 Dietary restriction filtering
- 🔄 AI-powered recipe suggestions (via Ollama)

## Architecture

Homemade uses a microservices architecture orchestrated by .NET Aspire:

```
┌─────────────────┐     ┌──────────────────┐     ┌─────────────────┐
│                 │     │                  │     │                 │
│  Blazor Web UI  │────▶│  Search API      │────▶│   PostgreSQL    │
│  (Homemade.Web) │     │  (gRPC Service)  │     │   (Database)    │
│                 │     │                  │     │                 │
└────────┬────────┘     └─────────┬────────┘     └─────────────────┘
         │                        │
         │                        │
         ▼                        ▼
    ┌─────────────┐         ┌─────────────┐
    │  Keycloak   │         │   Garnet    │
    │  (Auth)     │         │  (Cache)    │
    └─────────────┘         └─────────────┘
```

### Projects

- **Homemade.AppHost**: .NET Aspire orchestrator
- **Homemade.Web**: Blazor web application
- **Homemade.Search**: gRPC search API
- **Homemade.Search.Client**: gRPC client library
- **Homemade.Database**: Entity Framework Core context and entities
- **Homemade.Migrations**: Database migration worker
- **Homemade.ServiceDefaults**: Shared Aspire service configuration

## Getting Started

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed setup instructions.

### Quick Start

1. **Prerequisites**:
   - .NET SDK 9.0.306+
   - Docker Desktop or Podman Desktop
   - Aspire CLI ([installation guide](https://learn.microsoft.com/en-us/dotnet/aspire/cli/install))

2. **Clone and restore**:
   ```bash
   git clone <repository-url>
   cd Homemade
   dotnet restore
   dotnet tool restore
   ```

3. **Configure Keycloak client secret**:
   ```bash
   cd src/Homemade.Web
   dotnet user-secrets set "Keycloak:ClientSecret" "<secret-from-keycloak>"
   ```

4. **Run with Aspire**:
   ```bash
   aspire run
   ```

5. **Access the application**:
   - Web UI: https://localhost:7148
   - Aspire Dashboard: Check console output for URL

## Development

### Building

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Code Formatting

```bash
dotnet format
```

Run before committing to ensure consistent code style.

### Adding Migrations

```bash
cd src/Homemade.Migrations
dotnet ef migrations add <MigrationName> \
  --project ../Homemade.Database \
  --context HomemadeContext
```

## Project Structure

```
Homemade/
├── src/
│   ├── Homemade.AppHost/          # Aspire orchestrator
│   ├── Homemade.Web/              # Blazor web application
│   ├── Homemade.Search/           # gRPC search API
│   ├── Homemade.Search.Client/    # gRPC client library
│   ├── Homemade.Database/         # EF Core context & entities
│   ├── Homemade.Migrations/       # Database migrations
│   └── Homemade.ServiceDefaults/  # Shared Aspire defaults
├── tests/                         # Test projects
├── keycloak/
│   ├── themes/homemade/           # Custom Keycloak theme
│   └── realms/realm-export.json   # Realm configuration
├── .config/                       # Tool manifests
├── CLAUDE.md                      # Project guidelines
└── CONTRIBUTING.md                # Development setup guide
```

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for:

- Development environment setup
- Configuration requirements
- Coding standards
- Submission guidelines

## Licence

[MIT](LICENSE.md)

## Acknowledgements

Built with:
- [.NET Aspire](https://learn.microsoft.com/dotnet/aspire/)
- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- [Keycloak](https://www.keycloak.org/)
- [TailwindCSS](https://tailwindcss.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [gRPC](https://grpc.io/)
