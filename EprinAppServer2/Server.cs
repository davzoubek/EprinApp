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
        private readonly IPAddress _ipAddress;
        private readonly DataManager _dataManager;

        public Server(int port,IPAddress ipAddress, string dataFilePath)
        {
            _port = port;
            _ipAddress = ipAddress;
            _dataManager = new DataManager(dataFilePath);

            SaveServerConfig(ipAddress.ToString(), port);
        }

        private void SaveServerConfig(string ipAddress, int port)
        {
            var config = new { IPAddress = ipAddress, Port = port };
            File.WriteAllText("server_config.json", JsonSerializer.Serialize(config));
        }

        public async Task StartAsync()
        {
            var listener = new TcpListener(_ipAddress, _port);
            listener.Start();
            Console.WriteLine($"Server connected to IP:{_ipAddress} and port is: {_port}");
            //Connected

            var clientTasks = new List<Task>();

            try
            {
                while (true)
                {
                    var client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connetcted");

                    var clientTask = HandleClientAsync(client);
                    clientTasks.Add(clientTask);

                    clientTasks.RemoveAll(t => t.IsCompleted);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accepting client: {ex.Message}");
            }
            finally
            {
                await Task.WhenAll(clientTasks);
                listener.Stop();
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            var buffer = new byte[4096];

            try
            {
                while(true)
                {
                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; //Client disconnected

                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var response = ProcessRequest(request);

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client:{ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected");
            }
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
