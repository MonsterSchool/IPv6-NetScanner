using SharpPcap;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Following: Form-Methods
        /// </summary>
        private MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            extractResources();

            // Lists
            deviceList = CaptureDeviceList.Instance;
            localIPv6AddrCollection = getLocalIPs();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnScanNet_Click(object sender, EventArgs e)
        {
            if (liveDevice != null & localIPv6 != null)
            {
                if (scan.scanStatus == false & picLoading.Image == null)
                {
                    scanNet_Start();
                }
                else
                {
                    scanNet_Stop();
                }
            }
        }

        private void btnShowHosts_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = "Total amount of hosts found: " + scan.retrievHosts().Count;

            }
            catch (Exception) { }
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

        private void dataGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGV.Rows[e.RowIndex].Cells[1].Value.ToString());
            lblInfo.Text = "Cell content copied to clipboard!";
        }

        /// <summary>
        /// Following: MISC-Methods
        /// </summary>
        private void scanNet_Start()
        {
            btnScanNet.Text = "Stop Scan";
            lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan started...";
            picLoading.Image = Properties.Resources.load;
            scan.scanNetwork(this, liveDevice, localIPv6);
        }

        private void scanNet_Stop()
        {
            btnScanNet.Text = "Start Scan";
            lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan stopped...";
            picLoading.Image = null;
        }   

        private void extractResources()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IPv6-NetScanner\vendor-macs.xml";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, Properties.Resources.vendorMacs);
            }
        }

        private string getManufacturer(string pPhysicalAddress)
        {
            string manufacturer = "Not Found";

            try
            {
                
            }
            catch (Exception) { }   

            return manufacturer;
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
