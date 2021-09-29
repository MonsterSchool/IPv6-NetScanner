using NetDotNet.Layer_2;
using PacketDotNet;
using SharpPcap;
using System.Net;
using System.Net.NetworkInformation;

class PingPacket
{
    public Packet buildPacket(ILiveDevice pLiveDevice, string pMulticastScopeAddress)
    {
        if (pLiveDevice.MacAddress != null)
        {
            EthernetPacket ethernetPacket = new EthernetPacket(pLiveDevice.MacAddress, PhysicalAddress.Parse("33-33-00-00-00-01"), EthernetType.IPv6);

            ICMPv6Layer icmpv6Packet = new ICMPv6Layer()
            {
                type = 0x80,
                code = 0x00,
                checksum = new byte[] { 0x00, 0x01 },
                identifier = new byte[] { 0x00, 0x01 },
                sequence = new byte[] { 0x00, 0x01 },

                icmpv6Option = new byte[]
                {
                  0x61, 0x62, 0x63, 0x64, 0x65, 0x66,
                  0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c,
                  0x6d, 0x6e, 0x6f, 0x70, 0x71, 0x72,
                  0x73, 0x74, 0x75, 0x76, 0x77, 0x61,
                  0x62, 0x63, 0x64, 0x65, 0x66, 0x67,
                  0x68, 0x69
                },
            };

            byte[] icmpv6LayerBytes = icmpv6Packet.buildLayer();

            IPv6Packet ipv6Packet = new IPv6Packet(getLocalIp(), IPAddress.Parse(pMulticastScopeAddress))
            {
                NextHeader = ProtocolType.IcmpV6,
                PayloadLength = (ushort)icmpv6LayerBytes.Length,
                PayloadData = icmpv6LayerBytes,
            };

            ethernetPacket.PayloadPacket = ipv6Packet;
            return ethernetPacket;
        }
        return null;
    }

    private IPAddress getLocalIp()
    {
        string strHostName = Dns.GetHostName(); ;
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        return IPAddress.Parse(addr[0].ToString().Split('%')[0]);
    }
}