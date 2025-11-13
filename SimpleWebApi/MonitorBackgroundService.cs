namespace SimpleWebApi
{
    public class MonitorBackgroundService:BackgroundService
    {
        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("https://simpleredmine.nexdev.net")
        };
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response =await Client.GetAsync("/", stoppingToken);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync(stoppingToken);

                Console.WriteLine($"Get {content.Length}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
