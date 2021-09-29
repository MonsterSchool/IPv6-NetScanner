
namespace IPv6_Scanner
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panTop = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnShowHosts = new System.Windows.Forms.Button();
            this.dUpDoDevice = new System.Windows.Forms.DomainUpDown();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.btnScanNet = new System.Windows.Forms.Button();
            this.panFill = new System.Windows.Forms.Panel();
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.Hostname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPv6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EthernetAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.panFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.Transparent;
            this.panTop.Controls.Add(this.lblInfo);
            this.panTop.Controls.Add(this.btnShowHosts);
            this.panTop.Controls.Add(this.dUpDoDevice);
            this.panTop.Controls.Add(this.picLoading);
            this.panTop.Controls.Add(this.btnScanNet);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(800, 30);
            this.panTop.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInfo.Location = new System.Drawing.Point(545, 5);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(225, 20);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnShowHosts
            // 
            this.btnShowHosts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnShowHosts.FlatAppearance.BorderSize = 0;
            this.btnShowHosts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHosts.Location = new System.Drawing.Point(140, 5);
            this.btnShowHosts.Name = "btnShowHosts";
            this.btnShowHosts.Size = new System.Drawing.Size(130, 20);
            this.btnShowHosts.TabIndex = 3;
            this.btnShowHosts.Text = "Show Result";
            this.btnShowHosts.UseVisualStyleBackColor = false;
            this.btnShowHosts.Click += new System.EventHandler(this.btnShowHosts_Click);
            // 
            // dUpDoDevice
            // 
            this.dUpDoDevice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dUpDoDevice.Location = new System.Drawing.Point(275, 5);
            this.dUpDoDevice.Name = "dUpDoDevice";
            this.dUpDoDevice.Size = new System.Drawing.Size(260, 20);
            this.dUpDoDevice.TabIndex = 2;
            this.dUpDoDevice.Text = "Select Networkadapter";
            this.dUpDoDevice.SelectedItemChanged += new System.EventHandler(this.dUpDoDevice_SelectedItemChanged);
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = global::IPv6_Scanner.Properties.Resources.load;
            this.picLoading.Location = new System.Drawing.Point(775, 5);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(20, 20);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 1;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // btnScanNet
            // 
            this.btnScanNet.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnScanNet.FlatAppearance.BorderSize = 0;
            this.btnScanNet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanNet.Location = new System.Drawing.Point(5, 5);
            this.btnScanNet.Name = "btnScanNet";
            this.btnScanNet.Size = new System.Drawing.Size(130, 20);
            this.btnScanNet.TabIndex = 0;
            this.btnScanNet.Text = "Start Networkscan";
            this.btnScanNet.UseVisualStyleBackColor = false;
            this.btnScanNet.Click += new System.EventHandler(this.btnScanNet_Click);
            // 
            // panFill
            // 
            this.panFill.Controls.Add(this.dataGV);
            this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFill.Location = new System.Drawing.Point(0, 30);
            this.panFill.Name = "panFill";
            this.panFill.Size = new System.Drawing.Size(800, 420);
            this.panFill.TabIndex = 1;
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGV.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hostname,
            this.IPv6,
            this.EthernetAddress});
            this.dataGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGV.Location = new System.Drawing.Point(0, 0);
            this.dataGV.Name = "dataGV";
            this.dataGV.Size = new System.Drawing.Size(800, 420);
            this.dataGV.TabIndex = 0;
            // 
            // Hostname
            // 
            this.Hostname.Frozen = true;
            this.Hostname.HeaderText = "Hostname";
            this.Hostname.Name = "Hostname";
            this.Hostname.Width = 80;
            // 
            // IPv6
            // 
            this.IPv6.Frozen = true;
            this.IPv6.HeaderText = "IPv6";
            this.IPv6.Name = "IPv6";
            this.IPv6.Width = 54;
            // 
            // EthernetAddress
            // 
            this.EthernetAddress.Frozen = true;
            this.EthernetAddress.HeaderText = "Ethernet-Address";
            this.EthernetAddress.Name = "EthernetAddress";
            this.EthernetAddress.Width = 113;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panFill);
            this.Controls.Add(this.panTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPv6-Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.panFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Panel panFill;
        private System.Windows.Forms.Button btnScanNet;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.DomainUpDown dUpDoDevice;
        private System.Windows.Forms.Button btnShowHosts;
        public System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hostname;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPv6;
        private System.Windows.Forms.DataGridViewTextBoxColumn EthernetAddress;
        public System.Windows.Forms.Label lblInfo;
    }
}

