using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace AspBot
{
    public class BotService : BackgroundService
    {
        private readonly TelegramBotClient _botClient;

        public BotService()
        {
            _botClient = new TelegramBotClient("7182360517:AAFMJseBoasM9m0P0LygoFiz2jX0ufZV5fU");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions(),
                stoppingToken
            );

            var me = await _botClient.GetMeAsync(stoppingToken);
            Console.WriteLine($"Start listening for @{me.Username}");

            // Wait for the bot to stop
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.Text is { } messageText)
            {
                var chatId = update.Message.Chat.Id;
                await botClient.SendTextMessageAsync(chatId, "Echo: " + messageText, cancellationToken: cancellationToken);
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.ToString()}");
            return Task.CompletedTask;
        }
    }
}
