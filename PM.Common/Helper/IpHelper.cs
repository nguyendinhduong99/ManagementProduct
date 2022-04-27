using System.Net;
using System.Net.Sockets;

namespace PM.Common.Helper
{
    public static class IpHelper
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
