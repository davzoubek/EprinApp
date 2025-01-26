using EprinAppServer2;
using System.Net;

Console.WriteLine("Enter server port. Default is 12345.");
var portInput = Console.ReadLine();
int port;
if(string.IsNullOrEmpty(portInput))
{
    port = 12345;
}
else if(!int.TryParse(portInput, out port))
{
    Console.WriteLine("Invalid port format. Using default port 12345");
    port = 12345;
}
else if(port < 1023 || port > 65535)
{
    Console.WriteLine("Port out of range. Using default port 12345.");
    port = 12345;
}

const string dataFilePath = "people.json";

var server = new Server(port, dataFilePath);
await server.StartAsync();
