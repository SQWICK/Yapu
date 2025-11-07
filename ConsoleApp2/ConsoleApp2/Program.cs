using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Инициализация сервисов
        var externalService = new SlowExternalDataService();
        var aggregatorService = new PageAggregatorService(externalService);

        int testUserId = 123;

        Console.WriteLine("=== ТЕСТ ПОСЛЕДОВАТЕЛЬНОЙ ЗАГРУЗКИ ===");
        var sequentialStopwatch = Stopwatch.StartNew();
        var sequentialResult = await aggregatorService.LoadPageDataSequentialAsync(testUserId);
        sequentialStopwatch.Stop();
        Console.WriteLine(sequentialResult);
        Console.WriteLine($"Общее время выполнения: {sequentialStopwatch.ElapsedMilliseconds} мс\n");

        Console.WriteLine("=== ТЕСТ ПАРАЛЛЕЛЬНОЙ ЗАГРУЗКИ ===");
        var parallelStopwatch = Stopwatch.StartNew();
        var parallelResult = await aggregatorService.LoadPageDataParallelAsync(testUserId);
        parallelStopwatch.Stop();
        Console.WriteLine(parallelResult);
        Console.WriteLine($"Общее время выполнения: {parallelStopwatch.ElapsedMilliseconds} мс\n");

        // Сравнение результатов
        Console.WriteLine("=== ИТОГИ ===");
        Console.WriteLine($"Последовательная загрузка: {sequentialStopwatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Параллельная загрузка:    {parallelStopwatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Выигрыш в производительности: {sequentialStopwatch.ElapsedMilliseconds - parallelStopwatch.ElapsedMilliseconds} мс");
    }
}