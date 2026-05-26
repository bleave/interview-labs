# Ecommerce & Distributed Systems

## What is idempotency?
Idempotency means repeated identical requests produce the same result without unintended duplicate side effects.

### Ecommerce Example
Retrying checkout should not charge the customer twice.

### Common Implementations
- idempotency keys
- transaction IDs
- duplicate detection
- payment attempt tracking

---

## What is a webhook?
A webhook is an HTTP callback/event notification where one system pushes event data to another system asynchronously.

### Typical Architecture
Webhook -> API endpoint -> Queue -> Background processing

---

## ASP.NET Core Webhook Example

```csharp
[ApiController]
[Route("api/webhooks")]
public class WebhooksController : ControllerBase
{
    [HttpPost("payment")]
    public async Task<IActionResult> PaymentWebhook([FromBody] PaymentWebhookDto dto)
    {
        _logger.LogInformation($"Received payment event {dto.TransactionId}");

        await _serviceBus.PublishAsync(dto);

        return Ok();
    }
}
```

---

## Queue vs Topic

### Queue
FIFO processing where messages are consumed once.

### Topic
Publish/subscribe fan-out messaging.

---

## What is a DLQ?
Dead-letter queue stores poison messages that repeatedly fail processing.

---

## What is a poison message?
A poison message repeatedly fails due to invalid payload or unrecoverable processing conditions.

---

## What is PeekLock?
PeekLock locks a message temporarily during processing.

Message removed only after successful completion.

---

## Why use queues?
Queues improve:
- scalability
- retries
- durability
- fault tolerance
- decoupling
- async workflows

---

## What is eventual consistency?
Distributed systems may not become immediately consistent but converge over time.

### Ecommerce Example
Payment succeeds immediately while inventory and analytics update asynchronously.

---

## Fault-Tolerant Checkout Design

Key concepts:
- tokenize payment data
- validate inventory
- create payment attempt
- idempotency keys
- async processing
- retries + DLQ
- monitoring
- observability

---

## Common Async Ecommerce Tasks
- fulfillment
- inventory updates
- fraud processing
- analytics
- email notifications
