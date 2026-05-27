# Senior Developer Interview Rapid Recall Guide

## SOLID

- **S — Single Responsibility**: One job only.
- **O — Open/Closed**: Extend, don’t rewrite.
- **L — Liskov Substitution**: Child behaves like parent.
- **I — Interface Segregation**: Keep interfaces small.
- **D — Dependency Inversion**: Depend on abstractions.

## ACID

- **Atomicity**: All or nothing.
- **Consistency**: Keep data valid.
- **Isolation**: Transactions stay separate.
- **Durability**: Saved forever.

## HTTP Status Codes

- **200** OK → Success
- **201** Created → Resource created
- **204** No Content → Success, nothing returned
- **400** Bad Request → Invalid request
- **401** Unauthorized → Not authenticated
- **403** Forbidden → No permission
- **404** Not Found → Resource missing
- **409** Conflict → Duplicate/version conflict
- **500** Internal Server Error → Server crash/error
- **502** Bad Gateway → Upstream service failure
- **503** Service Unavailable → Temporary outage
- **504** Gateway Timeout → Upstream timeout

Quick memory:
- 2xx = success
- 4xx = client issue
- 5xx = server issue

## Authentication vs Authorization

- **Authentication** = Who are you?
- **Authorization** = What can you do?

## JWT / OAuth / CORS

- **JWT**: Stateless auth token
- **OAuth**: Delegated login (Google/Microsoft)
- **CORS**: Controls allowed frontend origins

## CSRF vs XSS

- **CSRF**: Tricks authenticated users into requests
- **XSS**: Injects malicious JavaScript

Protection:
- CSRF tokens
- Output encoding
- Input sanitization
- CSP headers

## Dependency Injection

Inject dependencies instead of creating them.

Benefits:
- Loose coupling
- Easier testing
- Swappable implementations

## Async / Await

Don’t block threads during I/O.

Best for:
- DB calls
- HTTP calls
- File operations

Not useful for CPU-heavy work.

## Repository Pattern / Unit of Work

- **Repository**: Abstract data access
- **Unit of Work**: Commit multiple DB operations together

## Middleware (.NET)

Request pipeline components.

Examples:
- Authentication
- Logging
- Exception handling
- CORS

## Idempotency

Safe retries without duplication.

Example:
- User clicks checkout twice
- Only one payment processes

Use idempotency keys.

## Message Queues / Events

### Queues
Async communication.

Examples:
- Azure Service Bus
- RabbitMQ
- Kafka

### Event-Driven Architecture
“Something happened.”

Examples:
- OrderPlaced
- PaymentProcessed
- InventoryReserved

## Caching

- **Memory Cache**: Single server
- **Distributed Cache**: Shared cache (Redis)
- **CDN**: Global static asset caching

## Horizontal Scaling

Add more servers.

Requirements:
- Stateless APIs
- Distributed cache
- Shared DB/storage

## Microservices

Small independently deployable services.

Pros:
- Independent scaling
- Isolation
- Team ownership

Cons:
- Complexity
- Network latency
- Distributed debugging

Senior answer:
> Don’t start with microservices unless complexity justifies it.

## SQL

### Indexes
Make reads faster.

Tradeoff:
- Faster reads
- Slower writes

### Transactions
Keep DB operations consistent.

### Deadlocks
Processes waiting on each other.

Reduce with:
- Short transactions
- Consistent table access order

### N+1 Query Problem
One query triggers many extra queries.

## API Design

- **GET** = Read
- **POST** = Create
- **PUT** = Replace
- **PATCH** = Partial update
- **DELETE** = Remove

Best practices:
- Pagination
- Validation
- Versioning
- Proper status codes

## E-Commerce System Design

### Robust Shopping Cart

Key ideas:
- Stateless APIs
- Redis/session caching
- Persistent carts
- Inventory reservation
- Idempotent checkout
- Queue order processing
- Retry failed payments

Senior answer:
> Separate cart, checkout, payment, and fulfillment concerns.

## Azure Quick Hits

- **App Service**: Hosted web apps/APIs
- **Azure Functions**: Serverless compute
- **Service Bus**: Reliable messaging
- **Azure Storage**: Scalable storage
- **Key Vault**: Secrets/certificates
- **Application Insights**: Telemetry/monitoring

## Reliability / Production Engineering

- **Retry Policy**: Retry transient failures
- **Circuit Breaker**: Stop hammering failing services
- **Health Checks**: Verify dependencies work
- **Observability**: Logs, metrics, traces

## Garbage Collection (.NET)

- **Gen 0**: Short-lived objects
- **Gen 1**: Survived Gen 0
- **Gen 2**: Long-lived objects
- **LOH**: Large Object Heap

Memory leak clues:
- Increasing memory
- High GC pressure
- Long Gen 2 collections

Tools:
- App Insights
- dotMemory
- PerfView

## Angular Quick Hits

- **Components**: Reusable UI pieces
- **Services**: Shared logic/data access
- **Dependency Injection**: Angular heavily uses DI
- **Observables/RxJS**: Reactive async streams
- **Modules**: Feature organization
- **Standalone Components**: Modern Angular architecture
- **Templates**: HTML with bindings/directives

## Senior-Level Interview Phrases

### Scaling
> Start simple and evolve architecture as bottlenecks become real.

### Performance
> Find the biggest bottleneck before optimizing.

### Reliability
> Design assuming downstream systems will eventually fail.

### Leadership
> Senior engineers reduce risk and help other developers make good decisions.

### Modernization
> Incremental modernization is safer than full rewrites.

## Common Buzzwords

- **Loose Coupling**: Minimal dependencies
- **High Cohesion**: Related responsibilities together
- **Separation of Concerns**: Keep responsibilities separated
- **DRY**: Don’t Repeat Yourself
- **KISS**: Keep It Simple, Stupid
- **YAGNI**: You Aren’t Gonna Need It
