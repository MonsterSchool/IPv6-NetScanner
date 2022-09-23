using PacketDotNet;
using PacketDotNet.Utils;
using SharpPcap;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

class PacketBuilder
{
    private string[] multiAddr = {"FF02::1", "FF02::2" };

    public Packet buildPacket(ILiveDevice pLiveDevice, IPAddress pLocalIPv6, int pMultiAddrIndex)
    {
        if (pLiveDevice.MacAddress != null)
        {
            EthernetPacket ethernetPacket = new EthernetPacket(pLiveDevice.MacAddress, PhysicalAddress.Parse("33-33-00-00-00-01"), EthernetType.IPv6);

            IcmpV6Packet icmpv6Packet = new IcmpV6Packet(new ByteArraySegment(new byte[40]))
            {
                Type = IcmpV6Type.EchoRequest,
                PayloadData = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwabcdefghi")
            };

            IPv6Packet ipv6Packet = new IPv6Packet(pLocalIPv6, IPAddress.Parse(multiAddr[pMultiAddrIndex]));

            ethernetPacket.PayloadPacket = ipv6Packet;
            ipv6Packet.PayloadPacket = icmpv6Packet;

            return ethernetPacket;
        }
        return null;
    }

    /*
    private ushort ComputeChecksum(byte[] payLoad)
    {
        uint xsum = 0;
        ushort shortval = 0, hiword = 0, loword = 0;

        // Sum up the 16-bits
        for (int i = 0; i < payLoad.Length / 2; i++)
        {
            hiword = (ushort)(((ushort)payLoad[i * 2]) << 8);
            loword = (ushort)payLoad[(i * 2) + 1];
            shortval = (ushort)(hiword | loword);
            xsum = xsum + (uint)shortval;
        }
        // Pad if necessary

        if ((payLoad.Length % 2) != 0)
        {
            xsum += (uint)payLoad[payLoad.Length - 1];
        }

        xsum = ((xsum >> 16) + (xsum & 0xFFFF));
        xsum = (xsum + (xsum >> 16));
        shortval = (ushort)(~xsum);

        return shortval;
    }
    */
}
