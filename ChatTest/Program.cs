using ChatApp;
using ChatNetwork;

//var server = new ChatServer(new MessageSource(0));
//new Task(() => server.WorkAsync()).Start();
//var client1 = new ChatClient("Vasya", new MessageSource(0));
//new Thread(client1.Start).Start();
//var client2 = new ChatClient("Vitek", new MessageSource(0));
//new Thread(client2.Start).Start();
if (args.Length == 0)
{
    //var messageSource = new MessageSource(12345);
    var server = new ChatServer(new MessageSource());
    server.Work();

}
else if (args.Length == 2)
{
    var client1 = new ChatClient(args[0], new MessageSource(int.Parse(args[1]), "127.0.0.1"));
    client1.Start();
}
else
{
    Console.WriteLine("Error. Input name and port.");
}