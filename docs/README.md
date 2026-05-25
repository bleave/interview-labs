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
