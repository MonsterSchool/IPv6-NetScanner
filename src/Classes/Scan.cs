using IPv6_Scanner;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Threading;

class Scan
{
    private MainForm mainForm;
    private PingPacket ping = new PingPacket();
    private ILiveDevice liveDevice;
    private List<Host> hostList = new List<Host>();

    /// <summary>
    /// Indicates whether a scan is active
    /// </summary>
    public bool scanStatus = false;

    /// <summary>
    /// Starts an endless network scan. This can be stopped via the stopScan method.
    /// </summary>
    /// <param name="pLiveDevice">The network device to be used for the scan.</param>
    public void scanNetwork(MainForm pMainForm, ILiveDevice pLiveDevice, string pMulticastAddress)
    {
        mainForm = pMainForm;
        liveDevice = pLiveDevice;

        liveDevice.OnPacketArrival +=
            new PacketArrivalEventHandler(device_OnPacketArrival);

        scanStatus = true;
        liveDevice.StartCapture();

        Thread scanThread = new Thread(()=> threadMethod(pMulticastAddress));
        scanThread.Start();
    }    
    
    /// <summary>
    /// 
    /// </summary>
    private void threadMethod(string pMulticastAddress)
    {
        while (scanStatus)
        {
            performNetworkscan(pMulticastAddress);
            Thread.Sleep(5000);
        }
    }
    
    /// <summary>
    /// Builds an ICMPv6 ping request packet and sends it over the specified network gateway.
    /// </summary>
    private void performNetworkscan(string pMulticastAddress)
    {
        PacketDotNet.Packet packet = ping.buildPacket(liveDevice, pMulticastAddress);
        if (packet != null)
        {
            liveDevice.SendPacket(packet);
        }
        else
        {
            mainForm.lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Error! The network adapter cannot be used!";
        }
    }
    
    /// <summary>
    /// Receives ICMPv6 echo replies and evaluates the IPv6 and MAC address.
    /// If a device is not present in the list, this is recorded.
    /// </summary>
    private void device_OnPacketArrival(object sender, PacketCapture e)
    {
        var rawPacket = e.GetPacket();
        var packet = PacketDotNet.Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
        EthernetPacket ethernetPacket = packet.Extract<PacketDotNet.EthernetPacket>();
        IPv6Packet ipv6Packet = packet.Extract<PacketDotNet.IPv6Packet>();
        IcmpV6Packet icmpv6Packet = packet.Extract<PacketDotNet.IcmpV6Packet>();

        if (ethernetPacket != null && ipv6Packet != null && icmpv6Packet != null)
        {
            if (icmpv6Packet.Type == IcmpV6Type.EchoReply)
            {
                bool matchFound = false;
                foreach (Host host in hostList)
                {
                    if (host.ipAddress.Equals(ipv6Packet.SourceAddress))
                    {
                        mainForm.lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Already identified host detected!";
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    Host tempHost = new Host();
                    tempHost.physicalAddress = ethernetPacket.SourceHardwareAddress;
                    tempHost.ipAddress = ipv6Packet.SourceAddress;
                    hostList.Add(tempHost);
                    mainForm.lblInfo.Text = DateTime.Now.ToLongTimeString() + ": New host discovered!";
                }
            }
        }
    }

    /// <summary>
    /// Stops the active scan
    /// </summary>
    public void stopScan()
    {
        scanStatus = false;
        liveDevice.StopCapture();
    }

    /// <summary>
    /// Returns the list with the scanned network participants
    /// </summary>
    /// <returns></returns>
    public List<Host> retrievHosts()
    {
        return hostList;
    }    
}
