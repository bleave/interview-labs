# Senior Engineering Interview Reps & Reference Guide

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

## Important Concepts

### 401 vs 403
- 401 = unauthenticated
- 403 = authenticated but forbidden

### 429
Commonly related to:
- throttling
- rate limiting
- DDoS protection
- API protection

### 503 vs 504
- 503 = service unavailable
- 504 = dependency timeout

### 202 Accepted
Common in:
- queue-based systems
- async processing
- distributed architectures
- webhook/event systems

---

# Angular / Frontend

## What controls state in Angular?
### Answer
Angular state can exist at multiple levels depending on scope and complexity. Local component state is typically managed directly within components, shared application state is often managed through services using RxJS observables or BehaviorSubjects, and larger applications may use centralized state management solutions like NgRx. Newer Angular versions also support signals for reactive state handling.

---

## What is the async pipe?
### Answer
The async pipe automatically subscribes and unsubscribes from observables or promises directly within Angular templates.

### Example
```html
<div>{{ user$ | async }}</div>
```

---

## What is two-way binding?
### Answer
Two-way binding synchronizes UI values and component state in both directions.

### Example
```html
<input [(ngModel)]="username" />
```

---

## What causes unnecessary Angular re-renders?
### Common Causes
- mutable object references
- excessive change detection
- functions executed in templates
- unnecessary subscriptions
- large component trees
- non-OnPush components

---

## What is OnPush change detection?
### Answer
OnPush reduces unnecessary change detection by only re-rendering when input references change, events occur, or observables/signals emit updated values.

---

## Angular Dependency Injection Hierarchy
### Answer
Angular dependency injection uses a hierarchy of injectors including root injectors and component-level injectors, allowing dependencies to be scoped globally or locally.

---

# Azure / Cloud / Distributed Systems

## What is Azure Service Bus?
### Answer
Azure Service Bus is a durable cloud messaging platform used for asynchronous communication and decoupling distributed systems.

---

## Queue vs Topic
### Queue
FIFO message processing where each message is typically consumed once.

### Topic
Publish/subscribe messaging where multiple subscriptions can receive copies of the same message.

---

## What is a subscription?
### Answer
A subscription belongs to a topic and receives its own copy of topic messages for processing.

---

## What is dead lettering?
### Answer
Dead lettering occurs when messages fail processing/retries and are moved to a dead-letter queue for inspection.

---

## What is PeekLock mode?
### Answer
PeekLock allows a consumer to lock and inspect a message before completing it. The message is only removed after successful completion.

---

## Why are queues useful?
### Answer
Queues improve reliability, throughput, retry handling, decoupling, and asynchronous communication across distributed systems.

---

## What are Azure Functions?
### Answer
Azure Functions are serverless event-driven compute units triggered by HTTP, queues, timers, webhooks, or events.

---

## When are serverless functions useful?
### Answer
Useful for lightweight event-driven workloads, background processing, queue consumers, scheduled jobs, and webhook handling.

---

## What is autoscaling?
### Answer
Autoscaling automatically increases or decreases infrastructure/resources based on system load or metrics.

---

## What is App Insights?
### Answer
Azure Application Insights provides telemetry, distributed tracing, metrics, logging, dependency tracking, and performance monitoring.

---

## What are distributed traces?
### Answer
Distributed traces follow requests across multiple services, APIs, queues, or dependencies to understand end-to-end request flow and latency.

---

## What are deployment slots?
### Answer
Deployment slots allow applications to stage builds separately from production for safer releases and easier rollback.

---

## Blob Storage vs SQL Storage
### Blob Storage
Used for unstructured files or binary data.

### SQL Storage
Used for structured relational data and transactional querying.

---

## Azure SQL vs Cosmos DB
### Azure SQL
Relational structured database.

### Cosmos DB
Globally distributed NoSQL database designed for high-scale partitioned workloads.

---

## What is eventual consistency?
### Answer
Eventual consistency allows distributed systems to become consistent over time rather than requiring immediate synchronization.

---

## What is a CDN?
### Answer
A CDN (Content Delivery Network) distributes static content closer to users for lower latency and improved scalability.

---

## Why use Redis?
### Answer
Redis improves performance through distributed caching, session/state caching, and reducing database load.

---

## Sync vs Async Integration
### Synchronous
Systems communicate in real time while waiting for responses.

### Asynchronous
Systems communicate without blocking while work completes independently.

---

# Observability / Reliability / Metrics

## What is P95 latency?
### Answer
P95 latency means 95% of requests complete within a specified response time threshold.

---

## Why are averages dangerous?
### Answer
Averages can hide outliers and poor user experiences caused by slow tail latency.

---

## Metrics vs Logs
### Metrics
Aggregated numerical measurements.

### Logs
Detailed event records.

---

## SLI / SLO / SLA
### SLI
Service Level Indicator - measurement of system health/performance.

### SLO
Service Level Objective - internal target performance goals.

### SLA
Service Level Agreement - contractual availability/performance commitment.

---

## Why are correlation IDs important?
### Answer
Correlation IDs trace requests across services, APIs, queues, logs, and dependencies.

---

## Uptime vs Availability
### Uptime
Raw operational runtime.

### Availability
Ability for users to successfully access and use the system.

---

## What are health checks?
### Answer
Health check endpoints allow infrastructure, load balancers, or orchestration systems to verify application health.

---

## Ecommerce Monitoring
### Important Metrics
- checkout success/failure
- payment lifecycle
- response times
- DB latency
- cache hit/miss
- queue backlog
- dependency failures
- error rates

---

## Leading vs Lagging Indicators
### Leading
Real-time indicators of current health.

### Lagging
Historical or summarized trend reporting.

---

## Synthetic vs Real User Monitoring
### Synthetic Monitoring
Automated scripted testing/checks.

### Real User Monitoring
Telemetry collected from actual users.

---

## What is alert fatigue?
### Answer
Too many low-value alerts desensitize teams and can hide critical incidents.

---

## What is MTTR?
### Answer
MTTR (Mean Time To Resolution/Recovery) measures how quickly incidents are resolved.

---

## What is throughput?
### Answer
Throughput measures how many requests, transactions, or operations a system can process over time.

---

## What is tail latency?
### Answer
Tail latency refers to the slowest requests in a system, typically represented by P95/P99 metrics.
