using System;
using System.Threading.Tasks;

public class SlowExternalDataService : IExternalDataService
{
    public async Task<string> GetUserDataAsync(int userId)
    {
        Console.WriteLine($"[User Service] Запрос данных пользователя {userId} начат...");
        await Task.Delay(2000); // Имитация задержки в 2000 мс
        string result = $"Данные пользователя #{userId}";
        Console.WriteLine($"[User Service] Запрос данных пользователя {userId} завершен.");
        return result;
    }

    public async Task<string> GetUserOrdersAsync(int userId)
    {
        Console.WriteLine($"[Order Service] Запрос заказов пользователя {userId} начат...");
        await Task.Delay(3000); // Имитация задержки в 3000 мс
        string result = $"Список заказов пользователя #{userId}";
        Console.WriteLine($"[Order Service] Запрос заказов пользователя {userId} завершен.");
        return result;
    }

    public async Task<string> GetAdsAsync()
    {
        Console.WriteLine($"[Ad Service] Запрос рекламного контента начат...");
        await Task.Delay(1000); // Имитация задержки в 1000 мс
        string result = "Акционные предложения недели";
        Console.WriteLine($"[Ad Service] Запрос рекламного контента завершен.");
        return result;
    }
}