using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Speed_Hack_Loader
{
    public partial class SpeedHack : Form
    {
        public string tempPath;
        public string smiPath;
        public string cheatPath;

        public SpeedHack()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tempPath = Path.GetTempPath();

            smiPath = Path.Combine(tempPath, "smi.exe");

            cheatPath = Path.Combine(tempPath, "CheatDLL.dll");

            var assembly = Assembly.GetExecutingAssembly();

            ExtractBinaryResource(assembly, "Speed_Hack_Loader.CheatDLL.dll", Path.Combine(tempPath, "CheatDLL.dll"));

            ExtractBinaryResource(assembly, "Speed_Hack_Loader.SharpMonoInjector.dll", Path.Combine(tempPath, "SharpMonoInjector.dll"));

            ExtractBinaryResource(assembly, "Speed_Hack_Loader.smi.exe", Path.Combine(tempPath, "smi.exe"));

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                string processName = process.ProcessName;
                GameDropDown.Items.Add(processName);
            }
        }

        private void GameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GameLabel_Click(object sender, EventArgs e)
        {
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Process.Start(smiPath, $"inject -p \"{GameDropDown.SelectedItem.ToString()}\" -a \"{cheatPath}\" -n CheatDLL -c Loader -m Init");
        }

        static void ExtractBinaryResource(Assembly assembly, string resourceName, string outputPath)
        {
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    MessageBox.Show($"Resource {resourceName} not found!");
                    return;
                }

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
        }
    }
}