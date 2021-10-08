using NetDotNet.Layer_2;
using PacketDotNet;
using SharpPcap;
using System.Net;
using System.Net.NetworkInformation;

class PacketBuilder
{
    private string[] multiAddr = { "FF01::1", "FF01::2", "FF02::1", "FF02::2" };

    public Packet buildPacket(ILiveDevice pLiveDevice, IPAddress pLocalIPv6, int pMultiAddrIndex)
    {
        if (pLiveDevice.MacAddress != null)
        {
            EthernetPacket ethernetPacket = new EthernetPacket(pLiveDevice.MacAddress, PhysicalAddress.Parse("33-33-00-00-00-01"), EthernetType.IPv6);

            ICMPv6Layer icmpv6Packet = new ICMPv6Layer()
            {
                type = 0x80,
                code = 0x00,
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

            // Setting Checksum
            switch (pMultiAddrIndex)
            {
                case 0:
                    icmpv6Packet.checksum = new byte[] { 0xed, 0x31 };
                    break;
                case 1:
                    icmpv6Packet.checksum = new byte[] { 0xed, 0x30 };
                    break;
                case 2:
                    icmpv6Packet.checksum = new byte[] { 0xed, 0x30 };
                    break;
                case 3:
                    icmpv6Packet.checksum = new byte[] { 0xed, 0x2f };
                    break;
            };            
            
            // Building Packet
            byte[] icmpv6LayerBytes = icmpv6Packet.buildLayer();

            IPv6Packet ipv6Packet = new IPv6Packet(pLocalIPv6, IPAddress.Parse(multiAddr[pMultiAddrIndex]))
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
}
