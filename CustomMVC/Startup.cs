using System.Threading.Tasks;
using WebServer.Server;

namespace CustomMVC
{
    class Startup
    {
        static async Task Main(string[] args)
        {


            var server = new Server("127.0.0.1", 8090);

            await server.Start();
        }
    }
}
