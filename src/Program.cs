using SharpPcap;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace IPv6_NetScanner
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Check for Npcap
            checkNpcap();

            // Check Version
            // checkVersion();

            // Start Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void checkNpcap()
        {
            try
            {
                Debug.WriteLine("SharpPcap {0}", Pcap.SharpPcapVersion);
            }
            catch (Exception)
            {
                MessageBox.Show("Please install Npcap to proceed!", "Error Npcap not found!");
                Environment.Exit(0);
            }
        }

        private static void checkVersion()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    int webVersion = Convert.ToInt32(webClient.DownloadString("http://software.calysoft.net/software/netscan/version"));

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    int fileVersion = Convert.ToInt32(fvi.FileVersion.Replace(".", ""));

                    Debug.WriteLine("IPv6-NetScanner Web-version: " + webVersion);
                    Debug.WriteLine("IPv6-NetScanner File-version: " + fileVersion);


                    if (webVersion > fileVersion)
                    {
                        // Update available
                        DialogResult messageBox = MessageBox.Show("A new version of the IPv6-NetScanner is available. Do you want to download the new version?", "A new version is available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (messageBox == DialogResult.Yes)
                            Process.Start("https://calysoft.net");
                    }
                }
            }
            catch (Exception)
            {
                DialogResult messageBox = MessageBox.Show("The latest IPv6 NetScanner version could not be retrieved from our server. Do you still want to continue with this version?", "An unexpected error occurred!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (messageBox == DialogResult.No)
                    Environment.Exit(0);
            }
        }
    }
}
