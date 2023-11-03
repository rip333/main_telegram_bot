
using System.Reflection.Metadata;

namespace Telegram;
public class PollingService
{
    private readonly IMessageService _messageService;

    private static int _lastUpdateId = 0;

    public PollingService(IMessageService messageService)
    {
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    }

    public async Task PollForUpdatesAsync()
    {
        try
        {
            var updates = await _messageService.GetUpdates(_lastUpdateId);

            if (updates?.result == null) return;
            foreach (var result in updates.result)
            {
                _lastUpdateId = result.update_id;
                await ProcessUpdate(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}. {e.StackTrace}");
        }
    }

    private async Task ProcessUpdate(Result result)
    {
        var text = result.message?.text ?? "";
        Console.WriteLine($"Received message: {text}");
        if (string.IsNullOrEmpty(text)) return;


        if (text.ToLower().Contains("fart"))
        {
            Console.WriteLine($"GENERATING FART NOISE");
            await _messageService.SendAudio(result.message.chat.id, Constants.FART_WITH_REVERB);
        }
    }
}