using EprinAppServer2;
using System.Net;

Console.WriteLine("Enter server IP adress. Default is 127.0.0.1.");
var ipAddressInput = Console.ReadLine();
IPAddress ipAddress;
if(string.IsNullOrEmpty(ipAddressInput))
{
    ipAddress = IPAddress.Any;
}
else if(!IPAddress.TryParse(ipAddressInput, out ipAddress))
{
    Console.WriteLine("Invalid IP address format. Using default IP address");
    ipAddress = IPAddress.Parse("127.0.0.1");
}

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
else if(port < 1 || port > 65535)
{
    Console.WriteLine("Port out of range. Using default port 12345.");
    port = 12345;
}

const string dataFilePath = "people.json";

var server = new Server(port, ipAddress, dataFilePath);
server.Start();
