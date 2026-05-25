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

