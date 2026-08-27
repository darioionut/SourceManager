using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SourceEngineManager
{
    public partial class Form1 : Form
    {
        private TextBox txtGamePath;
        private Button btnBrowse;
        private Button btnHammer;
        private Button btnModelViewer;
        private Button btnVpkCompiler;

        public Form1()
        {
            InitializeComponent();
            SetupCustomLayout();
        }

        private void SetupCustomLayout()
        {
            this.Text = "Source Engine Manager";
            this.Width = 450;
            this.Height = 300;

            Label lblPath = new Label() { Text = "Source Engine Game Folder:", Left = 20, Top = 20, Width = 300 };
            
            txtGamePath = new TextBox() { Left = 20, Top = 45, Width = 300 };
            
            btnBrowse = new Button() { Text = "Browse", Left = 330, Top = 44, Width = 80 };
            btnBrowse.Click += BtnBrowse_Click;

            btnHammer = new Button() { Text = "Hammer Editor", Left = 20, Top = 90, Width = 390, Height = 40 };
            btnHammer.Click += BtnHammer_Click;

            btnModelViewer = new Button() { Text = "Model Viewer", Left = 20, Top = 140, Width = 390, Height = 40 };
            btnModelViewer.Click += BtnModelViewer_Click;

            btnVpkCompiler = new Button() { Text = "VPK Compiler", Left = 20, Top = 190, Width = 390, Height = 40 };
            btnVpkCompiler.Click += BtnVpkCompiler_Click;

            this.Controls.Add(lblPath);
            this.Controls.Add(txtGamePath);
            this.Controls.Add(btnBrowse);
            this.Controls.Add(btnHammer);
            this.Controls.Add(btnModelViewer);
            this.Controls.Add(btnVpkCompiler);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtGamePath.Text = fbd.SelectedPath;
                }
            }
        }

        private void BtnHammer_Click(object sender, EventArgs e)
        {
            string gamePath = txtGamePath.Text;
            string hammerPath = Path.Combine(gamePath, "bin", "hammer.exe");

            if (File.Exists(hammerPath))
            {
                Process.Start(hammerPath);
            }
            else
            {
                MessageBox.Show("Could not find hammer.exe in the specified game directory's bin folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModelViewer_Click(object sender, EventArgs e)
        {
            string gamePath = txtGamePath.Text;
            string hlmvPath = Path.Combine(gamePath, "bin", "hlmv.exe");

            if (File.Exists(hlmvPath))
            {
                Process.Start(hlmvPath);
            }
            else
            {
                MessageBox.Show("Could not find hlmv.exe (Model Viewer) in the specified game directory's bin folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVpkCompiler_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select the folder you want to compile into a VPK:";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string targetFolder = fbd.SelectedPath;
                    string gamePath = txtGamePath.Text;
                    string vpkPath = Path.Combine(gamePath, "bin", "vpk.exe");

                    if (File.Exists(vpkPath))
                    {
                        Process.Start(vpkPath, $"\"{targetFolder}\"");
                    }
                    else
                    {
                        MessageBox.Show($"Selected Folder to VPK: {targetFolder}\n\nNote: vpk.exe was not found in your game's bin folder.", "VPK Compiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}