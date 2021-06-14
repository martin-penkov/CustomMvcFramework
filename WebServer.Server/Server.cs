using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Server
{
    public class Server
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public Server(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, port);
        }

        public async Task Start()
        {
            listener.Start();
            Console.WriteLine($"Http server started on port {port}");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                Console.WriteLine("waiting for connection");

                var tcpClient = await listener.AcceptTcpClientAsync();
                Console.WriteLine("connection established");
                var networkStream = tcpClient.GetStream();
                await ReadRequest(networkStream);

                await WriteRequest(networkStream);
                tcpClient.Close();
            }
        }


        private async Task ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            Console.WriteLine(requestBuilder);
        }

        private async Task WriteRequest(NetworkStream networkStream)
        {
            var content = "Random text";

            var response = $@"HTTP/1.1 200 OK
Content-Length: {content.Length}

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes);
        }
    }
}
