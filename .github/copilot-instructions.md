# TeachReach-API Coding Agent Instructions

## Project Overview
**TeachReach-API** is an ASP.NET Core 6.0 microservice for a tutoring marketplace connecting teachers and students. It uses **clean architecture** with four layers: API controllers, Application services, Domain entities, and Infrastructure (repositories & database).

## Architecture & Layer Patterns

### Four-Layer Structure
- **TeachReach.API**: Controllers handling HTTP requests via REST endpoints (`/api/[controller]`)
- **TeachReach.Application**: Business logic services and request/response models
- **TeachReach.Domain**: Entity models (Teacher, Student, Subject, Session, Review) extending `BaseEntity`
- **TeachReach.Infrastructure**: Database context, generic/custom repositories, EF Core configurations

### Data Flow
Controllers → Services (ITeacherService, etc.) → Repositories (IGenericRepository + custom) → TeachReachDbContext → SQL Server

### Generic + Custom Repository Pattern
- **IGenericRepository<T>**: Reusable CRUD methods (GetById, GetAll, Create, Update, Delete, Count)
- **Custom Repositories** (ITeacherRepository, ISessionRepository, etc.): Domain-specific queries like `GetTeacherWithSubjectsAndReviewsById()` or `GetAllTeachersWithSubjectsAndReviews()`
- Always inject both generic AND custom repository where needed (e.g., TeacherService uses both)

## Entity Relationships & Conventions

### Core Entities
- **Teacher**: FirstName, LastName, Email, PhoneNumber, HourlyRate, Experience, DateOfBirth, ProfilePictureUrl, Password (hashed with BCrypt), Bio, City
- **Student**: FirstName, LastName, Email, Password (BCrypt), minimal profile
- **Subject**: Name, Description (many-to-many with Teachers - define in OnModelCreating if needed)
- **Session**: Booking between Teacher and Student
- **Review**: TeacherId + StudentId with Rating (1-5) and Comment

### Key Patterns
- All entities inherit from `BaseEntity` (provides `Id`)
- Relationships declared as `List<T>` properties with null-coalescing initialization (`= new List<T>()`)
- Foreign keys are nullable in migrations (e.g., `TeacherId`, `StudentId` on Review)
- Circular reference handling: JSON serialization ignores cycles via `ReferenceHandler.IgnoreCycles` in Program.cs

## Service & Controller Conventions

### Service Methods
- Methods are **async/await**
- Return `Task<T>` or `Task<SaveResponse>` for mutations
- Throw exceptions for validation (checked in controllers with try-catch)
- Example pattern (TeacherService.Update):
  ```csharp
  var existing = await _repository.GetById(id);
  if (existing == null) throw new Exception("Entity not found");
  existing.Property = newValue; // Manual property mapping
  await _repository.Update(existing);
  return new SaveResponse() { Success = true };
  ```

### Controller Patterns
- Routes: `[HttpGet("endpoint")]`, `[HttpPost("endpoint")]`, `[HttpPut("endpoint/{id}")]`, `[HttpDelete("endpoint/{id}")]`
- Always wrap business logic in try-catch returning `StatusCode(500, ex.Message)` on failure
- Use `[FromRoute]` for URL parameters, `[FromBody]` for request bodies
- Return `Ok(response)`, `BadRequest()`, or `ActionResult<T>`

## Build & Run Commands

```bash
# Build solution
dotnet build TeachReach.sln

# Run with watch mode (auto-restarts on file changes)
dotnet watch run --project TeachReach.sln

# Publish for deployment
dotnet publish TeachReach.sln
```

**Tasks in .vscode/tasks.json**: Use VS Code's task runner (Ctrl+Shift+B) to execute build/publish/watch tasks.

## Database & Migrations

- **Database**: SQL Server, connection string in `appsettings.json` ("DefaultConnection")
- **DbContext**: [TeachReachDbContext.cs](../TeachReach.Infrastructure/TeachReachDbContext.cs) with DbSet declarations
- **Initial migration**: [20240311130314_Initial.cs](../Migrations/20240311130314_Initial.cs)
- Add migrations via EF Core tools (requires Microsoft.EntityFrameworkCore.Tools)

## Dependency Injection (Program.cs)

All services & repositories are registered in Program.cs:
```csharp
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
```
- Scoped lifetime for repositories & services (one per HTTP request)
- AutoMapper singleton configured via `MapperConfigurations.CreateMapper()`
- CORS configured for `http://localhost:3000` (frontend dev server)

## Key External Dependencies

- **EntityFramework Core 6.0.25**: ORM with SQL Server provider
- **AutoMapper 12.0.1**: DTO/Entity mapping
- **BCrypt.Net 4.0.3**: Password hashing (imported but confirm usage in auth services)
- **MediatR 12.2.0**: CQRS pattern (installed but not heavily used currently)
- **Swagger/Swashbuckle**: API documentation on `/swagger` (dev only)

## Request/Response Models

- **SaveResponse**: Standard mutation response with `Success` boolean
- **Request models**: Located in `TeachReach.Application/RequestModels/` (e.g., ReviewRequestModels, TeacherRequestModels)
- **Response models**: Located in `TeachReach.Application/ResponseModels/`
- Use AutoMapper to convert entity ↔ DTO in services

## Important Notes

- **Nullable reference types enabled** (`<Nullable>enable</Nullable>` in .csproj) - mark optional props with `?`
- **Identity integration**: Project includes Microsoft.AspNetCore.Identity but StudentAuthenticationService is the primary auth implementation
- **Docker support**: Windows containers (DockerDefaultTargetOS=Windows, Dockerfile present)
- **Manual property mapping**: Update methods manually assign properties rather than using AutoMapper (see TeacherService.Update pattern)
- **No validation middleware**: Validation happens in services/controllers—consider adding FluentValidation for complex rules

## When Adding Features

1. Create entity in Domain/Entities if needed
2. Add DbSet to TeachReachDbContext
3. Create migration: `dotnet ef migrations add FeatureName`
4. Create repository interface in Infrastructure/Interfaces
5. Implement repository in Infrastructure/Implementation
6. Create service interface in Application/Services/Interfaces
7. Implement service with dependency injection of both generic and custom repo
8. Create request/response models in Application/RequestModels & ResponseModels
9. Add controller in API/Controllers with try-catch error handling
10. Register in Program.cs

## Testing & Debugging

- Use Swagger UI (`/swagger` when running in Development) to test endpoints
- Debug mode: Run `dotnet watch run` and set breakpoints in Visual Studio Code
- Check [appsettings.Development.json](../appsettings.Development.json) for dev-specific configs
