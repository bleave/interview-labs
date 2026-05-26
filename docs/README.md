# Senior Engineering Interview Prep Guide

## Purpose
This document is designed for rapid interview prep and verbal recall practice for senior .NET / Angular / Ecommerce / Distributed Systems interviews.

Recommended answer structure:
1. Definition
2. Why it’s used
3. Real-world example

Keep answers concise and structured.

---

# Important HTTP Status Codes

## Success Codes (2xx)

### 200 OK
Request succeeded successfully.

### 201 Created
Resource was successfully created.

### 202 Accepted
Request accepted for asynchronous/background processing.

### 204 No Content
Request succeeded but nothing is returned.

---

## Client Errors (4xx)

### 400 Bad Request
Invalid request/payload from caller.

### 401 Unauthorized
Authentication required or invalid credentials.

### 403 Forbidden
User is authenticated but not allowed to access resource.

### 404 Not Found
Requested resource does not exist.

### 409 Conflict
Conflict with current state/data (often concurrency issues or duplicates).

### 422 Unprocessable Entity
Validation failed despite valid request structure.

### 429 Too Many Requests
Rate limiting/throttling exceeded.

---

## Server Errors (5xx)

### 500 Internal Server Error
Generic unexpected server failure.

### 502 Bad Gateway
Upstream/downstream service returned invalid response.

### 503 Service Unavailable
Service temporarily unavailable or overloaded.

### 504 Gateway Timeout
Upstream dependency timed out.

---

# Angular / Frontend

## What controls state in Angular?
Angular state can exist at multiple levels depending on scope and complexity. Local component state is typically managed directly within components, shared application state is often managed through services using RxJS observables or BehaviorSubjects, and larger applications may use centralized state management solutions like NgRx. Newer Angular versions also support signals for reactive state handling.

## What is the async pipe?
The async pipe automatically subscribes and unsubscribes from observables or promises directly within Angular templates.

## What is two-way binding?
Two-way binding synchronizes UI values and component state in both directions.

## What causes unnecessary Angular re-renders?
- mutable object references
- excessive change detection
- functions executed in templates
- unnecessary subscriptions
- large component trees
- non-OnPush components

## What is OnPush change detection?
OnPush reduces unnecessary change detection by only re-rendering when input references change, events occur, or observables/signals emit updated values.

## Angular Dependency Injection Hierarchy
Angular dependency injection uses a hierarchy of injectors including root injectors and component-level injectors, allowing dependencies to be scoped globally or locally.

## What is a reducer?
A reducer is a pure function that takes the current state and an action, then returns a new updated state.

## What are actions?
Actions are events or payloads describing something that happened in the application, typically dispatched to update centralized state.

## What are selectors?
Selectors are reusable functions used to retrieve or derive specific pieces of state from a centralized store.

## What are effects?
Effects handle asynchronous operations and side effects such as API calls, logging, navigation, or external integrations.

## What are signals in Angular?
Signals are Angular's newer reactive state primitives used for managing reactive UI state with automatic dependency tracking and updates.

## What are observables?
Observables are asynchronous streams of data or events that subscribers can react to over time.

## Subject vs BehaviorSubject
### Subject
Only emits new values after subscription.

### BehaviorSubject
Retains the latest value and immediately emits it to new subscribers.

---

# Azure / Cloud / Distributed Systems

## What is Azure Service Bus?
Azure Service Bus is a durable cloud messaging platform used for asynchronous communication and decoupling distributed systems.

## Queue vs Topic
### Queue
FIFO message processing where each message is typically consumed once.

### Topic
Publish/subscribe messaging where multiple subscriptions can receive copies of the same message.

## What is a subscription?
A subscription belongs to a topic and receives its own copy of topic messages for processing.

## What is dead lettering?
Dead lettering occurs when messages fail processing/retries and are moved to a dead-letter queue for inspection.

## What is PeekLock mode?
PeekLock allows a consumer to lock and inspect a message before completing it. The message is only removed after successful completion.

## Why are queues useful?
Queues improve reliability, throughput, retry handling, decoupling, and asynchronous communication across distributed systems.

## What are Azure Functions?
Azure Functions are serverless event-driven compute units triggered by HTTP, queues, timers, webhooks, or events.

## What is App Insights?
Azure Application Insights provides telemetry, distributed tracing, metrics, logging, dependency tracking, and performance monitoring.

## What are distributed traces?
Distributed traces follow requests across multiple services, APIs, queues, or dependencies to understand end-to-end request flow and latency.

## What is eventual consistency?
Eventual consistency allows distributed systems to become consistent over time rather than requiring immediate synchronization.

---

# Observability / Reliability / Metrics

## What is P95 latency?
P95 latency means 95% of requests complete within a specified response time threshold.

## Metrics vs Logs
### Metrics
Aggregated numerical measurements.

### Logs
Detailed event records.

## SLI / SLO / SLA
### SLI
Service Level Indicator.

### SLO
Service Level Objective.

### SLA
Service Level Agreement.

## What is MTTR?
MTTR (Mean Time To Resolution/Recovery) measures how quickly incidents are resolved.

## What is tail latency?
Tail latency refers to the slowest requests in a system, typically represented by P95/P99 metrics.

---

# Security

## What is hashing?
Hashing is a one-way non-reversible transformation commonly used for passwords and integrity verification.

## Why should passwords be hashed and salted?
Passwords should be salted and hashed rather than encrypted directly so the original password cannot easily be recovered even if the datastore is compromised.

## What is JWT authentication?
JWT authentication uses signed tokens containing claims about the authenticated user, allowing stateless authentication between clients and APIs.

## What is SQL injection?
SQL injection occurs when malicious SQL input is executed against a database.

---

# Ecommerce / Distributed Systems

## What is idempotency?
Idempotency means repeated operations produce the same result without creating unintended duplicate side effects.

## Why are retries dangerous without idempotency?
Retries can create duplicate orders, duplicate charges, or inconsistent state if operations are not idempotent.

## What is a webhook?
A webhook is an HTTP callback/event notification where one system pushes event data to another system asynchronously.

Typical flow:
Webhook -> API endpoint -> Queue -> Background processing

---

# Production / Operations

## What causes cascading failures?
Cascading failures occur when failing systems continue retrying or overloading dependent systems without failing fast or isolating the failure.

## What is a deadlock?
A deadlock occurs when multiple processes or transactions block each other while waiting for resources held by the other process.

## What is a race condition?
A race condition occurs when multiple threads or processes access and modify shared mutable state concurrently in a way that produces unpredictable or inconsistent results.

## Why are ecommerce systems difficult?
Ecommerce systems combine payments, inventory consistency, integrations, fraud prevention, scaling, reliability, and high user expectations under real-time transactional workloads.
