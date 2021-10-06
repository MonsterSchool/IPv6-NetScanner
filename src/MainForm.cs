using SharpPcap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace IPv6_NetScanner
{
    public partial class MainForm : Form
    {
        // Networkdevice
        private static ILiveDevice liveDevice;

        // Classes
        private static CaptureDeviceList deviceList;        
        private static Scan scan = new Scan();

        // Variables
        private IPAddress localIPv6;
        private static List<IPAddress> localIPv6AddrCollection;
        private string multicastAddress = "FF02::1";

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            deviceList = CaptureDeviceList.Instance;
            localIPv6AddrCollection = getLocalIPs();

            picLoading.Image = null;
        }

        private void btnScanNet_Click(object sender, EventArgs e)
        {
            if (liveDevice != null & localIPv6 != null)
            {
                if (picLoading.Image == null)
                {
                    btnScanNet.Text = "Stop Networkscan";
                    lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan started...";
                    picLoading.Image = IPv6_NetScanner.Properties.Resources.load;
                    scan.scanNetwork(this, liveDevice, localIPv6, multicastAddress);

                }
                else
                {
                    btnScanNet.Text = "Start Networkscan";
                    lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan stopped...";
                    picLoading.Image = null;
                    scan.stopScan();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (deviceList.Count < 1)
            {
                MessageBox.Show("No devices were found on this machine", "Error");
                return;
            }
            else
            {
                foreach (ILiveDevice dev in deviceList)
                {
                    dUpDoDevice.Items.Add(dev.Description);
                }
            }
            
            foreach (IPAddress addr in localIPv6AddrCollection)
            {
                if (addr.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    dUpDoIP.Items.Add(addr.ToString());
                }
            }
        }

        private void dUpDoDevice_SelectedItemChanged(object sender, EventArgs e)
        {
            if (liveDevice != null)
            {
                liveDevice.Close();
            }

            foreach (ILiveDevice dev in deviceList)
            {
                if (dev.Description == dUpDoDevice.Text)
                {
                    liveDevice = dev;

                    if (liveDevice != null)
                    {
                        liveDevice.Open();
                    }
                }
            }
        }


        private void dUpDoIP_SelectedItemChanged(object sender, EventArgs e)
        {
            foreach (IPAddress addr in localIPv6AddrCollection)
            {
                if (addr.ToString() == dUpDoIP.Text)
                {
                    localIPv6 = addr;
                }
            }            
        }

        private void btnShowHosts_Click(object sender, EventArgs e)
        {
            while (dataGV.Rows.Count > 0)
            {
                dataGV.Rows.Remove(dataGV.Rows[0]);
            }

            foreach (Host host in scan.retrievHosts())
            {
                string[] row = new string[] { "-", host.ipAddress.ToString(), host.physicalAddress.ToString() };
                dataGV.Rows.Add(row);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }


        private List<IPAddress> getLocalIPs()
        {
            List<IPAddress> returnList = new List<IPAddress>();

            string strHostName = Dns.GetHostName(); ;
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            foreach (IPAddress addr in ipEntry.AddressList)
            {
                returnList.Add(IPAddress.Parse(addr.ToString().Split('%')[0]));
            }

            return returnList;
        }
    }
}
