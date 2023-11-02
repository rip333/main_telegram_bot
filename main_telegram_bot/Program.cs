using Telegram;

Console.WriteLine("Welcome to New York.");

var botToken = await ConfigUtility.GetBotConfig();
var httpClient = new HttpClient();
var pollingService = new PollingService(
        new MessageService(botToken.TelegramToken, httpClient)
    );

while (true)
{
    await pollingService.PollForUpdatesAsync();
    await Task.Delay(250);
}