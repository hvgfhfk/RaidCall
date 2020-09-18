using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.SqlServer.Server;
using System.Timers;
using System.ComponentModel;

namespace Raidcall
{
    class RaidCall
    {
        private readonly DiscordSocketClient _client;

        int Among_min = 0;
        int Among_Max = 10;

        static void Main(string[] args)
        { new RaidCall().MainAsync().GetAwaiter().GetResult(); }

        public RaidCall()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            await _client.SetStatusAsync(UserStatus.DoNotDisturb); // 봇의 상태 메세지 변경
            await _client.SetGameAsync("수성준비 ");
          //  await _client.LoginAsync(TokenType.Bot, "NzUxMzU5OTg0MzMxMjU5OTU1.X1H8gw.IVE0uPvFNcPthQuQ0Qx61C1SfSA"); // CALL 봇
            await _client.LoginAsync(TokenType.Bot, "NzU2NDM5ODc3OTYwMzM1Mzcw.X2R3iA.IHu_1-gDeN7gja27RhyYOcBKkA8");
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        private Task Ready()
        {
            Console.WriteLine($"{_client.CurrentUser} 연결됨!");

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(SocketMessage message)
        {
            var Text_Channel__01chl = message.Channel.Id == 682591375036121258;
            var Text_Channel__12chl = message.Channel.Id == 692450471813578814;
            var Text_Channel__23chl = message.Channel.Id == 747345150782603286;
            var Text_Channel__34chl = message.Channel.Id == 756440639716982804;

            var Text_Channel__Among = message.Channel.Id == 756440639716982804;


             if (Text_Channel__01chl || Text_Channel__12chl || Text_Channel__23chl)
             {
                 if(message.Content == "@ㄹㅇㄷ")
                 {
                     await message.DeleteAsync();
                 }
             }
             else if(message.Content == "@ㄹㅇㄷ")
             {
                 for (int i = 0; i < 5; i++)
                 {
                     await message.Channel.SendMessageAsync("@everyone 레이드!!");
                 }
             } 

            if (Text_Channel__Among)
            {
                if (message.Content == "@ㅇㅁ")
                {
                    await message.Channel.SendMessageAsync("@everyone 어몽 하실분 있나요");
                    Among_min = 0;
                }
                else if (message.Content == ".")
                {
                    if(Among_min < Among_Max)
                    {
                        Among_min++;
                        await message.Channel.SendMessageAsync(Among_min + " / " +  Among_Max);
                    }
                    else
                    {
                        await message.Channel.SendMessageAsync("자리가 꽉 찾습니다.");
                    }
                }
                else if(message.Content == "@ㅊㄱ")
                {
                    await message.Channel.SendMessageAsync(Among_Max - Among_min + "자리 남습니다. 더 하실분 . 쳐주세요");
                }
            }
        }
    }
} 