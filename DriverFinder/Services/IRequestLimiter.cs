namespace DriverFinder.Services
{
    public interface IRequestLimiter
    {
        bool TryAcquire();
        void Release();
        int GetCurrentRequests();
        int GetLimit();
    }

    public class RequestLimiter : IRequestLimiter, IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<RequestLimiter> _logger;
        private int _currentRequests;

        public RequestLimiter(int parallelLimit, ILogger<RequestLimiter> logger)
        {
            _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
            _logger = logger;
            _currentRequests = 0;
        }

        public bool TryAcquire()
        {
            bool acquired = _semaphore.Wait(0);
            if (acquired)
            {
                Interlocked.Increment(ref _currentRequests);
                _logger.LogDebug($"Запрос приобретен. Текущие запросы: {_currentRequests}");
            }
            else
            {
                _logger.LogWarning($"Лимит параллельных запросов достигнут. Текущие: {_currentRequests}, Лимит: {GetLimit()}");
            }

            return acquired;
        }

        public void Release()
        {
            _semaphore.Release();
            Interlocked.Decrement(ref _currentRequests);
            _logger.LogDebug($"Запрос освобожден. Текущие запросы: {_currentRequests}");
        }

        public int GetCurrentRequests()
        {
            return _currentRequests;
        }

        public int GetLimit()
        {
            return _semaphore.CurrentCount + _currentRequests;
        }

        public void Dispose()
        {
            _semaphore?.Dispose();
        }
    }
}