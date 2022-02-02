using IPv6_NetScanner;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

class Scan
{
    private MainForm mainForm;
    private PacketBuilder networkPacket = new PacketBuilder();
    private ILiveDevice liveDevice;
    private List<Host> hostList = new List<Host>();
    private int multiAddrIndex = 0;

    /// <summary>
    /// Indicates whether a scan is active
    /// </summary>
    public bool scanStatus = false;

    /// <summary>
    /// Starts an endless network scan. This can be stopped via the stopScan method.
    /// </summary>
    /// <param name="pLiveDevice">The network device to be used for the scan.</param>
    public void scanNetwork(MainForm pMainForm, ILiveDevice pLiveDevice, IPAddress pLocalIPv6)
    {
        mainForm = pMainForm;
        liveDevice = pLiveDevice;

        liveDevice.OnPacketArrival +=
            new PacketArrivalEventHandler(device_OnPacketArrival);

        scanStatus = true;
        liveDevice.StartCapture();

        Thread scanThread = new Thread(() => threadMethod(pLocalIPv6));
        scanThread.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    private void threadMethod(IPAddress pLocalIPv6)
    {
        while (scanStatus)
        {
            if (multiAddrIndex == 4)
            {
                multiAddrIndex = 0;
                stopScan();
                break;
            }


            performNetworkscan(pLocalIPv6, multiAddrIndex);
            Thread.Sleep(2000);
            multiAddrIndex++;

            mainForm.Text = "IPv6-NetScanner " + multiAddrIndex * 25 + "%";
        }
    }

    /// <summary>
    /// Builds an ICMPv6 ping request packet and sends it over the specified network gateway.
    /// </summary>
    private void performNetworkscan(IPAddress pLocalIPv6, int pMultiAddrIndex)
    {
        PacketDotNet.Packet packet = networkPacket.buildPacket(liveDevice, pLocalIPv6, pMultiAddrIndex);
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
                checkHost(ethernetPacket, ipv6Packet);
            }
        }
    }

    private void checkHost(EthernetPacket pEthernetPacket, IPv6Packet pIPv6Packet)
    {
        bool _matchFound = false;
        foreach (Host host in hostList)
        {
            if (host.ipAddress.Equals(pIPv6Packet.SourceAddress))
            {
                switch (multiAddrIndex)
                {
                    case 1:
                        host.info = "Interface-local Router";
                        break;
                    case 3:
                        host.info = "Link-local Router";
                        break;
                }
                mainForm.lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Already identified host detected!";
                _matchFound = true;
                break;
            }
        }

        if (!_matchFound)
        {
            Host tempHost = new Host();
            tempHost.physicalAddress = pEthernetPacket.SourceHardwareAddress;
            tempHost.ipAddress = pIPv6Packet.SourceAddress;

            switch (multiAddrIndex)
            {
                case 0:
                    tempHost.info = "Interface-local node";
                    break;
                case 1:
                    tempHost.info = "Interface-local Router";
                    break;
                case 2:
                    tempHost.info = "Link-local node";
                    break;
                case 3:
                    tempHost.info = "Link-local Router";
                    break;
            }

            hostList.Add(tempHost);
            mainForm.lblInfo.Text = DateTime.Now.ToLongTimeString() + ": New host discovered!";
        }
    }

    /// <summary>
    /// Stops the active scan
    /// </summary>
    public void stopScan()
    {
        scanStatus = false;
        liveDevice.StopCapture();

        mainForm.btnScanNet_Stop();
        mainForm.refreshHostList();
        mainForm.Text = "IPv6-NetScanner";
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
