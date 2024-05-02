using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ChatClient_Archive
    {
        private UdpClient _client;
        private IPEndPoint _ipEndPoint;

        public ChatClient_Archive(string serverIp, int serverPort)
        {
            _client = new UdpClient(); ;
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
        }

        public void SendMessage(ChatMessage message)
        {
            var data = Encoding.UTF8.GetBytes(message.ToJson());
            _client.Send(data, data.Length, _ipEndPoint);
        }

        public void StartChat()
        {
            try
            {
                Console.WriteLine("Введите ваше имя:");
                var userName = Console.ReadLine();

                SendMessage(new ChatMessage { Command = Command.Register, FromName = userName });

                Console.WriteLine("Введите имя получателя:");
                var toName = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine("Введите сообщение.");
                    var text = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(text))
                        break;

                    SendMessage(new ChatMessage() { FromName = userName, ToName = toName, Text = text, Command = Command.Message });
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
