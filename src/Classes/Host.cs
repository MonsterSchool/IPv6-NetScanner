using System.Net;
using System.Net.NetworkInformation;

class Host
{
    public string hostname { get; set; }
    public IPAddress ipAddress { get; set; }
    public PhysicalAddress physicalAddress { get; set; }
    public string info { get; set; }
}