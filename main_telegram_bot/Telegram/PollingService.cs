
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
            Random r = new Random();
            int roll = r.Next(1, 101);
            Console.WriteLine($"Rolled a {roll}");
            string fartSound = Constants.FART_WITH_REVERB; // Default sound

            if (roll <= 15)
            {
                // Fart noise with reverb is already set as the default sound.
            }
            else if (roll <= 25)
            {
                fartSound = Constants.FART;
            }
            else if (roll <= 40)
            {
                fartSound = Constants.LIL_SPUTTER;
            }
            else if (roll <= 55)
            {
                fartSound = Constants.BALLOON_PINCH;
            }
            else if (roll <= 70)
            {
                fartSound = Constants.HUSBAND_AND_WIFE;
            }
            else if (roll <= 85)
            {
                fartSound = Constants.SQUEEKER;
            }
            else if (roll <= 100)
            {
                fartSound = Constants.SMALL_BUT_MIGHTY;
            }

            await _messageService.SendAudio(result.message.chat.id, fartSound);
        }
    }
}