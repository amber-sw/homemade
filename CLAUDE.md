# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Homemade is a recipe management application built with Blazor and .NET Aspire.

### Technology Stack

- **Frontend**: Blazor with TailwindCSS v4 (custom color palette only, no default colors)
- **Orchestration**: .NET Aspire
- **Authentication**: Keycloak
- **Database**: PostgreSQL
- **Distributed Cache**: Garnet (Redis-compatible)
- **Messaging**: RabbitMQ

## Solution Structure

```
/src/          # All application projects
/tests/        # All test projects
```

- `Directory.Build.props` - Centralized MSBuild properties that apply to all projects in the solution

### Blazor Component Organization

Blazor components should be organized under `Components/Shared/` (with additional subfolders as appropriate):
- `Components/` - Auto-generated Blazor folder
- `Components/Shared/` - Custom reusable components
- `Components/Shared/Forms/` - Form-related components (example subfolder)
- `Components/Shared/Layout/` - Layout components (example subfolder)

## Common Commands

### Building
```bash
dotnet build
dotnet build -c Release
```

### Running Tests
```bash
dotnet test
dotnet test --filter "FullyQualifiedName~Namespace.ClassName"  # Run specific test class
dotnet test --filter "Name~TestMethodName"                      # Run specific test method
```

### Running Projects
```bash
dotnet run --project <ProjectPath>
```
or
```bash
aspire run
```

### Adding New Projects
```bash
dotnet new <template> -n <ProjectName> -o <OutputPath>    # Create new project
dotnet sln add <ProjectPath>                              # Add project to solution
```

**Important**: Always use `dotnet sln add` to add projects to the solution (never edit solution file manually).

### Package Management
```bash
dotnet package add <PackageName> --project <ProjectPath>   # Add NuGet package to project
dotnet restore                                             # Restore all packages
```

## Development Workflow

### Making Code Changes
Always verify compilation after making larger code changes:
```bash
dotnet build
```

### Package Management Policy
- **Always use the `dotnet` CLI** to add packages (never edit `.csproj` manually for package additions)
- Use `dotnet package add <PackageName> --project <ProjectPath>` to add NuGet packages
- This ensures proper package references and version resolution

## Development Notes

### Design Philosophy & Styling

The design system prioritises:
- **Warmth and approachability** - Using warm colours associated with home cooking
- **Clarity and simplicity** - Clean interfaces that don't overwhelm
- **Efficiency** - Quick access to ingredient search and recipe browsing
- **Visual feedback** - Clear indication of user actions and state

#### TailwindCSS v4
- Use **TailwindCSS v4** for all styling
- **Do not use default Tailwind colours** - only use the custom colour palette defined in `src/Homemade.Web/Styles/app.css`
- Colours are defined using OKLCH colour space for perceptually uniform colours, ensuring consistent visual weight and better accessibility

#### Colour Palette

All colours are defined in `src/Homemade.Web/Styles/app.css` using TailwindCSS v4's `@theme` directive.

**Primary (Warm Orange)**
- Purpose: Main brand colour, CTAs, active states, interactive elements
- The warm orange evokes appetite, home cooking, and comfort
- Common usage:
  - `bg-primary-500` - Primary buttons, active states
  - `bg-primary-600` - Hover states for primary actions
  - `bg-primary-100` - Selected ingredient tags background
  - `text-primary-800` - Tag text colour
  - `bg-primary-50` - Page background gradient start
  - `from-primary-200 to-primary-300` - Recipe card placeholder gradients
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

**Secondary (Soft Cream)**
- Purpose: Complementary neutral with warmth, secondary UI elements
- Provides subtle backgrounds and supports the primary colour scheme
- Common usage:
  - `bg-secondary-100` - Subtle background sections
  - `text-secondary-700` - Secondary text elements
  - `bg-secondary-50` - Very light backgrounds
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

**Accent (Fresh Green)**
- Purpose: Highlighting fresh ingredients, herbs, and special features
- Use sparingly for emphasis on ingredient-related content
- Common usage:
  - `bg-accent-500` - Accent buttons or badges
  - `border-accent-400` - Highlighting fresh ingredient features
  - `text-accent-700` - Accent text for ingredient callouts
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

**Semantic Colours**

*Success (Leafy Green)*
- Purpose: Positive feedback, successful actions
- Common usage:
  - `bg-success-500` - Success banners
  - `text-success-700` - Success messages
  - Examples: "Recipe saved", "Ingredient added"
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

*Warning (Golden Yellow)*
- Purpose: Cautionary messages, important notes
- Common usage:
  - `bg-warning-100` - Warning backgrounds
  - `text-warning-700` - Warning text
  - Examples: "Missing ingredients", "Recipe notes"
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

*Error (Rich Red)*
- Purpose: Error states, destructive actions, validation failures
- Common usage:
  - `bg-error-500` - Error alerts
  - `text-error-700` - Error messages
  - `border-error-300` - Invalid input borders
  - Examples: "Search failed", "Delete recipe"
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

**Neutral (Warm Grays)**
- Purpose: Text, borders, backgrounds - slightly warm to complement the orange theme
- The neutral palette has a subtle warm tint to harmonise with the food-focused design
- Common usage:
  - White (`#ffffff`) - Card backgrounds, input backgrounds
  - `text-neutral-900` - Primary headings
  - `text-neutral-800` - Secondary headings
  - `text-neutral-700` - Suggestion button text
  - `text-neutral-600` - Body text, metadata
  - `border-neutral-200` - Input borders
  - `bg-neutral-100` - Ingredient tags, suggestion buttons
  - `bg-neutral-300` - Disabled button state
  - `bg-neutral-50` - Subtle background sections
- Available shades: 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950

### Localisation and Language
- Use **Oxford English** (British English) for all user-facing text, labels, messages, and UI content
- Examples: "colour" not "color", "organise" not "organize", "favour" not "favor"
- This does not apply to code identifiers, which should follow C# conventions

### Build Configuration
`Directory.Build.props` is the central location for shared MSBuild properties. Any properties set here will automatically apply to all projects in the solution, making it ideal for:
- Target framework versions
- Language versions (C# version)
- Nullable reference type settings
- Code analysis settings
- Package version management