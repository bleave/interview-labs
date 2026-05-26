# SQL & Data Systems

## IEnumerable vs IQueryable

### IEnumerable
Operates in memory.

### IQueryable
Builds database queries executed remotely.

---

## Deferred Execution
LINQ queries often execute only when enumerated.

---

## First vs FirstOrDefault

### First()
Throws if empty.

### FirstOrDefault()
Returns null/default.

---

## What causes slow SQL queries?

- missing indexes
- large scans
- poor joins
- bad execution plans
- excessive locking
- non-sargable predicates

---

## Connection Pooling
Reuses database connections for efficiency.

---

## Transactions
Transactions ensure operations fully complete or rollback.

---

## ACID

### Atomicity
All or nothing.

### Consistency
Data remains valid.

### Isolation
Concurrent operations remain isolated.

### Durability
Committed data survives failure.

---

## Optimistic vs Pessimistic Locking

### Optimistic
Check for changes before update.

### Pessimistic
Lock data during update.

---

## What is a race condition?
Concurrent updates produce inconsistent state.

---

## What is a deadlock?
Transactions block each other indefinitely.

---

## Elasticsearch
Elasticsearch is a distributed search engine optimized for fast search/query workloads.

Common ecommerce uses:
- product search
- autocomplete
- filtering
- faceting

---

## Redis
Redis is an in-memory distributed cache.

Common uses:
- caching
- distributed state
- sessions
- throttling

---

## Cache Invalidation
Cached data must eventually refresh.

Common methods:
- TTL
- expiration
- event invalidation

---

## Cache Hits vs Misses

### Hit
Requested data already cached.

### Miss
Data retrieved from source.
