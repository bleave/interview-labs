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
Occurs when too many thread pool threads become blocked waiting on slow I/O or synchronous operations.

Common causes:
- sync-over-async
- blocking HTTP calls
- slow SQL queries
- .Result / .Wait()
- long-running synchronous work
- lock contention

Symptoms:
- low CPU
- high response times
- request timeouts
- queued requests

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

# Polly / Resilience Patterns

## What is Polly?
Polly is a .NET resilience and transient-fault-handling library.

It helps applications handle failures around:
- external APIs
- payment processors
- SQL calls
- distributed systems
- network calls

Common policies:
- retries
- exponential backoff
- circuit breakers
- timeouts
- fallback behavior
- rate limiting

---

## Polly vs Azure Service Bus

### Polly
Application-level resilience logic.

Used when code is calling another dependency directly.

### Azure Service Bus
Messaging infrastructure for asynchronous communication and durable message processing.

They are different tools, but they can work together.

Example flow:
```text
API -> Polly retry/circuit breaker -> if still failing -> queue message or fail gracefully
```

---

## Retry Policy Example
Retries can help recover from temporary failures.

```csharp
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .RetryAsync(3);

await retryPolicy.ExecuteAsync(async () =>
{
    await httpClient.GetAsync("https://api.partner.com/orders");
});
```

Important:
Retries need limits.
Unlimited retries can make outages worse.

---

## Exponential Backoff
Exponential backoff waits longer between each retry.

Example:
```text
Retry 1 -> wait 1 second
Retry 2 -> wait 2 seconds
Retry 3 -> wait 4 seconds
```

Why it matters:
- avoids retry storms
- reduces pressure on failing systems
- gives dependencies time to recover

---

## Circuit Breaker Pattern
A circuit breaker temporarily stops calling a dependency after repeated failures.

Purpose:
- protect thread pools
- reduce cascading failures
- avoid hammering a failing dependency
- fail fast instead of hanging

Example:
```csharp
var breaker = Policy
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(
        exceptionsAllowedBeforeBreaking: 5,
        durationOfBreak: TimeSpan.FromSeconds(30));
```

---

## Timeout Policy
Timeouts prevent requests from hanging forever.

```csharp
var timeoutPolicy = Policy.TimeoutAsync(5);
```

This is important because long waits can contribute to thread starvation and request pileups.

---

## Why retries can be dangerous
Retries can:
- create duplicate side effects
- amplify outages
- overload downstream systems
- exhaust thread pools
- cause cascading failures

Especially dangerous when operations are not idempotent.

---

## Payment Processor Example
If a third-party payment processor is failing, resilience patterns do not magically make checkout work.

A circuit breaker protects the rest of the system by failing fast instead of letting requests hang or retry forever.

Possible responses:
- show a controlled “payments temporarily unavailable” message
- disable checkout while carts/browsing continue working
- fail over to another payment provider if available
- queue non-payment follow-up work where appropriate
- alert operations/support immediately

Important concept:
Sometimes resilience means controlled degradation, not full recovery.

---

## Strong Interview Answer
“Polly is a .NET resilience library used for retries, circuit breakers, timeouts, and fallback behavior. It is useful for transient failures, but retry logic needs limits, backoff, and idempotency because otherwise it can make distributed system failures worse.”

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
