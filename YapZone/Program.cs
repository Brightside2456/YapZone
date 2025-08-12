using YapZone_Client;

namespace YapZone
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //// To customize application configuration such as set high DPI settings or default font,
            //// see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new ClientForm());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //DialogResult result = MessageBox.Show(
            //    "Choose application mode:\n\n" +
            //    "• Click YES to run as SERVER\n" +
            //    "• Click NO to run as CLIENT\n" +
            //    "• Click CANCEL to exit",
            //    "YapZone - Select Mode",
            //    MessageBoxButtons.YesNoCancel,
            //    MessageBoxIcon.Question
            //    );

            // Create custom selection form
            Form selectionForm = new Form()
            {
                Text = "YapZone",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Button serverBtn = new Button()
            {
                Text = "Start as SERVER",
                Size = new Size(120, 40),
                Location = new Point(20, 30),
                BackColor = Color.LimeGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            Button clientBtn = new Button()
            {
                Text = "Start as CLIENT",
                Size = new Size(120, 40),
                Location = new Point(150, 30),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Add click events
            serverBtn.Click += (s, e) => { selectionForm.DialogResult = DialogResult.Yes; };
            clientBtn.Click += (s, e) => { selectionForm.DialogResult = DialogResult.No; };

            // Add buttons to form
            selectionForm.Controls.Add(serverBtn);
            selectionForm.Controls.Add(clientBtn);

            // Show the custom form
            DialogResult result = selectionForm.ShowDialog();

            Console.WriteLine( result );
            if (result == DialogResult.Yes)
            {
                Application.Run(new ServerForm());
            }
            else if (result == DialogResult.No)
            {
                Application.Run(new ClientForm());
            }
            else {
                return;
            }
        }
    }
}