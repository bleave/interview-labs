# Senior Developer Interview Rapid Recall Guide

---

# SOLID Principles

## S — Single Responsibility
“One job only.”

A class should have one reason to change.

---

## O — Open/Closed
“Extend, don’t rewrite.”

Add new behavior without modifying existing code.

---

## L — Liskov Substitution
“Child must behave like parent.”

Derived classes should not break expected behavior.

---

## I — Interface Segregation
“Keep interfaces small.”

Don’t force classes to implement methods they don’t use.

---

## D — Dependency Inversion
“Depend on contracts, not implementations.”

Inject abstractions/interfaces instead of concrete classes.

Example:
- `IMessageService`
- `EmailService`
- `SmsService`

---

# ACID Database Principles

## A — Atomicity
“All or nothing.”

---

## C — Consistency
“Keep data valid.”

---

## I — Isolation
“Transactions stay separate.”

---

## D — Durability
“Saved forever.”

---

# HTTP Status Codes

## 200 OK
Request succeeded.

## 201 Created
Resource successfully created.

## 204 No Content
Success but nothing returned.

## 400 Bad Request
Invalid request from client.

## 401 Unauthorized
Not authenticated.

## 403 Forbidden
Authenticated but not allowed.

## 404 Not Found
Resource does not exist.

## 409 Conflict
Conflict with current state/data.

Example:
- Duplicate email
- Version conflict

## 500 Internal Server Error
Unhandled server failure.

## 502 Bad Gateway
Upstream service failed.

## 503 Service Unavailable
Service temporarily unavailable.

## 504 Gateway Timeout
Upstream service timed out.

Quick memory:
- 2xx = success
- 4xx = client issue
- 5xx = server issue

---

# Authentication vs Authorization

## Authentication
“Who are you?”

Login validation.

---

## Authorization
“What can you do?”

Permissions and access control.

---

# JWT

“Stateless auth token.”

Contains:
- User ID
- Claims/Roles
- Expiration

Server validates token signature instead of session state.

---

# OAuth

“Delegated login.”

Examples:
- Login with Google
- Login with Microsoft

---

# CORS

“Browser security policy.”

Controls which frontends may call your API.

---

# CSRF vs XSS

## CSRF
Tricks authenticated users into making requests.

Protection:
- CSRF tokens
- SameSite cookies

---

## XSS
Injects malicious JavaScript.

Protection:
- Output encoding
- Input sanitization
- CSP headers

---

# Dependency Injection

“Inject dependencies instead of creating them.”

Benefits:
- Loose coupling
- Easier testing
- Swappable implementations

---

# Async / Await

“Don’t block threads during I/O.”

Improves scalability for:
- Database calls
- HTTP requests
- File operations

Not useful for CPU-heavy work.

---

# Repository Pattern

“Abstract data access.”

Separates business logic from persistence logic.

---

# Unit of Work

“Commit multiple DB operations together.”

---

# Middleware (.NET)

“Request pipeline components.”

Examples:
- Authentication
- Logging
- Exception handling
- CORS

---

# Idempotency

“Safe retries without duplication.”

Example:
- User clicks checkout twice
- Only one payment processes

Use:
- Idempotency keys

---

# Message Queues

“Async communication between systems.”

Examples:
- Azure Service Bus
- RabbitMQ
- Kafka

Good for:
- Orders
- Emails
- Inventory updates

---

# Event-Driven Architecture

“Something happened.”

Examples:
- OrderPlaced
- PaymentProcessed
- InventoryReserved

---

# Caching

## Memory Cache
Single server cache.

## Distributed Cache
Shared cache across servers.

Example:
- Redis

## CDN
Caches static files globally.

---

# Horizontal Scaling

“Add more servers.”

Requirements:
- Stateless APIs
- Distributed cache
- Shared storage/database

---

# Microservices

“Small independently deployable services.”

Pros:
- Independent scaling
- Team ownership
- Isolation

Cons:
- Complexity
- Network latency
- Distributed debugging

Senior answer:
“Don’t start with microservices unless complexity justifies it.”

---

# SQL Indexes

“Make reads faster.”

Tradeoff:
- Faster reads
- Slower inserts/updates

---

# Transactions

“Keep multiple DB operations consistent.”

Use when multiple operations must succeed together.

---

# Deadlocks

“Processes waiting on each other.”

Reduce by:
- Short transactions
- Consistent table access order

---

# N+1 Query Problem

“One query triggers many extra queries.”

Common ORM issue.

---

# API Design

## GET
Read data.

## POST
Create data.

## PUT
Replace resource.

## PATCH
Partial update.

## DELETE
Remove resource.

Best practices:
- Pagination
- Validation
- Versioning
- Proper status codes

---

# E-Commerce System Design

## Robust Shopping Cart

Key ideas:
- Stateless APIs
- Redis/session caching
- Persistent carts
- Inventory reservation
- Idempotent checkout
- Queue-based order processing
- Retry failed payments

Senior answer:
“I’d separate cart, checkout, payment, and fulfillment concerns while designing for retries and eventual consistency.”

---

# Azure Quick Hits

## Azure App Service
Hosted web apps/APIs.

## Azure Functions
Serverless event-driven compute.

## Azure Service Bus
Reliable enterprise messaging.

## Azure Storage
Scalable cloud storage.

## Azure Key Vault
Secrets/certificates management.

## Application Insights
Telemetry and monitoring.

---

# Reliability / Production Engineering

## Retry Policy
Retry transient failures.

## Circuit Breaker
Stop hammering failing services.

## Health Checks
Verify app dependencies are functional.

## Observability
Logs, metrics, traces.

---

# Garbage Collection (.NET)

## Gen 0
Short-lived objects.

## Gen 1
Objects surviving Gen 0.

## Gen 2
Long-lived objects.

## LOH (Large Object Heap)
Large allocations.

Memory leak clues:
- Increasing memory usage
- High GC pressure
- Long Gen 2 collections

Tools:
- Application Insights
- dotMemory
- PerfView

---

# Angular Quick Hits

## Components
Reusable UI pieces.

## Services
Shared logic/data access.

## Dependency Injection
Angular heavily uses DI.

## Observables / RxJS
Reactive async event streams.

## Modules
Feature organization (older Angular style).

## Standalone Components
Modern Angular component architecture.

## Templates
HTML views with bindings/directives.

---

# Senior-Level Interview Phrases

## Scaling
“Start simple and evolve architecture as bottlenecks become real.”

---

## Performance
“Find the biggest bottleneck first before optimizing.”

---

## Reliability
“Design assuming downstream systems will eventually fail.”

---

## Leadership
“Senior engineers reduce risk and help other developers make good decisions.”

---

## Modernization
“Incremental modernization is usually safer than full rewrites.”

---

# Common Interview Buzzwords

## Loose Coupling
Components minimally depend on each other.

## High Cohesion
Related responsibilities stay together.

## Separation of Concerns
Keep responsibilities separated.

## DRY
“Don’t Repeat Yourself.”

## KISS
“Keep It Simple, Stupid.”

## YAGNI
“You Aren’t Gonna Need It.”

Avoid premature complexity.
