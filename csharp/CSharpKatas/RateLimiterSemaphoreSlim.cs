/*
WHAT:
  Concurrency limiter: cap the number of operations running at the same time.

IMPORTANT CLARIFICATION:
  This is NOT time-based rate limiting (e.g., "100 requests/second").
  This IS concurrency throttling (e.g., "max 5 in-flight operations").

HOW:
  Use SemaphoreSlim(maxConcurrency):
    - WaitAsync() before starting work (acquire a slot)
    - Run the operation
    - Release() in finally (always return the slot)

WHY SemaphoreSlim (vs lock):
  - lock is not await-friendly (you should not await while holding a lock)
  - SemaphoreSlim has WaitAsync, so callers can await without blocking threads
  - Allows N concurrent entries (not just 1)

EDGE CASES:
  - Always release in finally
  - Support CancellationToken
  - If they truly want time-based limiting: token bucket/sliding window

COMPLEXITY:
  Overhead per operation: O(1)
*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class RateLimiterSemaphoreSlim
{
    public static void Run()
    {
        Console.WriteLine("Talk track: Concurrency limiter with SemaphoreSlim (max N in-flight). Not time-based limiting.");
        Console.WriteLine();

        RunAsync().GetAwaiter().GetResult();
    }

    private static async Task RunAsync()
    {
        var maxConcurrency = 5;
        var limiter = new ConcurrencyLimiter(maxConcurrency);

        var sw = Stopwatch.StartNew();

        var tasks = Enumerable.Range(1, 20).Select(async i =>
        {
            await limiter.RunAsync(async ct =>
            {
                Console.WriteLine($"[{sw.ElapsedMilliseconds,5}ms] START {i,2}");

                // Simulate variable work time
                await Task.Delay(Random.Shared.Next(250, 900), ct);

                Console.WriteLine($"[{sw.ElapsedMilliseconds,5}ms] END   {i,2}");
            });
        });

        await Task.WhenAll(tasks);

        Console.WriteLine();
        Console.WriteLine($"Configured max concurrency: {maxConcurrency}");
        Console.WriteLine($"Observed max concurrency:  {limiter.ObservedMaxConcurrent}");
        Console.WriteLine("Done.");
    }

    private sealed class ConcurrencyLimiter
    {
        private readonly SemaphoreSlim _semaphore;

        // Demo counters (kept intentionally simple)
        private readonly object _gate = new();
        private int _inFlight;
        private int _observedMax;

        public int ObservedMaxConcurrent
        {
            get { lock (_gate) return _observedMax; }
        }

        public ConcurrencyLimiter(int maxConcurrency)
        {
            if (maxConcurrency <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrency));
            _semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        }

        public async Task RunAsync(Func<CancellationToken, Task> action, CancellationToken ct = default)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));

            // Acquire a concurrency slot (awaits if none are available)
            await _semaphore.WaitAsync(ct);

            try
            {
                // Safe, tiny critical section: we do NOT await inside the lock
                lock (_gate)
                {
                    _inFlight++;
                    if (_inFlight > _observedMax) _observedMax = _inFlight;
                }

                // Execute work while holding a semaphore slot
                await action(ct);
            }
            finally
            {
                // Safe, tiny critical section: we do NOT await inside the lock
                lock (_gate)
                {
                    _inFlight--;
                }

                // Always release the slot
                _semaphore.Release();
            }
        }
    }
}
