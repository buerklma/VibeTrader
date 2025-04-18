# GitHub Copilot Instructions for VibeTrader

## Project Overview
VibeTrader is a stock trading alert application consisting of:
- ASP.NET Core REST API backend
- Blazor WebAssembly client frontend

## Architecture Guidelines

### Backend (ASP.NET Core REST API)
- Follow Clean Architecture principles with clear separation of concerns
- Implement vertical slice architecture using MediatR for request/response patterns
- Use repository pattern for data access
- Implement proper exception handling and logging
- Follow REST API best practices with appropriate status codes and responses
- Use dependency injection for all services
- Implement authentication and authorization using JWT tokens
- Use Entity Framework Core for database access
- Implement validations using FluentValidation
- Include comprehensive API documentation with Swagger

### Frontend (Blazor WebAssembly)
- Follow component-based architecture
- Implement state management using Fluxor or similar
- Create reusable UI components
- Use proper form validation
- Implement client-side error handling
- Follow responsive design principles
- Use Blazor authentication with JWT tokens

## Coding Standards
- Follow Microsoft's C# coding conventions
- Use async/await pattern for asynchronous operations
- Write SOLID-compliant code
- Include XML documentation for public APIs
- Use meaningful variable and method names
- Implement consistent error handling
- Write unit tests for all business logic
- Use nullable reference types

## Project Structure
```
VibeTrader/
├── src/
│   ├── API/                      # ASP.NET Core API project
│   │   ├── Controllers/          # API endpoints
│   │   ├── Filters/              # Action filters
│   │   ├── Middlewares/          # Custom middlewares
│   │   ├── Program.cs            # Application entry point
│   │   └── appsettings.json      # Configuration
│   ├── Application/              # Application logic layer
│   │   ├── Commands/             # Write operations
│   │   ├── Queries/              # Read operations
│   │   ├── Validators/           # Request validation
│   │   ├── Mappings/             # Object mapping profiles
│   │   └── Interfaces/           # Abstraction interfaces
│   ├── Domain/                   # Business domain layer
│   │   ├── Entities/             # Domain entities
│   │   ├── Events/               # Domain events
│   │   ├── Exceptions/           # Domain specific exceptions
│   │   └── ValueObjects/         # Domain value objects
│   ├── Infrastructure/           # Infrastructure layer
│   │   ├── Data/                 # Data access
│   │   ├── Services/             # External services
│   │   └── Configuration/        # Infrastructure configuration
│   └── Client/                   # Blazor WebAssembly project
│       ├── Pages/                # Blazor pages
│       ├── Components/           # Reusable UI components
│       ├── Services/             # Client services
│       ├── State/                # State management
│       ├── Shared/               # Shared layouts
│       └── wwwroot/              # Static files
├── tests/
│   ├── API.Tests/                # API tests
│   ├── Application.Tests/        # Application layer tests
│   ├── Domain.Tests/             # Domain layer tests
│   ├── Infrastructure.Tests/     # Infrastructure tests
│   └── Client.Tests/             # Blazor client tests
├── docs/                         # Documentation
└── .github/                      # GitHub specific files
```

## Feature Implementation Guidelines

### Stock Alert Feature
Based on requirement #001:
- Create full CRUD operations for stock alerts
- Implementation should include validation, error handling, and notifications
- Ensure proper separation between API and client responsibilities
- Include unit tests for alert business logic

### Sample Code Patterns

#### API Controller
When generating API controllers, follow this pattern:
```csharp
[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AlertsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<AlertDto>>> GetAlerts(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAlertsQuery(), cancellationToken);
        return Ok(result);
    }

    // Additional endpoints following similar pattern
}
```

#### Domain Entity
When generating domain entities, follow this pattern:
```csharp
public class Alert
{
    public Guid Id { get; private set; }
    public string Symbol { get; private set; }
    public decimal TargetPrice { get; private set; }
    public AlertType Type { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? TriggeredOn { get; private set; }
    public bool IsActive { get; private set; }
    
    // Constructors, methods, and behavior
}
```

#### Blazor Component
When generating Blazor components, follow this pattern:
```csharp
@inject IAlertService AlertService
@inject IState<AlertsState> AlertsState

<div class="alert-container">
    @if (AlertsState.Value.IsLoading)
    {
        <LoadingComponent />
    }
    else
    {
        <AlertList Alerts="AlertsState.Value.Alerts" OnDelete="DeleteAlert" />
    }
</div>

@code {
    protected override async Task OnInitializedAsync()
    {
        await AlertService.LoadAlertsAsync();
    }

    private async Task DeleteAlert(Guid alertId)
    {
        await AlertService.DeleteAlertAsync(alertId);
    }
}
```

## Testing Guidelines
- Use xUnit for testing
- Follow Arrange-Act-Assert pattern
- Use NSubstitute for mocking dependencies
- Aim for high test coverage of business logic