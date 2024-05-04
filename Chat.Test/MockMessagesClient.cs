using ChatApp;
using ChatNetwork;
using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Test
{
    public class MockMessagesClient : IMessageSource
    {
        private Queue<ChatMessage> _messages = new();

        private ChatClient _chatClient;
        private UdpClient _udpClient;
        private IPEndPoint _udpServerEndPoint;

        public ChatClient ChatClient
        {
            get => _chatClient;
            set => _chatClient = value;
        }


        public void InitializeClient(ChatClient chatClient)
        {
            ChatClient = chatClient;
        }

        public MockMessagesClient(int port, string adress = "127.0.0.1", int portServer = 12345)
        {
            _udpClient = new UdpClient(port);
            _udpServerEndPoint = new IPEndPoint(IPAddress.Parse(adress), portServer);
            _messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Leonid" });
            //_messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Dimac" });
            //_messages.Enqueue(new ChatMessage()
            //{ Command = Command.Message, FromName = "Leonid", ToName = "Dimac", Text = "Hello, Dimac" });
            _messages.Enqueue(new ChatMessage()
            { Command = Command.Message, FromName = "Leonid", ToName = "Leonid", Text = "Hello, Leonid" });
            //_messages.Enqueue(new ChatMessage()
            //    { Command = Command.Confirmation, FromName = "Leonid", ToName = "Leonid" });
        }

        public IPEndPoint CreateNewIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        }

        public ChatMessage Receive(ref IPEndPoint ipEndPoint)
        {
            //return _messages.Dequeue();
            return new ChatMessage();
        }

        public void SendMessage(ChatMessage chatMessage, IPEndPoint ipEndPoint)
        {
            if (_messages.Count > 0)
            {
                var message = _messages.Dequeue();
                byte[] data = Encoding.UTF8.GetBytes(message.ToJson());
                _udpClient.Send(data, data.Length, _udpServerEndPoint);
            }
            else
            {
                ChatClient.Stop();
            }

            
        }

        public IPEndPoint GetServerIPEndPoint()
        {
            return new IPEndPoint(_udpServerEndPoint.Address, _udpServerEndPoint.Port);
        }
    }
}