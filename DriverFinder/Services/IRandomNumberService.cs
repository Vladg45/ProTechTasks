using System.Text.Json;

namespace DriverFinder.Services
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumberAsync(int min, int max);
    }

    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly Random _localRandom;
        private readonly ILogger<RandomNumberService> _logger;

        public RandomNumberService(HttpClient httpClient, ILogger<RandomNumberService> logger)
        {
            _httpClient = httpClient;
            _localRandom = new Random();
            _logger = logger;
        }

        public async Task<int> GetRandomNumberAsync(int min, int max)
        {
            try
            {
                // Получаем случайное число из удаленного API
                var response = await _httpClient.GetAsync($"http://www.randomnumberapi.com/api/v1.0/random?min={min}&max={max}&count=1");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var numbers = JsonSerializer.Deserialize<int[]>(content);

                    if (numbers != null && numbers.Length > 0)
                    {
                        return numbers[0];
                    }
                }

                _logger.LogWarning("Не удалось получить случайное число из удаленного API, используем локальный генератор");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении случайного числа из удаленного API");
            }

            // Получаем случайное число с помощью random
            return _localRandom.Next(min, max + 1);
        }
    }
}
