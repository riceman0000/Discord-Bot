using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBotTest1
{
    class Program
    {
        private DiscordSocketClient _client;
        public static CommandService _commands;
        public static IServiceProvider _services;

        static void Main(string[] args)
          => new Program().MainAsync().GetAwaiter().GetResult();
        /// <summary>
        /// トークン取得とかログイン処理
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            _client.Log += Log;
            _commands = new CommandService();
            _services = new ServiceCollection().BuildServiceProvider();
            _client.MessageReceived += CommandRecieved;

            string token = "";//個別トークンの設定
            await _commands.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), _services);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        /// <summary>
        /// コマンド処理
        /// </summary>
        /// <param name="messageParam"></param>
        /// <returns></returns>
        private async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;

            Console.WriteLine("{0} {1}:{2}", message.Channel.Name, message.Author.Username, message);
            if (message == null)
                return;

            if (message.Author.IsBot)
                return;

            var context = new CommandContext(_client, message);

            var CommandContext = message.Content;
            //以下コマンド
            if (CommandContext == "おはよう")
            {
                await message.Channel.SendMessageAsync("オッスオッス!!!早起きえらい!!!");
            }
        }
        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
