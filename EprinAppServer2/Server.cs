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
        private readonly DataManager _dataManager;
        private CancellationTokenSource _cancellationTokenSource;

        public Server(int port, string dataFilePath)
        {
            _port = port;
            _dataManager = new DataManager(dataFilePath);
        }

        public async Task StartAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"Server port is: {_port}");
            //Connected

            var clientTasks = new List<Task>();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (listener.Pending())
                    {
                        var client = await listener.AcceptTcpClientAsync();
                        Console.WriteLine("Client connected");
                        var clientTask = HandleClientAsync(client, cancellationToken);
                        clientTasks.Add(clientTask);
                    }
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accepting client: {ex.Message}");
            }
            finally
            {
                listener.Stop();
                Console.WriteLine("Server stopping...");

                await Task.WhenAll(clientTasks);
                Console.WriteLine("All client tasks completed");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
        {

            try
            {
                using var stream = client.GetStream();
                var buffer = new byte[4096];

                while (!cancellationToken.IsCancellationRequested)
                {
                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (bytesRead == 0) break; //Client disconnects when client ends connection

                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var response = ProcessRequest(request);

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length, cancellationToken);
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
