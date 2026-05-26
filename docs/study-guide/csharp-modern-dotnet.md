# Modern C# / .NET Study Guide

## async / await

### async
Marks a method as asynchronous.

### await
Asynchronously waits without blocking the thread.

### Task
Represents asynchronous work.

```csharp
public async Task<Order> GetOrderAsync(int id)
{
    return await _repo.GetOrderAsync(id);
}
```

---

## Why async matters
Async improves scalability by freeing threads during I/O waits.

Common scenarios:
- database calls
- HTTP requests
- cloud integrations
- queues
- file IO

---

## var
`var` allows implicit typing when the compiler can infer the type.

```csharp
var orders = new List<Order>();
```

---

## nullable reference types
Nullable reference types help prevent null reference exceptions.

```csharp
string? name = null;
```

---

## records
Records are immutable reference types commonly used for DTOs/value objects.

```csharp
public record UserDto(int Id, string Name);
```

Benefits:
- immutability
- value equality
- cleaner DTO syntax

---

## init setters
Allows object initialization while remaining immutable afterward.

```csharp
public class User
{
    public string Name { get; init; }
}
```

---

## expression-bodied members
Shorter syntax for simple members.

```csharp
public string FullName => FirstName + " " + LastName;
```

---

## pattern matching
Modern C# supports advanced pattern matching.

```csharp
if (obj is Order order)
{
    Console.WriteLine(order.Id);
}
```

---

## switch expressions
Cleaner conditional logic.

```csharp
var status = code switch
{
    200 => "Success",
    404 => "Missing",
    _ => "Unknown"
};
```

---

## LINQ
LINQ provides declarative querying for collections and databases.

```csharp
var activeUsers = users.Where(x => x.IsActive);
```

---

## IEnumerable vs IQueryable

### IEnumerable
Runs in memory.

### IQueryable
Translates to database queries.

---

## Dependency Injection
ASP.NET Core has built-in dependency injection.

```csharp
builder.Services.AddScoped<IOrderService, OrderService>();
```

---

## Middleware
Middleware intercepts requests/responses.

Common uses:
- auth
- logging
- exception handling
- metrics

---

## Minimal APIs
Modern lightweight API syntax introduced in newer .NET versions.

```csharp
app.MapGet("/orders", () => orders);
```

---

## Background Services
Hosted background processing within ASP.NET Core.

```csharp
public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessQueue();
        }
    }
}
```

---

## CancellationToken
Used to cancel async operations gracefully.

Important for:
- APIs
- queues
- long-running work
- shutdown handling

---

## IAsyncEnumerable
Supports asynchronous streaming.

```csharp
await foreach(var item in stream)
{
    Console.WriteLine(item);
}
```

---

## ConfigureAwait(false)
Avoids resuming on original synchronization context.

Mostly important in library code.

---

## Span<T>
High-performance memory abstraction reducing allocations.

Useful in:
- parsing
- serialization
- high-throughput systems

---

## Memory leaks in .NET
Common causes:
- static references
- event subscriptions
- undisposed objects
- long-lived caches

---

## IDisposable
Used for explicit cleanup of unmanaged resources.

```csharp
using var connection = new SqlConnection(cs);
```

---

## Garbage Collection
.NET automatically manages memory cleanup.

Generations:
- Gen 0
- Gen 1
- Gen 2

---

## Boxing / Unboxing

### Boxing
Value type -> object.

### Unboxing
Object -> value type.

---

## Reflection
Allows runtime inspection of types and metadata.

Used in:
- DI frameworks
- serializers
- ORMs
- plugins

---

## Attributes
Metadata annotations.

```csharp
[ApiController]
public class OrdersController : ControllerBase
{
}
```

---

## Generic Types
Reusable strongly typed code.

```csharp
public class Repository<T>
{
}
```

---

## Extension Methods
Adds functionality to existing types.

```csharp
public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}
```

---

## SOLID Principles

### S
Single Responsibility

### O
Open/Closed

### L
Liskov Substitution

### I
Interface Segregation

### D
Dependency Inversion

---

## Clean Architecture
Separates concerns into layers.

Common layers:
- API
- services
- domain
- infrastructure
- persistence

---

## What causes thread starvation?
Occurs when blocked threads exhaust the thread pool.

Common causes:
- sync-over-async
- blocking I/O
- long-running tasks

---

## What is a race condition?
Concurrent access to shared mutable state causing inconsistent results.

---

## What is a deadlock?
Processes/threads block each other indefinitely.
