using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.ComponentModel;


namespace Fortnite_Pinger
{
    public partial class Form1 : Form
    {
        private int pingCount;
        private String targetDomain;
        private int pingInterval;
        private Timer timer;
        private string fortniteURL;
        private int latencyLimit;

        private BackgroundWorker pingWorker;

        private bool isDragging;
        private Point lastCursorPosition;


        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            ReadConfig();

            pingWorker = new BackgroundWorker();
            pingWorker.WorkerSupportsCancellation = true;
            pingWorker.DoWork += PingWorker_DoWork;
            pingWorker.RunWorkerAsync();

            timer.Interval = pingInterval;
            timer.Start();


            panel1.MouseDown += Form1_MouseDown;
            panel1.MouseMove += Form1_MouseMove;
            panel1.MouseUp += Form1_MouseUp;

            panel1.MouseEnter += Form1_MouseEnter;
            panel1.MouseLeave += Form1_MouseLeave;
            pictureBox1.MouseEnter += Form1_MouseEnter;
            pictureBox1.MouseLeave += Form1_MouseEnter;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursorPosition = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - lastCursorPosition.X;
                int deltaY = e.Y - lastCursorPosition.Y;

                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        //Mouse Hover Opacity Changes
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.3;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
            Process.Start(fortniteURL);

        }

        private void ReadConfig()
        {
            try
            {
                pingCount = int.Parse(ConfigurationManager.AppSettings["PING_COUNT"]);
                targetDomain = ConfigurationManager.AppSettings["TARGET_DOMAIN"];
                pingInterval = int.Parse(ConfigurationManager.AppSettings["PING_INTERVAL"]);
                fortniteURL = ConfigurationManager.AppSettings["FORTNITE_URL"];
                latencyLimit = int.Parse(ConfigurationManager.AppSettings["LATENCY_LIMIT"]);
            }
            catch (Exception ex)
            {
                // Handle any errors when reading config values
                MessageBox.Show("Error reading configuration: " + ex.Message);
                Environment.Exit(0); // Close the application
            }
        }

        private void PingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!pingWorker.CancellationPending)
            {
                Process process = new Process();
                process.StartInfo.FileName = "ping";
                process.StartInfo.Arguments = $"-n {pingCount} {targetDomain}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Match match = Regex.Match(output, @"Average = (\d+)ms");
                if (match.Success)
                {
                    int avgLatency = int.Parse(match.Groups[1].Value);
                    pictureBox1.BackgroundImage = avgLatency < latencyLimit ? Properties.Resources.iconReady : Properties.Resources.iconNotReady;
                    pictureBox2.BackgroundImage = avgLatency < latencyLimit ? Properties.Resources.textReady : Properties.Resources.textNotReady;

                    // Update UI on the main thread
                    this.Invoke((MethodInvoker)delegate {
                        label1.Text = $"Average Latency: {avgLatency} ms";
                 
                    });
                }

                // Wait for the specified interval
                System.Threading.Thread.Sleep(pingInterval*1000);
            }
        }

    }

}
