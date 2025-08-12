namespace YapZone_Client
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            grpConnection = new GroupBox();
            lblConnectionStatus = new Label();
            btnDisconnect = new Button();
            btnConnect = new Button();
            txtPortClient = new TextBox();
            lblPort = new Label();
            txtServerIP = new TextBox();
            txtUsername = new TextBox();
            lblServerIP = new Label();
            lblUsername = new Label();
            lblChatHistory = new Label();
            rtbChatDisplay = new RichTextBox();
            lblMesageInput = new Label();
            txtMessageInput = new TextBox();
            btnSend = new Button();
            grpConnection.SuspendLayout();
            SuspendLayout();
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(lblConnectionStatus);
            grpConnection.Controls.Add(btnDisconnect);
            grpConnection.Controls.Add(btnConnect);
            grpConnection.Controls.Add(txtPortClient);
            grpConnection.Controls.Add(lblPort);
            grpConnection.Controls.Add(txtServerIP);
            grpConnection.Controls.Add(txtUsername);
            grpConnection.Controls.Add(lblServerIP);
            grpConnection.Controls.Add(lblUsername);
            grpConnection.Location = new Point(17, 15);
            grpConnection.Name = "grpConnection";
            grpConnection.Size = new Size(630, 96);
            grpConnection.TabIndex = 0;
            grpConnection.TabStop = false;
            grpConnection.Text = "Connection Settings";
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.ForeColor = Color.Red;
            lblConnectionStatus.Location = new Point(231, 58);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(155, 20);
            lblConnectionStatus.TabIndex = 1;
            lblConnectionStatus.Text = "Status: Disconnected";
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.OrangeRed;
            btnDisconnect.Enabled = false;
            btnDisconnect.ForeColor = Color.White;
            btnDisconnect.Location = new Point(112, 53);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(100, 30);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.DodgerBlue;
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(6, 53);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(100, 30);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtPortClient
            // 
            txtPortClient.Location = new Point(419, 20);
            txtPortClient.Name = "txtPortClient";
            txtPortClient.Size = new Size(60, 27);
            txtPortClient.TabIndex = 2;
            txtPortClient.Text = "8888";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(378, 22);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(47, 20);
            lblPort.TabIndex = 1;
            lblPort.Text = "Port: ";
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(271, 20);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(100, 27);
            txtServerIP.TabIndex = 2;
            txtServerIP.Text = "127.0.0.1";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(88, 20);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(120, 27);
            txtUsername.TabIndex = 2;
            txtUsername.Text = "User2134";
            // 
            // lblServerIP
            // 
            lblServerIP.AutoSize = true;
            lblServerIP.Location = new Point(214, 23);
            lblServerIP.Name = "lblServerIP";
            lblServerIP.Size = new Size(63, 20);
            lblServerIP.TabIndex = 1;
            lblServerIP.Text = "Server: ";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(6, 23);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(88, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username: ";
            lblUsername.Click += lblUsername_Click;
            // 
            // lblChatHistory
            // 
            lblChatHistory.AutoSize = true;
            lblChatHistory.Location = new Point(15, 110);
            lblChatHistory.Name = "lblChatHistory";
            lblChatHistory.Size = new Size(121, 20);
            lblChatHistory.TabIndex = 1;
            lblChatHistory.Text = "Chat Messages: ";
            // 
            // rtbChatDisplay
            // 
            rtbChatDisplay.BackColor = Color.White;
            rtbChatDisplay.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbChatDisplay.Location = new Point(15, 135);
            rtbChatDisplay.Name = "rtbChatDisplay";
            rtbChatDisplay.ReadOnly = true;
            rtbChatDisplay.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbChatDisplay.Size = new Size(500, 300);
            rtbChatDisplay.TabIndex = 2;
            rtbChatDisplay.Text = "";
            // 
            // lblMesageInput
            // 
            lblMesageInput.AutoSize = true;
            lblMesageInput.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMesageInput.Location = new Point(15, 450);
            lblMesageInput.Name = "lblMesageInput";
            lblMesageInput.Size = new Size(138, 20);
            lblMesageInput.TabIndex = 3;
            lblMesageInput.Text = "Type your message:";
            // 
            // txtMessageInput
            // 
            txtMessageInput.Enabled = false;
            txtMessageInput.Location = new Point(15, 475);
            txtMessageInput.Name = "txtMessageInput";
            txtMessageInput.Size = new Size(460, 27);
            txtMessageInput.TabIndex = 4;
            txtMessageInput.KeyDown += txtMessageInput_KeyDown;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.MediumSeaGreen;
            btnSend.Enabled = false;
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(481, 470);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 36);
            btnSend.TabIndex = 5;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(655, 523);
            Controls.Add(btnSend);
            Controls.Add(txtMessageInput);
            Controls.Add(lblMesageInput);
            Controls.Add(rtbChatDisplay);
            Controls.Add(lblChatHistory);
            Controls.Add(grpConnection);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ClientForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YapZone-Client";
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpConnection;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblServerIP;
        private TextBox txtServerIP;
        private TextBox txtPortClient;
        private Label lblPort;
        private Button btnConnect;
        private Button btnDisconnect;
        private Label lblConnectionStatus;
        private Label lblChatHistory;
        private RichTextBox rtbChatDisplay;
        private Label lblMesageInput;
        private TextBox txtMessageInput;
        private Button btnSend;
    }
}
