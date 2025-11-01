# Contributing to Homemade

Thank you for your interest in contributing to Homemade! This guide will help you get your development environment set up and running.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Development Environment Setup](#development-environment-setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Development Workflow](#development-workflow)
- [Project Guidelines](#project-guidelines)
- [Troubleshooting](#troubleshooting)

## Prerequisites

Before you begin, ensure you have the following installed:

### Required

- **.NET SDK 9.0.306 or later**: [Download here](https://dotnet.microsoft.com/download)
  ```bash
  dotnet --version  # Should show 9.0.306 or higher
  ```

- **Container Runtime**: Required for running PostgreSQL, Keycloak, Garnet, and other services
  - [Docker Desktop](https://docs.docker.com/desktop/) or [Podman Desktop](https://podman-desktop.io/)

- **Aspire CLI**: Required for orchestrating services
  - See [Aspire CLI installation guide](https://learn.microsoft.com/en-us/dotnet/aspire/cli/install)

### Recommended

- **IDE**:
  - [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.12+) with ASP.NET and web development workload
  - [JetBrains Rider](https://www.jetbrains.com/rider/) (2024.3+)
  - [Visual Studio Code](https://code.visualstudio.com/) with C# Dev Kit extension

- **Git**: For version control

## Development Environment Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd Homemade
```

### 2. Restore Dependencies

Restore NuGet packages and local tools:

```bash
# Restore NuGet packages
dotnet restore

# Restore local .NET tools (EF Core CLI, gRPC tools)
dotnet tool restore
```

Verify tools are installed:
```bash
dotnet tool list
# Should show:
# - dotnet-ef (9.0.9)
# - dotnet-grpc (2.71.0)
```

### 3. Start Container Runtime

Ensure Docker Desktop or Podman Desktop is running. Aspire will automatically pull and start the required container images:

- **postgres:latest**: Database server
- **quay.io/keycloak/keycloak:26.3.5**: Authentication server
- **ghcr.io/microsoft/garnet:latest**: Redis-compatible cache
- **ollama/ollama:latest**: AI/LLM server
- **axllent/mailpit:latest**: Email testing

## Configuration

### User Secrets

Homemade uses .NET User Secrets for sensitive configuration. You'll need to configure the Keycloak client secret.

#### Step 1: Initial Run to Start Keycloak

First, start the application once to get Keycloak running:

```bash
aspire run
```

Wait for all services to start. The Aspire Dashboard will open automatically in your browser.

#### Step 2: Obtain Keycloak Client Secret

1. In the Aspire Dashboard, find the **keycloak** service and click its endpoint URL (usually `http://localhost:8080` or similar)

2. Access the Keycloak Admin Console:
   - Click "Administration Console"
   - Username: `admin`
   - Password: Check the Aspire Dashboard logs for keycloak or refer to the realm export

3. Navigate to the Homemade realm:
   - In the top-left dropdown, select **Homemade** realm

4. Get the client secret:
   - Click **Clients** in the left menu
   - Find and click **homemade.web**
   - Go to the **Credentials** tab
   - Copy the **Client Secret** value

#### Step 3: Configure User Secrets

Set the client secret for the Web project:

```bash
cd src/Homemade.Web
dotnet user-secrets set "Keycloak:ClientSecret" "<paste-client-secret-here>"
```

Example:
```bash
dotnet user-secrets set "Keycloak:ClientSecret" "Fbwc07Z1tEWGHPgmyGKG4E"
```

Verify the secret is set:
```bash
dotnet user-secrets list
```

You should see:
```
Keycloak:ClientSecret = <your-secret>
```

### Application Settings

Application settings are managed through `appsettings.json` and `appsettings.Development.json` files. No manual configuration is needed for:

- Database connection strings (managed by Aspire)
- Service discovery endpoints (managed by Aspire)
- Cache connections (managed by Aspire)

## Running the Application

### Option 1: Using Aspire (Recommended)

This is the easiest way to run the entire application stack:

```bash
aspire run
```

Or:

```bash
dotnet run --project src/Homemade.AppHost
```

This will:
1. Start all required Docker containers
2. Run database migrations automatically
3. Start the Search API
4. Start the Web application
5. Open the Aspire Dashboard for monitoring

### Option 2: Using Your IDE

#### Visual Studio 2022
1. Open the solution
2. Set **Homemade.AppHost** as the startup project
3. Press F5 or click Start

#### JetBrains Rider
1. Open the solution
2. Select **Homemade.AppHost** run configuration
3. Click Run or Debug

#### Visual Studio Code
1. Open the workspace
2. Use the Run and Debug panel
3. Select **Launch Homemade.AppHost**

### Accessing the Application

After starting:

- **Web Application**: https://localhost:7148 or http://localhost:5110
- **Aspire Dashboard**: Check console output for the URL (usually http://localhost:15888)
- **Search API**: Not directly accessible (gRPC service)
- **Keycloak Admin Console**: Check Aspire Dashboard for the endpoint
- **MailPit UI**: Check Aspire Dashboard for the endpoint

### Creating a Test User

To log in to the application, you'll need a Keycloak user:

1. Access the Keycloak Admin Console (see Aspire Dashboard)
2. Ensure you're in the **Homemade** realm
3. Click **Users** → **Add user**
4. Fill in the form:
   - **Username**: Choose a username (e.g., `testuser`)
   - **Email**: Add an email address
   - **First name** and **Last name**: Optional
5. Click **Create**
6. Go to the **Credentials** tab
7. Click **Set password**
8. Enter a password and disable **Temporary**
9. Click **Save**

Now you can log in to the Web application with these credentials.

## Development Workflow

### Building

Build all projects:
```bash
dotnet build
```

Build with Release configuration:
```bash
dotnet build -c Release
```

Build a specific project:
```bash
dotnet build src/Homemade.Web
```

### Running Tests

Run all tests:
```bash
dotnet test
```

Run tests with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

Run specific test class:
```bash
dotnet test --filter "FullyQualifiedName~Namespace.ClassName"
```

Run specific test method:
```bash
dotnet test --filter "Name~TestMethodName"
```

### Code Formatting

**IMPORTANT**: Always run the code formatter before committing:

```bash
dotnet format
```

This ensures consistent coding style across the project.

### Database Migrations

Migrations are **automatically applied** when you run the application via the `Homemade.Migrations` worker service.

#### Creating a New Migration

```bash
cd src/Homemade.Migrations
dotnet ef migrations add <MigrationName> \
  --project ../Homemade.Database \
  --context HomemadeContext
```

Example:
```bash
dotnet ef migrations add AddRecipeRatings \
  --project ../Homemade.Database \
  --context HomemadeContext
```

## Project Guidelines

### Language and Localisation

- Use **Oxford English** (British English) for all user-facing text, labels, messages, and UI content
- Examples: "colour" not "color", "organise" not "organize"
- Code identifiers should follow C# conventions (American English is acceptable)

### Code Style

- C# latest version with nullable reference types enabled
- Follow Microsoft's C# coding conventions
- Run `dotnet format` before committing
- Write XML documentation for public APIs
- Use meaningful variable and method names

### Blazor Component Organisation

Components should be organised under `Components/Shared/`:
```
Components/
├── Pages/              # Routable pages
├── Layout/             # Layout components
└── Shared/             # Reusable components
    ├── Forms/          # Form-related components
    └── ...             # Other categorised components
```

### Commit Guidelines

- Write clear, descriptive commit messages
- Use conventional commit format:
  - `feat:` New feature
  - `fix:` Bug fix
  - `docs:` Documentation changes
  - `style:` Code style changes (formatting)
  - `refactor:` Code refactoring
  - `test:` Adding tests
  - `chore:` Maintenance tasks

Example:
```
feat: add recipe rating system

- Add Rating entity to database
- Create rating API endpoint
- Add rating UI component to recipe detail page
```

### Pull Request Process

1. Create a feature branch from `main`
2. Make your changes
3. Run tests and ensure they pass
4. Run `dotnet format`
5. Commit your changes
6. Push to your fork
7. Create a Pull Request with a clear description

## Additional Resources

- [.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire/)
- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [Keycloak Documentation](https://www.keycloak.org/documentation)
- [gRPC Documentation](https://grpc.io/docs/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)
- [TailwindCSS Documentation](https://tailwindcss.com/docs)

## Licence

By contributing, you agree that your contributions will be licensed under the same licence as the project.
