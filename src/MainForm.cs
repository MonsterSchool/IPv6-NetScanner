using SharpPcap;
using System;
using System.Windows.Forms;

namespace IPv6_Scanner
{
    public partial class MainForm : Form
    {
        // Networkdevice
        private static ILiveDevice liveDevice;

        // Classes
        private static CaptureDeviceList deviceList;
        private static Scan scan = new Scan();

        // Variables
        private string multicastAddress = "FF02::1";

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            deviceList = CaptureDeviceList.Instance;
        }

        private void btnScanNet_Click(object sender, EventArgs e)
        {
            if (liveDevice != null)
            {
                if (!picLoading.Visible)
                {
                    btnScanNet.Text = "Stop Networkscan";
                    lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan started...";
                    picLoading.Visible = true;
                    scan.scanNetwork(this, liveDevice, multicastAddress);

                }
                else
                {
                    btnScanNet.Text = "Start Networkscan";
                    lblInfo.Text = DateTime.Now.ToLongTimeString() + ": Scan stopped...";
                    picLoading.Visible = false;
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

            foreach (ILiveDevice dev in deviceList)
            {
                dUpDoDevice.Items.Add(dev.Description);
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
    }
}
