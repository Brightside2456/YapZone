using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Drawing;


namespace YapZone_Client
{
    public partial class ClientForm : Form
    {

        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread receiveThread;
        private bool isConnected = false;
        private string currentUsername;



        public ClientForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isConnected)
            {
                btnDisconnect_Click(null, null);
            }
            base.OnFormClosing(e);
        }

        private bool IsValidConnection()
        {
            try
            {
                return tcpClient != null && tcpClient.Connected && networkStream != null;
            }
            catch
            {
                return false;
            }
        }

        private void AddChatMessage(string message)
        {
            // Check if we need to invoke (thread-safe UI update)
            if (rtbChatDisplay.InvokeRequired)
            {
                rtbChatDisplay.Invoke(new Action(() => AddChatMessage(message)));
                return;
            }

            // Add timestamp and message
            string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            rtbChatDisplay.AppendText(timestampedMessage + Environment.NewLine);

            // Auto-scroll to bottom
            rtbChatDisplay.ScrollToCaret();
        }

        private void SendCurrentMessage()
        {
            // Get message text and validate
            string messageText = txtMessageInput.Text.Trim();

            if (string.IsNullOrEmpty(messageText))
            {
                // Don't send empty messages
                txtMessageInput.Focus();
                return;
            }

            if (!IsValidConnection())
            {
                AddChatMessage("Cannot send message - not connected to server");
                return;
            }

            try
            {
                // Format message with username
                string formattedMessage = $"{currentUsername}: {messageText}";

                // Convert to bytes and send
                byte[] messageData = Encoding.UTF8.GetBytes(formattedMessage);
                networkStream.Write(messageData, 0, messageData.Length);

                // Clear input and refocus for next message
                txtMessageInput.Clear();
                txtMessageInput.Focus();

                // Optional: Show message being sent in chat (commented out to avoid duplicates)
                // AddChatMessage($"You: {messageText}");

            }
            catch (Exception ex)
            {
                AddChatMessage($"Error sending message: {ex.Message}");

                // Connection might be broken, trigger disconnect
                if (!IsValidConnection())
                {
                    btnDisconnect_Click(null, null);
                }
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (isConnected && tcpClient.Connected)
                {
                    // Read incoming data
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // Server closed connection
                        AddChatMessage("Server closed the connection");
                        break;
                    }

                    // Convert bytes to string
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Display the message
                    AddChatMessage(receivedMessage);
                }
            }
            catch (Exception ex)
            {
                // Only show error if we're supposed to be connected
                if (isConnected)
                {
                    AddChatMessage($"Connection lost: {ex.Message}");

                    // Trigger disconnect on UI thread
                    if (lblConnectionStatus.InvokeRequired)
                    {
                        lblConnectionStatus.Invoke(new Action(() => btnDisconnect_Click(null, null)));
                    }
                    else
                    {
                        btnDisconnect_Click(null, null);
                    }
                }
            }
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Validate username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username before connecting.",
                               "Username Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // Validate server IP
            if (string.IsNullOrWhiteSpace(txtServerIP.Text))
            {
                MessageBox.Show("Please enter a server IP address.",
                               "Server IP Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServerIP.Focus();
                return;
            }

            try
            {
                // Store username
                currentUsername = txtUsername.Text.Trim();

                // Get connection details
                string serverIP = txtServerIP.Text.Trim();
                int port = int.Parse(txtPortClient.Text);

                // Create and connect TCP client
                tcpClient = new TcpClient();
                tcpClient.Connect(serverIP, port);
                networkStream = tcpClient.GetStream();

                // Update connection state
                isConnected = true;

                // Update UI elements
                lblConnectionStatus.Text = $"Status: Connected as {currentUsername}";
                lblConnectionStatus.ForeColor = Color.Green;

                // Update button states
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                // Update input field states
                txtUsername.Enabled = false;
                txtServerIP.Enabled = false;
                txtPortClient.Enabled = false;
                txtMessageInput.Enabled = true;
                btnSend.Enabled = true;

                // Focus message input
                txtMessageInput.Focus();

                // Add connection message to chat
                AddChatMessage("Connected to server successfully!");
                AddChatMessage($"Welcome {currentUsername}! You can now start chatting.");

                // Start background thread to receive messages
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                // Send join notification to server (optional)
                SendJoinNotification();

            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid port number (e.g., 8888)",
                               "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Cannot connect to server:\n{ex.Message}\n\nMake sure the server is running and accessible.",
                               "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}",
                               "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SendJoinNotification()
        {
            try
            {
                string joinMessage = $"{currentUsername} joined the chat";
                byte[] data = Encoding.UTF8.GetBytes(joinMessage);
                networkStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                AddChatMessage($"Error sending join notification: {ex.Message}");
            }
        }

        private void SendLeaveNotification()
        {
            try
            {
                string leaveMessage = $"{currentUsername} left the chat";
                byte[] data = Encoding.UTF8.GetBytes(leaveMessage);
                networkStream.Write(data, 0, data.Length);

                // Give server time to receive and broadcast the message
                Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                // Ignore errors when leaving - connection might already be broken
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Send leave notification before disconnecting
                if (isConnected)
                {
                    SendLeaveNotification();
                }

                // Update connection state
                isConnected = false;

                // Close network resources
                networkStream?.Close();
                tcpClient?.Close();

                // Wait for receive thread to finish (with timeout)
                if (receiveThread != null && receiveThread.IsAlive)
                {
                    if (!receiveThread.Join(2000)) // Wait max 2 seconds
                    {
                        receiveThread.Abort(); // Force stop if needed
                    }
                }

                // Update UI elements
                lblConnectionStatus.Text = "Status: Disconnected";
                lblConnectionStatus.ForeColor = Color.Red;

                // Update button states
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;

                // Update input field states
                txtUsername.Enabled = true;
                txtServerIP.Enabled = true;
                txtPortClient.Enabled = true;
                txtMessageInput.Enabled = false;
                btnSend.Enabled = false;

                // Clear message input
                txtMessageInput.Clear();

                // Focus username for next connection
                txtUsername.Focus();

                // Add disconnection message to chat
                AddChatMessage("Disconnected from server");

            }
            catch (Exception ex)
            {
                AddChatMessage($"Error during disconnect: {ex.Message}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendCurrentMessage();
        }

        private void txtMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {

                //Prevent the Ding Sound 
                e.SuppressKeyPress = true;


                SendCurrentMessage();
            }


        }
        private void txtMessageInput_TextChanged(object sender, EventArgs e)
        {
            // Enable/disable send button based on whether there's text
            btnSend.Enabled = isConnected && !string.IsNullOrWhiteSpace(txtMessageInput.Text);
        }
    }
}
