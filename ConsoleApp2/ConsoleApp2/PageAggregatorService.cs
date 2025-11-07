using System.Threading.Tasks;

public class PageAggregatorService : IPageAggregator
{
    private readonly IExternalDataService _externalDataService;

    // Внедрение зависимости через конструктор (DI-паттерн)
    public PageAggregatorService(IExternalDataService externalDataService)
    {
        _externalDataService = externalDataService;
    }

    public async Task<PagePayload> LoadPageDataSequentialAsync(int userId)
    {
        // Последовательные вызовы. Каждый `await` приостанавливает метод до завершения задачи.
        var userData = await _externalDataService.GetUserDataAsync(userId);
        var orderData = await _externalDataService.GetUserOrdersAsync(userId);
        var adData = await _externalDataService.GetAdsAsync();

        return new PagePayload
        {
            UserData = userData,
            OrderData = orderData,
            AdData = adData
        };
    }

    public async Task<PagePayload> LoadPageDataParallelAsync(int userId)
    {
        // Параллельные вызовы. Задачи запускаются одновременно.
        var userDataTask = _externalDataService.GetUserDataAsync(userId);
        var orderDataTask = _externalDataService.GetUserOrdersAsync(userId);
        var adDataTask = _externalDataService.GetAdsAsync();

        // Ожидание завершения ВСЕХ задач параллельно.
        await Task.WhenAll(userDataTask, orderDataTask, adDataTask);

        // В этот момент все задачи завершены, и мы можем безопасно получить их результаты.
        return new PagePayload
        {
            UserData = userDataTask.Result,
            OrderData = orderDataTask.Result,
            AdData = adDataTask.Result
        };
    }
}