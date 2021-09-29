using SharpPcap;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IPv6_Scanner
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
