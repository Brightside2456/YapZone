using System.Collections.Generic;  // This is a list for storing Clients
using System.Net;  //This is used for the Socket Programing (IP Address and Networking)
using System.Net.Sockets;   // For TCP server Functionality
using System.Text;
using System.Threading; // Background threads for the server
//using System.Windows.Forms;

namespace YapZone
{
    public partial class ServerForm : Form
    {
        // Server networking variables
        private TcpListener tcpListener;
        List<TcpClient> connectedClients;
        private Thread serverThread;
        private bool isServerRunning = false;

        public ServerForm()
        {
            InitializeComponent();
            connectedClients = new List<TcpClient>();

        }

        private void AddServerLog(string message)
        {
            // Check if we need to invoke (thread-safe UI update)
            if (rtbServerLog.InvokeRequired)
            {
                rtbServerLog.Invoke(new Action(() => AddServerLog(message)));
                return;
            }

            // Add timestamp and message
            string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            rtbServerLog.AppendText(timestampedMessage + Environment.NewLine);

            // Auto-scroll to bottom
            rtbServerLog.ScrollToCaret();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isServerRunning)
            {
                btnStopServer_Click(null, null);
            }
            base.OnFormClosing(e);
        }

        private void BroadcastMessage(string message, TcpClient senderClient = null)
        {
            byte[] messageData = Encoding.UTF8.GetBytes(message);

            // Create a copy of the clients list to avoid modification during iteration
            TcpClient[] clientsCopy = connectedClients.ToArray();

            foreach (TcpClient client in clientsCopy)
            {
                // Skip the sender (don't echo message back to sender)
                if (client == senderClient)
                    continue;

                try
                {
                    if (client.Connected)
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(messageData, 0, messageData.Length);
                    }
                }
                catch (Exception ex)
                {
                    // Client connection broken, remove from list
                    AddServerLog($"Failed to send to client: {ex.Message}");

                    try
                    {
                        client.Close();
                    }
                    catch { }

                    connectedClients.Remove(client);
                }
            }

            // Update clients list display after any removals
            UpdateClientsList();

            // Log broadcast completion
            int recipientCount = (senderClient != null) ? clientsCopy.Length - 1 : clientsCopy.Length;
            AddServerLog($"Broadcasted message to {recipientCount} clients");
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream clientStream = null;
            string clientEndpoint = "Unknown";

            try
            {
                clientStream = client.GetStream();
                clientEndpoint = client.Client.RemoteEndPoint.ToString();

                byte[] buffer = new byte[1024];

                while (client.Connected && isServerRunning)
                {
                    // Read incoming message
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // Client disconnected gracefully
                        break;
                    }

                    // Convert bytes to string
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Log the received message
                    AddServerLog($"Received from {clientEndpoint}: {receivedMessage}");

                    // Broadcast message to all other clients
                    BroadcastMessage(receivedMessage, client);
                }
            }
            catch (Exception ex)
            {
                // Log client error (only if server is running)
                if (isServerRunning)
                {
                    AddServerLog($"Client {clientEndpoint} error: {ex.Message}");
                }
            }
            finally
            {
                // Clean up when client disconnects
                try
                {
                    clientStream?.Close();
                    client?.Close();
                }
                catch { }

                // Remove from clients list and update UI
                connectedClients.Remove(client);
                UpdateClientsList();
                AddServerLog($"Client {clientEndpoint} disconnected");
            }
        }


        private void UpdateClientsList()
        {
            // Ensure thread-safe UI update
            if (lblClients.InvokeRequired)
            {
                lblClients.Invoke(new Action(UpdateClientsList));
                return;
            }

            // Update client count
            lblClients.Text = $"Connected Clients: {connectedClients.Count}";

            // Update clients listbox
            lstClients.Items.Clear();
            foreach (TcpClient client in connectedClients)
            {
                try
                {
                    if (client.Connected)
                    {
                        string clientInfo = client.Client.RemoteEndPoint.ToString();
                        lstClients.Items.Add($"• {clientInfo}");
                    }
                }
                catch
                {
                    // Skip clients that are disconnecting
                }
            }
        }

        private void AcceptClients()
        {
            try
            {
                while (isServerRunning)
                {
                    // Accept incoming client connection
                    TcpClient newClient = tcpListener.AcceptTcpClient();

                    // Add to clients list
                    connectedClients.Add(newClient);

                    // Get client info
                    string clientEndpoint = newClient.Client.RemoteEndPoint.ToString();

                    // Update UI (thread-safe)
                    UpdateClientsList();

                    // Log the new connection
                    AddServerLog($"Client connected: {clientEndpoint}");

                    // Start thread to handle this client's messages
                    Thread clientThread = new Thread(() => HandleClient(newClient));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                // Only log errors if server is supposed to be running
                if (isServerRunning)
                {
                    AddServerLog($"Error accepting clients: {ex.Message}");
                }
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                // Get port number from textbox
                int port = int.Parse(txtPort.Text);

                // Create and start TCP listener
                //Console.WriteLine(IPAddress.Any);
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();

                // Update server state
                isServerRunning = true;

                // Update UI elements
                lblServerStatus.Text = $"Server Status: Running on Port {port}";
                lblServerStatus.ForeColor = Color.Green;
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
                txtPort.Enabled = false; // Prevent port changes while running

                // Log the start
                AddServerLog($"Server started on port {port} with IP: {IPAddress.Any}");
                AddServerLog("Waiting for client connections...");

                // Start background thread to accept clients
                serverThread = new Thread(AcceptClients);
                serverThread.IsBackground = true; // Dies when main program exits
                serverThread.Start();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid port number (e.g., 8888)",
                               "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start server: {ex.Message}",
                               "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddServerLog($"Error starting server: {ex.Message}");
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            try
            {
                // Set server state to stopping
                isServerRunning = false;

                // Stop accepting new connections
                tcpListener?.Stop();

                // Disconnect all existing clients
                foreach (TcpClient client in connectedClients.ToArray())
                {
                    try
                    {
                        client?.Close();
                    }
                    catch
                    {
                        // Ignore individual client close errors
                    }
                }

                // Clear clients list
                connectedClients.Clear();

                // Wait for server thread to finish (with timeout)
                if (serverThread != null && serverThread.IsAlive)
                {
                    if (!serverThread.Join(2000)) // Wait max 2 seconds
                    {
                        serverThread.Abort(); // Force stop if needed
                    }
                }

                // Update UI elements
                lblServerStatus.Text = "Server Status: Stopped";
                lblServerStatus.ForeColor = Color.Red;
                btnStartServer.Enabled = true;
                btnStopServer.Enabled = false;
                txtPort.Enabled = true;

                // Update clients display
                lblClients.Text = "Connected Clients: 0";
                lstClients.Items.Clear();

                // Log the stop
                AddServerLog("Server stopped");
                AddServerLog("All clients disconnected");
            }
            catch (Exception ex)
            {
                AddServerLog($"Error stopping server: {ex.Message}");
                MessageBox.Show($"Error stopping server: {ex.Message}",
                               "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
