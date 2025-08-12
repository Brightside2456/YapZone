

namespace YapZone
{
    partial class ServerForm
    {
        
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"> true if managed resources should be disposed; otherwise, false.</param>
        /// 
        

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }






        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblServerStatus = new Label();
            lblPort = new Label();
            txtPort = new TextBox();
            btnStartServer = new Button();
            btnStopServer = new Button();
            lblActivity = new Label();
            rtbServerLog = new RichTextBox();
            lblClients = new Label();
            lstClients = new ListBox();
            SuspendLayout();
            // 
            // lblServerStatus
            // 
            lblServerStatus.AutoSize = true;
            lblServerStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblServerStatus.ForeColor = Color.Red;
            lblServerStatus.Location = new Point(23, 27);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(196, 23);
            lblServerStatus.TabIndex = 0;
            lblServerStatus.Text = "Server Status: Stopped";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(320, 27);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(42, 20);
            lblPort.TabIndex = 1;
            lblPort.Text = "Port: ";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(366, 24);
            txtPort.Margin = new Padding(3, 4, 3, 4);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(91, 27);
            txtPort.TabIndex = 2;
            txtPort.Text = "8888";
            // 
            // btnStartServer
            // 
            btnStartServer.BackColor = Color.LimeGreen;
            btnStartServer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartServer.ForeColor = Color.White;
            btnStartServer.Location = new Point(23, 73);
            btnStartServer.Margin = new Padding(3, 4, 3, 4);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(137, 47);
            btnStartServer.TabIndex = 3;
            btnStartServer.Text = "Start Server";
            btnStartServer.UseVisualStyleBackColor = false;
            btnStartServer.Click += btnStartServer_Click;
            // 
            // btnStopServer
            // 
            btnStopServer.BackColor = Color.Red;
            btnStopServer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStopServer.ForeColor = Color.White;
            btnStopServer.Location = new Point(171, 73);
            btnStopServer.Margin = new Padding(3, 4, 3, 4);
            btnStopServer.Name = "btnStopServer";
            btnStopServer.Size = new Size(137, 47);
            btnStopServer.TabIndex = 4;
            btnStopServer.Text = "Stop Server";
            btnStopServer.UseVisualStyleBackColor = false;
            btnStopServer.Click += btnStopServer_Click;
            // 
            // lblActivity
            // 
            lblActivity.AutoSize = true;
            lblActivity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActivity.Location = new Point(23, 140);
            lblActivity.Name = "lblActivity";
            lblActivity.Size = new Size(120, 20);
            lblActivity.TabIndex = 5;
            lblActivity.Text = "Server Activity: ";
            // 
            // rtbServerLog
            // 
            rtbServerLog.BackColor = Color.WhiteSmoke;
            rtbServerLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbServerLog.Location = new Point(23, 173);
            rtbServerLog.Margin = new Padding(3, 4, 3, 4);
            rtbServerLog.Name = "rtbServerLog";
            rtbServerLog.ReadOnly = true;
            rtbServerLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbServerLog.Size = new Size(571, 265);
            rtbServerLog.TabIndex = 6;
            rtbServerLog.Text = "";
            // 
            // lblClients
            // 
            lblClients.AutoSize = true;
            lblClients.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClients.Location = new Point(23, 460);
            lblClients.Name = "lblClients";
            lblClients.Size = new Size(151, 20);
            lblClients.TabIndex = 7;
            lblClients.Text = "Connected Clients: 0";
            // 
            // lstClients
            // 
            lstClients.BackColor = Color.Azure;
            lstClients.FormattingEnabled = true;
            lstClients.Location = new Point(23, 493);
            lstClients.Margin = new Padding(3, 4, 3, 4);
            lstClients.Name = "lstClients";
            lstClients.Size = new Size(571, 104);
            lstClients.TabIndex = 8;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 615);
            Controls.Add(lstClients);
            Controls.Add(lblClients);
            Controls.Add(rtbServerLog);
            Controls.Add(lblActivity);
            Controls.Add(btnStopServer);
            Controls.Add(btnStartServer);
            Controls.Add(txtPort);
            Controls.Add(lblPort);
            Controls.Add(lblServerStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "ServerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YapZone";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblServerStatus;
        private Label lblPort;
        private TextBox txtPort;
        private Button btnStartServer;
        private Button btnStopServer;
        private Label lblActivity;
        private RichTextBox rtbServerLog;
        private Label lblClients;
        private ListBox lstClients;
    }
}
