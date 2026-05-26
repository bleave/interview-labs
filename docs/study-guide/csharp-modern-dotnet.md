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

# OOP Concepts

## Interface
An interface defines a contract without implementation.

```csharp
public interface IPaymentProcessor
{
    Task ProcessAsync();
}
```

### Benefits
- abstraction
- mocking/testing
- loose coupling

---

## Abstract Class
An abstract class can provide shared behavior and partial implementation.

```csharp
public abstract class PaymentProcessor
{
    public abstract Task ProcessAsync();
}
```

---

## Static Class
Static classes cannot be instantiated.

Used for:
- helpers
- utilities
- extension methods

```csharp
public static class DateHelpers
{
}
```

---

## Sealed Class
A sealed class cannot be inherited.

```csharp
public sealed class PaymentSettings
{
}
```

---

## Inheritance
A class inherits behavior from another class.

```csharp
public class AdminUser : User
{
}
```

---

## Composition vs Inheritance

### Composition
Classes contain other objects.

### Inheritance
Classes derive from base classes.

Composition is often preferred for flexibility.

---

## Polymorphism
Different implementations behind a shared contract.

```csharp
IPaymentProcessor processor = new StripeProcessor();
```

---

## Encapsulation
Hiding implementation details behind clean APIs.

---

## Access Modifiers

### public
Accessible everywhere.

### private
Accessible only within class.

### protected
Accessible within inheritance hierarchy.

### internal
Accessible within assembly/project.

Useful for hiding implementation details from external projects.

---

## Virtual vs Override

### virtual
Allows overriding.

### override
Overrides base implementation.

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

## Delegates
Delegates represent references to methods.

---

## Func vs Action

### Func
Returns a value.

### Action
Returns void.

---

## Lambda Expressions
Short inline functions.

```csharp
users.Where(x => x.IsActive)
```

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

## Stack vs Heap

### Stack
Fast memory for method frames/value types.

### Heap
Dynamic memory for objects.

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

# SOLID Principles

## S - Single Responsibility Principle
A class should have one reason to change.

### Bad Example
A class handling:
- database logic
- email sending
- business logic
- logging

### Better
Separate responsibilities into focused classes/services.

### Benefits
- maintainability
- testability
- cleaner architecture

---

## O - Open/Closed Principle
Software should be open for extension but closed for modification.

### Goal
Add new behavior without constantly changing existing code.

### Example
Adding a new payment provider through a new implementation rather than modifying existing processors.

```csharp
public interface IPaymentProcessor
{
    Task ProcessAsync();
}
```

---

## L - Liskov Substitution Principle
Derived classes should be replaceable for their base classes without breaking behavior.

### Example
If a class inherits from Bird but cannot fly while callers expect flying behavior, inheritance may be incorrect.

### Important Concept
Inheritance should preserve expected behavior.

---

## I - Interface Segregation Principle
Clients should not depend on interfaces they do not use.

### Bad Example
Large interfaces with unrelated methods.

### Better
Smaller focused interfaces.

```csharp
public interface IEmailSender
{
    Task SendAsync();
}
```

---

## D - Dependency Inversion Principle
High-level modules should depend on abstractions, not concrete implementations.

### Example
Depend on:
```csharp
IOrderService
```

instead of:
```csharp
OrderService
```

### Benefits
- loose coupling
- testability
- flexibility
- mocking

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
