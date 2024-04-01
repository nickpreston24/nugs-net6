using System.Net.NetworkInformation;

namespace nugsnet6.Services;

// Found: https://gist.github.com/jrusbatch/4211535
public class PortFinderService
{
    private const ushort MIN_PORT = 1;
    private const ushort MAX_PORT = ushort.MaxValue;

    public static int GetFirstAvailablePort(ushort lowerPort = MIN_PORT, ushort upperPort = MAX_PORT)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

        var connectionsEndpoints = ipGlobalProperties.GetActiveTcpConnections().Select(c => c.LocalEndPoint);
        var tcpListenersEndpoints = ipGlobalProperties.GetActiveTcpListeners();
        var udpListenersEndpoints = ipGlobalProperties.GetActiveUdpListeners();
        var portsInUse = connectionsEndpoints.Concat(tcpListenersEndpoints)
            .Concat(udpListenersEndpoints)
            .Select(e => e.Port);

        return Enumerable.Range(lowerPort, ushort.MaxValue - upperPort + 1).Except(portsInUse).FirstOrDefault();
    }
}