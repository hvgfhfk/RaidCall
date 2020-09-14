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

        int among_count = 0;

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
            await _client.LoginAsync(TokenType.Bot, "NzUxMzU5OTg0MzMxMjU5OTU1.X1H8gw.IVE0uPvFNcPthQuQ0Qx61C1SfSA"); // CALL 봇
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
            var _U_Chat = message.Channel.Id == 682591375036121258;
            var _M_Chat = message.Channel.Id == 692450471813578814;
            var _S_Chat = message.Channel.Id == 747345150782603286;

            var among = message.Channel.Id == 753241021692903435;

            if (message.Content == "@ㄹㅇㄷ")
            {
                if(_U_Chat)
                {
                    for(int i = 0; i < 5; i++)
                    {   await message.Channel.SendMessageAsync("@everyone 레이드 레이드"); }  //@everyone 레이드 레이드
                }
                else if(_M_Chat)
                {
                    for (int i = 0; i < 5; i++)
                    { await message.Channel.SendMessageAsync("@everyone 레이드 레이드"); }
                }
                else if(_S_Chat)
                {
                    for (int i = 0; i < 5; i++)
                    { await message.Channel.SendMessageAsync("@everyone 레이드 레이드"); }
                }
                else
                {
                    await message.DeleteAsync();
                }
            }

            if(among)
            {
                if (message.Content == "@ㅇㅁ")
                {
                    await message.Channel.SendMessageAsync("@everyone 어몽어스 하실분 . 쳐주세요");
                    among_count = 1;
                }
                else if (message.Content == ".")
                {
                    among_count = among_count + 1;
                    await message.Channel.SendMessageAsync(among_count + " / 10");
                    if (among_count == 10)
                    {
                        message.Channel.SendMessageAsync("인원이 꽉 찾습니다.");
                        among_count = 0;
                    }
                    else if (among_count > 10)
                    {
                        message.Channel.SendMessageAsync("인원이 꽉 찾습니다.");
                    }
                }
                else if (message.Content == "@ㄴㄱ")
                {
                    among_count = among_count - 1;
                    await message.Channel.SendMessageAsync("한명이 게임에서 나갔습니다. " + among_count + " / 10");
                    if(among_count < 1)
                    {
                        message.Channel.SendMessageAsync("인원이 2명 이상이 아닙니다.");
                        among_count = 1;
                    }
                }
                else if(message.Content == "@ㅁㄱ")
                {
                    await message.Channel.SendMessageAsync("인원 꽉참 (현재 인원 : " + among_count + " / 10 )");
                    await message.Channel.SendMessageAsync("인원수를 초기화합니다");
                    among_count = 0;
                }
                else if(message.Content == "@ㅊㄱ")
                {
                    await message.Channel.SendMessageAsync("어몽 더 하실분 . 쳐주세요");
                }
            }
        }
    }
} 