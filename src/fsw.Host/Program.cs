using Newtonsoft.Json;
using fsw;
using System.Runtime.InteropServices;
namespace fsw.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            fsw.fswCommandHost host = new fsw.fswCommandHost();
            host.Execute("#123");
        }
    }
}
