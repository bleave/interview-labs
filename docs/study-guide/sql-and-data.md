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
- table scans
- poor joins
- bad execution plans
- excessive locking
- non-sargable predicates
- parameter sniffing
- overfetching data

---

## Clustered vs Nonclustered Indexes

### Clustered Index
Determines the physical order of data.

A table typically has one clustered index.

### Nonclustered Index
Separate structure pointing to table rows.

Used to speed up lookups and filtering.

---

## Covering Index
A covering index contains all columns needed for a query.

Benefits:
- avoids key lookups
- improves performance
- reduces IO

---

## Composite Indexes
Indexes containing multiple columns.

Important:
Column order matters.

Example:
```sql
CREATE INDEX IX_Orders_UserId_CreatedDate
ON Orders(UserId, CreatedDate)
```

---

## Index Seek vs Table Scan

### Index Seek
Efficient targeted lookup.

### Table Scan
Reads large portions of table.

Scans become expensive on large datasets.

---

## What are execution plans?
Execution plans show how SQL Server executes queries.

Useful for identifying:
- scans
- expensive joins
- missing indexes
- sort operations
- bad cardinality estimates

---

## What is parameter sniffing?
SQL Server may cache inefficient query plans based on earlier parameter values.

Can cause major performance inconsistencies.

---

## What are non-sargable queries?
Queries that prevent indexes from being used efficiently.

Bad Example:
```sql
WHERE YEAR(OrderDate) = 2025
```

Better:
```sql
WHERE OrderDate >= '2025-01-01'
AND OrderDate < '2026-01-01'
```

---

## What is lock escalation?
SQL Server may escalate many row/page locks into table locks.

Can reduce concurrency under heavy load.

---

## NOLOCK Tradeoffs
NOLOCK can reduce blocking but allows dirty reads and inconsistent data.

Use carefully.

---

## Isolation Levels

### Read Committed
Default isolation level.

### Repeatable Read
Prevents rows from changing during transaction.

### Serializable
Highest isolation with strongest locking.

### Snapshot
Uses row versioning for concurrency.

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

## Connection Pooling
Reuses database connections for efficiency.

---

## Read Replicas
Read replicas allow scaling read workloads separately from writes.

Useful for:
- analytics
- reporting
- ecommerce browsing

---

## OLTP vs OLAP

### OLTP
Transactional systems.

Examples:
- ecommerce checkout
- orders
- payments

### OLAP
Analytical/reporting workloads.

Examples:
- dashboards
- BI reporting
- trend analysis

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
