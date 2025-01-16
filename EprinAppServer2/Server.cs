using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace EprinAppServer2
{
    public class Server
    {
        private readonly int _port;
        private readonly IPAddress _ipAdress;
        private readonly DataManager _dataManager;

        public Server(int port,IPAddress ipAddress, string dataFilePath)
        {
            _port = port;
            _ipAdress = ipAddress;
            _dataManager = new DataManager(dataFilePath);
        }

        public void Start()
        {
            var listener = new TcpListener(_ipAdress, _port);
            listener.Start();
            Console.WriteLine($"Server connected to IP:{_ipAdress} and port is: {_port}");
            //Connected

            while(true)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }
        }

        private void HandleClient(object clientObject)
        {
            using var client = (TcpClient)clientObject;
            using var stream = client.GetStream();
            var buffer = new byte[4096];

            while(true)
            {
                try
                {
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var response = ProcessRequest(request);

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
                catch
                {
                    break;
                }
            }
            Console.WriteLine("Client disonnected");
            //Disconnected
        }

        private string ProcessRequest(string request)
        {
            try
            {
                var parts = request.Split('|');
                var command = parts[0];

                return command switch
                {
                    "GET" => JsonSerializer.Serialize(_dataManager.GetAllPeople()),
                    "ADD" => _dataManager.AddPerson(parts[1], parts[2]).Id.ToString(),
                    "UPDATE" => _dataManager.UpdatePerson(int.Parse(parts[1]), parts[2], parts[3]) ? "OK" : "ERROR",
                    "DELETE" => _dataManager.DeletePerson(int.Parse(parts[1])) ? "OK" : "ERROR",
                    _ => "UNKNOWN_COMMAND",
                };
            }
            catch
            {
                return "ERROR";
            }
        }
    }
}
