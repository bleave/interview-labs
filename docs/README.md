# Senior Engineering Interview Reps & Reference Guide

## Purpose
This document is designed for rapid interview prep and verbal recall practice for senior .NET / Angular / Ecommerce / Distributed Systems interviews.

Recommended answer structure:
1. Definition
2. Why it’s used
3. Real-world example

Keep answers concise and structured.

---

# Angular / Frontend

## What controls state in Angular?
### Answer
Angular state can exist at multiple levels depending on scope and complexity. Local component state is typically managed directly within components, shared application state is often managed through services using RxJS observables or BehaviorSubjects, and larger applications may use centralized state management solutions like NgRx. Newer Angular versions also support signals for reactive state handling.

### Common Follow-Up
#### What tools are commonly used for Angular state?
- Component variables
- Services
- RxJS
- BehaviorSubject
- Signals
- NgRx
- Router state
- Form state

---

## Subject vs BehaviorSubject
### Answer
A Subject emits values to subscribers but does not retain previous values. A BehaviorSubject retains the latest emitted value and immediately provides it to new subscribers, making it useful for shared application state.

### Why Use Subject?
Subjects are useful for event streams where you only care that an event occurred, not the current state.

### Subject Examples
- refresh requested
- modal opened
- notification triggered
- websocket event

### BehaviorSubject Examples
- current user
- shopping cart
- selected account
- application settings

### Key Concept
Subject = future events only.
BehaviorSubject = current value + future events.

---

## What does next() do in RxJS?
### Answer
next() emits or publishes a new value to subscribers of a Subject or BehaviorSubject.

### Example
```typescript
cartItems$.next(updatedCart);
```

---

## What is NgRx?
### Answer
NgRx is a Redux-style state management library for Angular that centralizes application state into a predictable store using actions, reducers, selectors, and effects.

### Why Use It?
Useful for large Angular applications with complex shared state.

### Core Pieces
- Store
- Actions
- Reducers
- Effects
- Selectors

### Tradeoff
NgRx improves predictability and debugging but can introduce significant boilerplate.

---

## What is a reducer?
### Answer
A reducer is a pure function that takes the current state and an action, then returns a new updated state.

### Important Concept
Reducers should avoid side effects and remain predictable.

### Example
```typescript
on(addToCart, (state, action) => ({
   ...state,
   cart: [...state.cart, action.item]
}))
```

---

## What are actions?
### Answer
Actions are events or payloads describing something that happened in the application, typically dispatched to update centralized state.

### Examples
- addToCart
- loginSuccess
- loadOrders

---

## What are selectors?
### Answer
Selectors are reusable functions used to retrieve or derive specific pieces of state from a centralized store.

---

## What are effects?
### Answer
Effects handle asynchronous operations and side effects such as API calls, logging, navigation, or external integrations.

### Examples
- HTTP requests
- logging
- queue publishing
- navigation

---

## What are signals in Angular?
### Answer
Signals are Angular's newer reactive state primitives used for managing reactive UI state with automatic dependency tracking and updates.

### Example
```typescript
count = signal(0);
```

---

## What is change detection?
### Answer
Change detection is Angular's mechanism for tracking and updating the UI when application state changes.

---

## What are standalone components?
### Answer
Standalone components are Angular components that do not require NgModules and can declare their own dependencies directly.

---

## What are observables?
### Answer
Observables are asynchronous streams of data/events that subscribers can react to over time.

### Common Uses
- HTTP requests
- websocket streams
- reactive state
- event handling

---

## What is Redux?
### Answer
Redux is a predictable centralized state management pattern where application state is stored in a single store and updated through dispatched actions and reducers.

### Flow
UI -> dispatch action -> reducer updates store -> UI reacts.

---

## What is a side effect?
### Answer
A side effect occurs when a function modifies external state or interacts with systems outside its scope, such as API calls, database writes, logging, or changing global state.

### Pure Function
A pure function only computes and returns a value.

### Side Effect Examples
- API calls
- DB writes
- logging
- modifying external variables
- sending emails

---

# Ecommerce / Distributed Systems

## What is idempotency?
### Answer
Idempotency means repeated identical requests produce the same result without unintended duplicate side effects.

### Ecommerce Example
Retrying a checkout request should not charge the customer twice.

### Common Implementations
- idempotency keys
- transaction IDs
- duplicate detection
- payment attempt tracking

---

## What is a webhook?
### Answer
A webhook is an HTTP callback/event notification where one system pushes event data to another system asynchronously.

### Why Use It?
Prevents constant polling.

### Ecommerce Examples
- order created
- payment captured
- shipment updated
- refund processed

### Typical Architecture
Webhook -> API endpoint -> Queue -> Background processing

### Example: Receiving A Webhook In ASP.NET Core
```csharp
[ApiController]
[Route("api/webhooks")]
public class WebhooksController : ControllerBase
{
    [HttpPost("payment")]
    public async Task<IActionResult> PaymentWebhook([FromBody] PaymentWebhookDto dto)
    {
        // Validate signature/token if applicable

        // Log event
        _logger.LogInformation($"Received payment event {dto.TransactionId}");

        // Queue async processing
        await _serviceBus.PublishAsync(dto);

        return Ok();
    }
}
```

### Example: Subscribing To A Webhook
Typically done through a third-party provider dashboard or API.

Example:
```json
{
  "event": "payment_succeeded",
  "callbackUrl": "https://myapp.com/api/webhooks/payment"
}
```

### Important Concepts
- webhook signature validation
- retries
- idempotency
- async processing
- correlation IDs
- queue decoupling

---
### Answer
A webhook is an HTTP callback/event notification where one system pushes event data to another system asynchronously.

### Why Use It?
Prevents constant polling.

### Ecommerce Examples
- order created
- payment captured
- shipment updated
- refund processed

### Typical Architecture
Webhook -> API endpoint -> Queue -> Background processing

---

## What is a dead-letter queue (DLQ)?
### Answer
A dead-letter queue stores messages that repeatedly fail processing so poison messages do not endlessly retry and impact healthy queue throughput.

### Common Causes
- malformed payload
- processing exceptions
- exceeded MaxDeliveryCount
- validation failures

---

## What is a poison message?
### Answer
A poison message is a message that repeatedly fails processing due to invalid data or unrecoverable processing conditions.

---

## What is PeekLock in Azure Service Bus?
### Answer
PeekLock temporarily locks a message for processing without removing it from the queue. If processing succeeds the message is completed; otherwise it becomes available again after lock expiration.

### Why Important?
Improves durability and retry handling.

---

## What causes messages to move to the DLQ?
### Answer
Messages are typically moved to the dead-letter queue after exceeding MaxDeliveryCount or when explicitly dead-lettered due to unrecoverable processing failures.

---

## What is eventual consistency?
### Answer
Eventual consistency means distributed systems may not become immediately consistent across all components, but they will converge to a consistent state over time.

### Ecommerce Example
Payment succeeds immediately while inventory, analytics, and email processing update asynchronously afterward.

---

## Why are retries dangerous without idempotency?
### Answer
Retries without idempotency can create duplicate side effects such as duplicate charges, duplicate orders, or repeated downstream processing.

---

## Why use queues in ecommerce systems?
### Answer
Queues improve scalability, fault tolerance, retries, and system decoupling by allowing downstream operations to process asynchronously.

### Common Async Tasks
- email notifications
- fulfillment
- inventory updates
- analytics
- fraud processing

---

## How would you design a fault-tolerant checkout/payment system?
### Answer
Separate mutable cart state from transactional checkout processing. Validate inventory and pricing, create a pending order/payment attempt, tokenize payment data, process payment through a provider like Braintree or Stripe, and use idempotency keys to prevent duplicate charges. Downstream processing should occur asynchronously through queues like Azure Service Bus with retries and DLQ handling.

### Important Concepts
- tokenization
- PCI compliance
- retries
- idempotency
- async processing
- monitoring
- durability

---

# ASP.NET / Backend / Architecture

## What is dependency injection?
### Answer
Dependency injection decouples implementations from consumers by injecting dependencies through interfaces instead of tightly coupling concrete classes.

### Benefits
- testability
- maintainability
- lifecycle management
- inversion of control

---

## What is inversion of control?
### Answer
Inversion of control means object creation and dependency management are controlled externally rather than directly inside the class. Dependency injection is a common way to implement IoC.

---

## Service Lifetimes
### Singleton
One instance for the application lifetime.

### Scoped
One instance per HTTP request.

### Transient
New instance every injection/request.

### Singleton Risks
Singleton services can become dangerous when they maintain mutable shared state or use non-thread-safe resources across concurrent requests.

---

## What is middleware?
### Answer
Middleware consists of components within the ASP.NET request pipeline that process requests and responses for concerns like logging, authentication, exception handling, routing, and CORS.

---

## Authentication vs Authorization
### Answer
Authentication verifies who the user is, while authorization determines what an authenticated user is allowed to do.

### HTTP Status Codes
401 = unauthenticated
403 = authenticated but forbidden

---

## REST vs SOAP
### Answer
REST is a lightweight resource-oriented architectural style typically using HTTP verbs and JSON payloads, while SOAP is a more protocol-oriented XML-based messaging standard with stricter contracts and enterprise features.

---

## PUT vs PATCH
### Answer
PUT typically replaces or fully updates a resource, while PATCH performs partial updates to specific fields.

---

## What is CORS?
### Answer
CORS is a browser security mechanism that controls whether a web application can make requests to a different domain than the one that served the frontend.

---

## Horizontal vs Vertical Scaling
### Horizontal Scaling
Adding more servers/instances.

### Vertical Scaling
Adding more resources to an individual server.

---

## What causes memory leaks?
### Answer
Memory leaks occur when objects remain referenced longer than necessary and cannot be garbage collected.

### Common Causes
- event subscriptions
- static references
- cached objects
- unmanaged resources
- improperly scoped long-lived objects

---

## What is async/await?
### Answer
Task represents an asynchronous operation. The async keyword allows a method to use await internally, and await asynchronously waits for a Task to complete without blocking the calling thread.

### Why Async Improves Scalability
Async improves scalability because threads are not blocked while waiting on I/O operations like database calls or HTTP requests.

---

## What is thread starvation?
### Answer
Thread starvation occurs when too many threads are blocked waiting on work or resources, preventing the thread pool from servicing additional requests efficiently.

### Common Causes
- blocking I/O
- .Result
- .Wait()
- Thread.Sleep
- deadlocks
- long-running CPU tasks

---

## Synchronous vs Asynchronous Processing
### Answer
Synchronous processing blocks while waiting for completion, while asynchronous processing allows other work to continue while awaiting results or background processing.

---

## Stateful vs Stateless Systems
### Stateful
Retains session/context between requests.

### Stateless
Each request is independent and self-contained.

---

## Composition vs Inheritance
### Answer
Inheritance allows a class to derive behavior and properties from another class, while composition builds functionality by combining smaller collaborating objects or services.

### Example
Inheritance: Dog IS-A Animal
Composition: Car HAS-A Engine

---

# Additional Backend / OOP / Runtime Concepts

## What is deferred execution?
### Answer
Deferred execution means a query is not executed until the data is actually enumerated or materialized.

### Common Example
LINQ queries against IQueryable.

---

## What is lazy loading?
### Answer
Lazy loading delays loading related data or objects until they are actually accessed.

---

## Interface vs Abstract Class
### Interface
Defines a contract/signature without implementation.

### Abstract Class
Provides shared base behavior and partial implementation for derived classes.

---

## What is polymorphism?
### Answer
Polymorphism allows different objects or implementations to be treated through a common interface while behaving differently at runtime.

### Example
Animal.Speak() -> Dog barks, Cat meows.

---

## What is encapsulation?
### Answer
Encapsulation hides internal implementation details and exposes controlled access to data and behavior through methods or properties.

---

## Stack vs Heap
### Stack
- method calls
- local variables
- value types
- fast allocation

### Heap
- dynamically allocated objects
- reference types
- garbage collected

---

## What is boxing/unboxing?
### Boxing
Converting a value type into an object/reference type.

### Unboxing
Converting the object/reference back into a value type.

### Concern
Can introduce performance overhead.

---

## What is a deadlock?
### Answer
A deadlock occurs when multiple processes or transactions block each other while waiting for resources held by the other process.

---

## CPU-bound vs I/O-bound work
### CPU-bound
Limited primarily by processor computation.

### I/O-bound
Spends most of its time waiting on external operations like databases, APIs, files, or networks.

---

## What is connection pooling?
### Answer
Connection pooling reuses existing database connections instead of constantly opening and closing new ones, improving performance and scalability.

---

## What is a race condition?
### Answer
A race condition occurs when multiple threads or processes access and modify shared mutable state concurrently in a way that produces unpredictable or inconsistent results.

---

## Mutable vs Immutable Objects
### Mutable
Object state can change after creation.

### Immutable
Object state cannot change after creation.

### Benefit of Immutable
Improves predictability and thread safety.

---

## What is a DTO?
### Answer
A DTO (Data Transfer Object) is used to transfer data between application layers or services while decoupling internal domain models from external contracts.

---

## Serialization vs Deserialization
### Serialization
Object -> JSON/XML/etc.

### Deserialization
JSON/XML/etc -> Object.

---

## Optimistic vs Pessimistic Locking
### Optimistic Locking
Assumes conflicts are rare and validates data before save.

### Pessimistic Locking
Locks resources immediately to prevent concurrent modification.

---

## Monolith vs Microservices
### Monolith
Single deployable application with tightly integrated components.

### Microservices
Independently deployable services communicating through APIs or messaging.

### Microservice Tradeoff
Improves decoupling but increases operational complexity.

---

# SQL / Database

## IEnumerable vs IQueryable
### Answer
IEnumerable operates against in-memory collections, while IQueryable allows query expressions to be translated and executed against the underlying data source such as SQL Server.

---

## What is a database index?
### Answer
A database index improves query performance by creating a fast lookup structure on one or more columns.

### Tradeoff
Indexes improve reads but can slow inserts/updates because indexes must also be maintained.

---

## What is a CTE?
### Answer
A Common Table Expression (CTE) is a temporary named result set used to simplify complex queries and improve readability.

---

## Temp Table vs Table Variable
### Temp Table
Better for larger datasets and complex operations.

### Table Variable
Lighter-weight and often better for smaller short-lived datasets.

---

## What is normalization?
### Answer
Normalization organizes relational data to reduce redundancy and improve data integrity.

---

## What causes deadlocks?
### Answer
Deadlocks occur when multiple transactions block each other while waiting for resources held by the other transaction.

---

## What is optimistic concurrency?
### Answer
Optimistic concurrency assumes conflicts are rare and validates that data has not changed before committing updates, often using row versions or timestamps.

---

## SQL vs NoSQL
### SQL
Relational, structured, ACID-focused.

### NoSQL
Flexible schema, horizontally scalable, document/key-value oriented.

---

## Why Elasticsearch instead of SQL search?
### Answer
Elasticsearch is optimized for distributed full-text search, filtering, aggregation, and relevancy scoring, making it significantly more performant than relational databases for large ecommerce catalog searches.

---

# Senior Engineering Judgment / Architecture Tradeoffs

## When are microservices a bad idea?
### Answer
Microservices can be a bad idea when application complexity or team size does not justify the added operational overhead of distributed systems, networking, monitoring, retries, deployment coordination, and eventual consistency.

---

## Why is premature optimization dangerous?
### Answer
Premature optimization can waste development effort optimizing the wrong bottlenecks before real production metrics or scaling concerns are understood.

---

## Clean Architecture vs Operational Simplicity
### Answer
Architecture should balance maintainability and scalability without introducing unnecessary complexity. Simplicity should not ignore good design, but architecture should also not overextend for theoretical correctness.

---

## Technical Debt vs Delivery Speed
### Answer
Technical debt should be managed consciously by balancing delivery needs with maintainability, prioritizing debt that impacts reliability, scalability, developer velocity, or operational stability.

---

## Senior vs Mid-Level Engineer
### Answer
Senior engineers think beyond implementation and consider architecture, scalability, maintainability, operational impact, tradeoffs, mentoring, and long-term system health.

---

## Debugging Unfamiliar Systems
### Answer
Start by identifying scope, affected systems, dependencies, telemetry, logs, and recent changes before narrowing down the issue through monitoring, tracing, debugging, and code analysis.

---

## Scaling Reads vs Writes
### Answer
Read scaling is often improved through caching, replicas, CDNs, or distributed search systems, while write scaling is harder because writes require consistency, coordination, and transactional integrity.

---

## What causes cascading failures?
### Answer
Cascading failures occur when failing systems continue retrying or overloading dependent systems without failing fast or isolating the failure.

---

## Why can retries make outages worse?
### Answer
Retries can amplify outages when failed operations repeatedly overload already unhealthy downstream systems.

---

## Hardest Parts of Distributed Systems
### Answer
Distributed systems are difficult because of coordination, retries, observability, partial failures, eventual consistency, networking issues, and maintaining reliability across many moving parts.

---

## Why is observability important?
### Answer
Observability provides visibility into application behavior, performance, dependencies, failures, and distributed request flows so issues can be identified and resolved quickly.

---

## How do you reduce blast radius during deployments?
### Answer
Use staged deployments, feature flags, smaller releases, rollback strategies, isolated feature scopes, and thorough pre-production validation.

---

## Why are ecommerce systems difficult?
### Answer
Ecommerce systems combine payments, inventory consistency, integrations, fraud prevention, scaling, reliability, and high user expectations under real-time transactional workloads.

---

## Resilience vs Redundancy
### Resilience
Ability to fail gracefully and recover.

### Redundancy
Backup systems/resources available during failure.

---

## Why are integrations fragile?
### Answer
Integrations are often fragile because changing contracts, network failures, authentication changes, payload differences, or version mismatches can easily break communication between systems.

---

## Explaining Technical Debt
### Answer
Technical debt refers to shortcuts or legacy design decisions that make future development, maintenance, scaling, or operational stability more difficult over time.

---

## Production Stability vs Feature Development
### Answer
Production stability should generally take priority because unstable systems reduce customer trust and slow future delivery. Feature development should balance velocity with operational reliability.

---

## Why are APIs difficult to maintain long-term?
### Answer
Changing business needs, evolving contracts, backward compatibility requirements, versioning, and consumer dependencies make APIs difficult to evolve safely over time.

---

## Mentoring Junior Developers
### Answer
Mentor through code reviews, debugging guidance, architecture discussions, operational thinking, and helping junior developers understand tradeoffs rather than just implementation details.

---

## Why are soft skills important?
### Answer
Senior engineers need strong communication, collaboration, mentoring, stakeholder management, and cross-team coordination skills in addition to technical ability.

---

# Production / Operations / Reliability Scenarios

## Intermittent Checkout Failures But Successful Payments
### Answer
Check application logs, telemetry, App Insights, queue processing, and payment provider transaction records to determine whether failures occurred before or after order finalization.

---

## Low CPU But Extremely Slow Website
### Possible Causes
- thread starvation
- database contention
- deadlocks
- network latency
- slow downstream dependencies
- queue backlog
- lock contention

---

## Query Works In Dev But Times Out In Production
### Common Causes
- larger datasets
- missing indexes
- concurrency/load differences
- different execution plans
- parameter sniffing

---

## What does HTTP 429 mean?
### Answer
Too many requests / rate limiting.

---

## Error Rates Spike After Deployment
### Answer
Check logs, telemetry, and recent changes immediately. Determine whether rollback, feature disablement, or DR failover is necessary.

---

## Queue Backlog Growing Rapidly
### Common Causes
- slow consumers
- downstream dependency failures
- poison messages
- excessive retries
- insufficient scaling

---

## Risks When Cache Servers Fail
### Answer
Cache failures can create DB storms/thundering herd scenarios where all requests suddenly bypass cache and overload databases.

---

## Duplicate Charges Investigation
### Investigate
- idempotency handling
- retry logic
- timeout behavior
- payment gateway records
- transaction tracking
- duplicate submissions

---

## Protecting Against Slow External APIs
### Techniques
- circuit breakers
- retries with backoff
- timeout policies
- fail-fast behavior
- graceful degradation

---

## Technically Successful Deployment But Broken Business Workflows
### Answer
Technical deployment succeeded but business validation/testing missed workflow-level regressions or edge cases.

---

## Why Logs Alone May Not Be Enough
### Answer
Distributed systems may require metrics, tracing, infrastructure telemetry, and correlation IDs in addition to logs.

---

## Identifying Memory Leaks In Production
### Indicators
- memory growth over time
- increasing GC pressure
- degrading performance
- retained objects/resources

---

## High Traffic Event Risks
### Common Issues
- scaling bottlenecks
- queue backlog
- cache pressure
- DB saturation
- checkout failures
- slow response times

---

## Why Retries Create Duplicate Data
### Answer
Retries can repeat operations unless requests are idempotent or duplicate detection exists.

---

## Horizontal Scaling vs Load Balancing
### Horizontal Scaling
Adding additional nodes/servers.

### Load Balancing
Distributing traffic across servers.

---

## Shared Mutable State Risks
### Answer
Shared mutable state can create race conditions, synchronization issues, and unpredictable behavior.

---

## Feature Flags
### Answer
Feature flags allow features or functionality to be enabled or disabled independently of deployment.

---

## Safely Releasing Risky Changes
### Techniques
- staging validation
- phased rollout
- feature flags
- rollback plans
- DR validation
- isolated deployments

---

## Outage Priorities
### Answer
Service restoration and stabilization should generally take priority before detailed root cause analysis.

---

# Monitoring / Production Support

## Monitoring vs Durability
### Monitoring
Provides operational visibility into system behavior.

### Durability
Ensures critical data/work survives failures or outages.

---

## Production Issue Story Structure
1. Situation
2. Investigation
3. Root Cause
4. Mitigation
5. Long-Term Fix
6. Prevention

---

## Example Production Issue
### Situation
Production ecommerce platform experienced severe performance degradation under peak traffic.

### Investigation
Used telemetry, logs, SQL monitoring, and long-running query analysis to identify bottlenecks.

### Root Cause
Expensive SQL queries and indexing issues degraded significantly under production-scale load.

### Mitigation
Rollback procedures, query optimization, temporary feature reduction, and index rebuilding stabilized production.

### Long-Term Fix
Migrated heavy search operations into Elasticsearch and improved monitoring.

---

# Alliance Entertainment Story

## 30-60 Second Summary
At Alliance Entertainment I worked on modernization efforts for a large enterprise ecommerce ecosystem supporting multiple storefronts and integrations. The platform evolved from older monolithic and legacy systems toward more modern ASP.NET MVC, Web API, Angular, Elasticsearch, and distributed service-based architectures. My work involved performance optimization, operational support, payment and fraud integrations, search modernization, reliability improvements, and scalable enterprise commerce workflows.

