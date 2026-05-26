# Observability & Production Operations

## Metrics vs Logs vs Traces

### Metrics
Aggregated numerical measurements.

Examples:
- request duration
- CPU
- cache hit rate
- queue backlog

### Logs
Detailed event records.

### Traces
Follow requests across distributed systems.

---

## P95 / P99 Latency

### P95
95% of requests complete under threshold.

### P99
Shows slower tail latency.

Averages alone hide outliers.

---

## SLI / SLO / SLA

### SLI
Measurement.

### SLO
Target objective.

### SLA
Contractual guarantee.

---

## MTTR
Mean Time To Resolution/Recovery.

Measures incident recovery speed.

---

## Correlation IDs
Correlation IDs trace requests across:
- APIs
- queues
- databases
- services
- dependencies

---

## Health Checks
Health endpoints allow infrastructure and orchestrators to validate system health.

---

## Synthetic Monitoring
Automated scripts simulate users.

Examples:
- login tests
- checkout tests
- API probes

---

## Real User Monitoring
Telemetry from actual users.

---

## Alert Fatigue
Too many low-value alerts desensitize teams and hide critical incidents.

---

## Tail Latency
Tail latency measures slowest requests.

Important because users feel slow outliers.

---

## Production Outage Response

Typical steps:
1. assess scope
2. communicate
3. mitigate impact
4. rollback or patch
5. restore service
6. root cause analysis
7. postmortem

---

## Common Production Issues

### Slow SQL Queries
- missing indexes
- poor execution plans
- lock contention
- large scans

### Memory Leaks
- object retention
- growing heap

### Cascading Failures
Failing dependencies overload downstream systems.

### Retry Storms
Retries amplify outages if not rate limited.

---

## Feature Flags
Feature flags allow risky functionality to be enabled/disabled safely.

---

## Safer Deployments
Use staged environments and fast rollback strategies.

---

## What should ecommerce systems monitor?

- checkout success rate
- payment failures
- queue backlog
- response times
- cache hit/miss
- DB latency
- dependency failures
- error rates
- throughput
