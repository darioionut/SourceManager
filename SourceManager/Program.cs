using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Step 1: Ask for Source Engine game folder
        string gameFolder = SelectFolder("Select your Source Engine game folder");
        if (string.IsNullOrEmpty(gameFolder))
        {
            MessageBox.Show("No folder selected. Exiting.");
            return;
        }

        // Step 2: Ask what to open
        var choice = MessageBox.Show(
            "Choose YES for Hammer, NO for Model Viewer, CANCEL for VPK Tool",
            "Select Tool",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question
        );

        string toolPath = null;

        if (choice == DialogResult.Yes)
        {
            toolPath = Path.Combine(gameFolder, "bin", "hammer.exe");
        }
        else if (choice == DialogResult.No)
        {
            toolPath = Path.Combine(gameFolder, "bin", "hlmv.exe");
        }
        else if (choice == DialogResult.Cancel)
        {
            toolPath = Path.Combine(gameFolder, "bin", "vpk.exe");

            if (!File.Exists(toolPath))
            {
                MessageBox.Show("vpk.exe not found in bin folder.");
                return;
            }

            // Step 3: Ask what to pack into VPK
            string packFolder = SelectFolder("Select folder to pack into VPK");
            if (string.IsNullOrEmpty(packFolder))
            {
                MessageBox.Show("No folder selected for VPK.");
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = toolPath,
                    Arguments = $"\"{packFolder}\"",
                    UseShellExecute = false
                });
                MessageBox.Show($"Packing folder into VPK: {packFolder}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error running vpk.exe: {ex.Message}");
            }
            return;
        }

        // Step 4: Launch selected tool
        if (!File.Exists(toolPath))
        {
            MessageBox.Show($"Tool not found: {toolPath}");
            return;
        }

        try
        {
            Process.Start(toolPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error launching tool: {ex.Message}");
        }
    }

    static string SelectFolder(string description)
    {
        using (var dialog = new FolderBrowserDialog())
        {
            dialog.Description = description;
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
        }
        return null;
    }
}
