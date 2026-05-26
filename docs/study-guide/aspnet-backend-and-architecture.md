# ASP.NET Backend & Architecture

## async / await / Task

### Task
Represents asynchronous work.

### async
Marks a method as asynchronous.

### await
Asynchronously waits for a task to complete without blocking the thread.

```csharp
public async Task<Order> GetOrderAsync(int id)
{
    return await _repo.GetOrderAsync(id);
}
```

---

## Why async matters
Async frees threads while waiting on I/O.

Important for:
- APIs
- DB calls
- HTTP calls
- queues
- cloud systems

---

## Dependency Injection (DI)
DI decouples implementations from consumers.

Benefits:
- testing
- mocking
- maintainability
- lifecycle management

```csharp
builder.Services.AddScoped<IOrderService, OrderService>();
```

---

## Singleton vs Scoped vs Transient

### Singleton
Single instance for app lifetime.

### Scoped
One instance per request.

### Transient
New instance every injection.

---

## Middleware
Middleware intercepts HTTP requests/responses in ASP.NET Core.

Common uses:
- logging
- auth
- exception handling
- CORS
- metrics

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

---

## REST vs SOAP

### REST
- JSON
- lightweight
- HTTP verbs
- modern APIs

### SOAP
- XML
- strict contracts
- enterprise integrations

---

## What is CORS?
CORS controls whether browsers allow cross-origin requests.

---

## IEnumerable vs IQueryable

### IEnumerable
Operates in memory.

### IQueryable
Builds queries executed against the database.

```csharp
IQueryable<Order> query = _db.Orders.Where(x => x.IsActive);
```

---

## Deferred Execution
LINQ queries often execute only when enumerated.

---

## First vs FirstOrDefault

### First()
Throws if no result exists.

### FirstOrDefault()
Returns null/default if no result exists.

---

## DTOs
DTOs transfer data between layers/services.

Used to:
- avoid overexposing entities
- version APIs
- shape responses

---

## Serialization vs Deserialization

### Serialization
Object -> JSON/XML

### Deserialization
JSON/XML -> Object

---

## Interface vs Abstract Class

### Interface
Contract only.

### Abstract Class
Partial implementation + inheritance.

---

## Polymorphism
Different implementations behind same interface/base type.

```csharp
IPaymentProcessor processor = new StripeProcessor();
```

---

## Encapsulation
Hiding internal implementation details behind clean public APIs.

---

## Stack vs Heap

### Stack
Small fast memory for method frames/value types.

### Heap
Dynamic memory for objects/references.

---

## Boxing / Unboxing

### Boxing
Value type -> object.

### Unboxing
Object -> value type.

---

## What is thread starvation?
Occurs when threads are blocked too long and thread pool exhaustion occurs.

Common causes:
- sync-over-async
- blocking I/O
- long-running work

---

## What is a deadlock?
Processes/threads wait on each other indefinitely.

---

## CPU Bound vs I/O Bound

### CPU Bound
Heavy computation.

### I/O Bound
Waiting on disk/network/database.

---

## Connection Pooling
Reuses DB connections instead of constantly creating/disconnecting them.

---

## Optimistic Concurrency
Checks whether data changed before update.

Often uses:
- rowversion
- timestamps
- concurrency tokens

---

## Repository Pattern
Abstracts data access from business logic.

---

## Microservices vs Monolith

### Monolith
Single deployable application.

### Microservices
Smaller independently deployable services.

Tradeoffs:
- operational complexity
- observability
- deployment independence
- scaling flexibility
