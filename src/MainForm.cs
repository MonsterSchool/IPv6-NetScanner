using SharpPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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


        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            extractResources();

            // Lists
            deviceList = CaptureDeviceList.Instance;
            localIPv6AddrCollection = getLocalIPs();
        }

        private void btnScanNet_Click(object sender, EventArgs e)
        {
            if (liveDevice != null & localIPv6 != null)
            {
                if (picLoading.Image == null & scan.scanStatus == false)
                {
                    btnScanNet_Start();
                }
                else
                {
                    btnScanNet_Stop();
                }
            }
        }

        private void btnScanNet_Start()
        {
            btnScanNet.Text = "Stop Scan";
            lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan started...";
            picLoading.Image = IPv6_NetScanner.Properties.Resources.load;

            Debug.WriteLine("Local IPv6: " + localIPv6.ToString());
            scan.scanNetwork(this, liveDevice, localIPv6);
        }

        public void btnScanNet_Stop()
        {
            btnScanNet.Text = "Start Scan";
            lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan stopped...";
            picLoading.Image = null;
            // scan.stopScan();
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

        public void btnShowHosts_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = "Total amount of hosts found: " + scan.retrievHosts().Count;

                while (dataGV.Rows.Count > 0)
                {
                    dataGV.Rows.Remove(dataGV.Rows[0]);
                }

                foreach (Host host in scan.retrievHosts())
                {
                    string[] row = new string[] { host.ipAddress.ToString(), string.Join("-", host.physicalAddress.GetAddressBytes().Select(b => b.ToString("X2"))), host.info, getManufacturer(host.physicalAddress.ToString()) };
                    dataGV.Rows.Add(row);
                    Thread.Sleep(50);
                }
            }
            catch (Exception) { }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }


        private void extractResources()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\mac-vendor.txt";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, IPv6_NetScanner.Properties.Resources.mac_vendor);
            }
        }


        private string getManufacturer(string pPhysicalAddress)
        {
            string manufacturer = "Not Found";

            try
            {
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\mac-vendor.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Contains(pPhysicalAddress.Substring(0, 6)))
                        {
                            manufacturer = line.Remove(0, 6);
                            break;
                        }
                    }
                }
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

        private void dataGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGV.Rows[e.RowIndex].Cells[1].Value.ToString());
            lblInfo.Text = "Cell content copied to clipboard!";
        }
    }
}
