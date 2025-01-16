using EprinAppServer2;
using System.Net;

Console.WriteLine("Enter server IP adress. Default is 127.0.0.1.");
var ipAdressInput = Console.ReadLine();
IPAddress ipAdress;
if(string.IsNullOrEmpty(ipAdressInput))
{
    ipAdress = IPAddress.Any;
}
else if(!IPAddress.TryParse(ipAdressInput, out ipAdress))
{
    Console.WriteLine("Invalid IP address format. Using default IP address");
    ipAdress = IPAddress.Parse("127.0.0.1");
}

Console.WriteLine("Enter server port. Default is 12345.");
var portInput = Console.ReadLine();
int port = string.IsNullOrEmpty(portInput) ? 12345 : int.Parse(portInput);

const string dataFilePath = "people.json";

var server = new Server(port, ipAdress, dataFilePath);
server.Start();
