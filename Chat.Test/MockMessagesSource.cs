using ChatApp;
using ChatNetwork;
using CommonChat.DTO;
using System.Net;
using System.Net.Sockets;

namespace Chat.Test
{
    public class MockMessagesSource : IMessageSource
    {
        private Queue<ChatMessage> _messages = new Queue<ChatMessage>();

        private ChatServer _chatServer;
        private IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private IPEndPoint _udpServerEndPoint;

        public ChatServer ChatServer
        {
            get => _chatServer;
            set => _chatServer = value;
        }

        public void InitializeServer(ChatServer chatServer)
        {
            ChatServer = chatServer;
        }

        public MockMessagesSource()
        {
            _messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Alex" });
            _messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Ivan" });
            _messages.Enqueue(new ChatMessage()
                { Command = Command.Message, FromName = "Alex", ToName = "Ivan", Text = "Hello, Ivan" });
            _messages.Enqueue(new ChatMessage()
                { Command = Command.Message, FromName = "Ivan", ToName = "Alex", Text = "Hello, Alex" });
        }

        public IPEndPoint CreateNewIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 0);
        }

        public ChatMessage Receive(ref IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;

            if (_messages.Count == 0)
            {
                ChatServer.Stop();
                return null;
            }

            return _messages.Dequeue();
        }

        public void SendMessage(ChatMessage chatMessage, IPEndPoint ipEndPoint)
        {
        }

        public IPEndPoint GetServerIPEndPoint()
        {
            return new IPEndPoint(_udpServerEndPoint.Address, _udpServerEndPoint.Port);
        }
    }
}