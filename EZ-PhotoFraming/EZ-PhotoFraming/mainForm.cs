using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoFraming
{
    public partial class mainForm : Form
    {
        int ptBoxDefault_Width, ptBoxDefault_Height;
        Boolean AllowDropNotProcessing;
        SettingsForm settingsForm;

        public mainForm()
        {
            InitializeComponent();

            InitUI();
        }

        private void InitUI()
        {
            ptBoxDefault_Width = ptBox.Width;
            ptBoxDefault_Height = ptBox.Height;
            AllowDropNotProcessing = true;

            settingsForm = new SettingsForm();
            settingsForm.VisibleChanged += new EventHandler(settingsForm_VisibleChanged);
        }

        private void dirTree_DragEnter(object sender, DragEventArgs e)
        {
            if (AllowDropNotProcessing && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dirTree_DragDrop(object sender, DragEventArgs e)
        {
            ProcessDrop((String[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void ProcessDrop(String[] eData)
        {
            foreach (var fullname in eData)
            {
                String dir, filename, ext;
                if (Helper.ParsePath(fullname, out dir, out filename, out ext) == false) continue;

                if (ext == ".lnk")
                {
                    IWshRuntimeLibrary.WshShell wsh = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut wshort = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(fullname);
                    if (Helper.ParsePath(wshort.TargetPath, out dir, out filename, out ext) == false) continue;
                }

                if (ext == String.Empty) // This is a Folder
                {
                    AddNodeDirectory(dir);
                }
                else
                {
                    if (checkExtension(ext))
                    {
                        AddNodeFile(dir, filename);
                    }
                }

                dirTree.Nodes[dir].Expand();
            }
        }

        private Boolean checkExtension(String ext)
        {
            ext = ext.Remove(0, 1);
            if (ext == "bmp"
                || ext == "jpg"
                || ext == "gif"
                || ext == "png"
                )
            {
                return true;
            }
            else return false;
        }

        private void AddNodeDirectory(String dir)
        {
            if (dirTree.Nodes[dir] == null)
            {
                dirTree.Nodes.Add(dir, dir);
            }
            else
            {
                dirTree.Nodes[dir].Nodes.Clear();
            }

            foreach (String fN in Directory.GetFiles(dir))
            {
                String ext = Path.GetExtension(fN);
                if (checkExtension(ext))
                {
                    String Key = Path.GetFileName(fN);
                    dirTree.Nodes[dir].Nodes.Add(Key, Key);
                }
            }

            if (dirTree.Nodes[dir].Nodes.Count == 0)
            {
                dirTree.Nodes[dir].Remove();
            }
        }

        private void AddNodeFile(String dir, String filename)
        {
            if (dirTree.Nodes[dir] == null)
            {
                dirTree.Nodes.Add(dir, dir);
            }

            if (dirTree.Nodes[dir].Nodes[filename] == null)
            {
                dirTree.Nodes[dir].Nodes.Add(filename, filename);
            }
        }

        private void removeDirBtn_Click(object sender, EventArgs e)
        {
            for (int i = dirTree.Nodes.Count - 1; i >= 0; i--)
            {
                if (dirTree.Nodes[i].Checked)
                {
                    dirTree.Nodes[i].Remove();
                }
                else
                {
                    for (int j = dirTree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
                    {
                        if (dirTree.Nodes[i].Nodes[j].Checked)
                        {
                            dirTree.Nodes[i].Nodes[j].Remove();
                        }
                    }
                    if (dirTree.Nodes[i].Nodes.Count == 0)
                    {
                        dirTree.Nodes[i].Remove();
                    }
                }
            }
        }


        Boolean ptBoxAutoScale;

        private void dirTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                ptBox.ImageLocation = e.Node.FullPath;
                statusLbl.Text = "Loading Preview - " + e.Node.FullPath;
            }
            else
            {
                ptBox.CancelAsync();
                ptBox.ImageLocation = null;
                statusLbl.Text = String.Empty;
            }
        }

        private void ptBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ptBox.Image.Width <= ptBoxDefault_Width
                    || ptBox.Image.Height <= ptBoxDefault_Height)
            {
                ptBoxAutoScale = false;
                ptBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                ptBoxAutoScale = true;
                AutoScalePtBox();
            }

            statusLbl.Text = dirTree.SelectedNode.FullPath;
        }

        private void ptBox_SizeChanged(object sender, EventArgs e)
        {
            if (ptBox.Image != null && ptBoxAutoScale == true)
            {
                AutoScalePtBox();
            }
        }

        private void AutoScalePtBox()
        {
            if (ptBox.Image.Width >= ptBox.Width
                    || ptBox.Image.Height >= ptBox.Height)
            {
                ptBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                ptBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        private void startallBtn_Click(object sender, EventArgs e)
        {
            for (int i = dirTree.Nodes.Count - 1; i >= 0; i--)
            {
                // Check all branches
                for (int j = dirTree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
                {
                    dirTree.Nodes[i].Nodes[j].Checked = true;
                }

                if (dirTree.Nodes[i].Nodes.Count == 0)
                {
                    dirTree.Nodes[i].Remove();
                }
            }

            StartUIProcessing();
        }

        private void startselectedBtn_Click(object sender, EventArgs e)
        {
            for (int i = dirTree.Nodes.Count - 1; i >= 0; i--)
            {
                if (dirTree.Nodes[i].Checked)
                {
                    // Check all branches
                    for (int j = dirTree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
                    {
                        dirTree.Nodes[i].Nodes[j].Checked = true;
                    }
                }

                if (dirTree.Nodes[i].Nodes.Count == 0)
                {
                    dirTree.Nodes[i].Remove();
                }
            }

            StartUIProcessing();
        }

        private void StartUIProcessing()
        {
            AllowDropNotProcessing = false;
            dirTree.Enabled = false;
            startallBtn.Enabled = false;
            startselectedBtn.Enabled = false;
            removeDirBtn.Enabled = false;
            settingsForm.groupBox1.Enabled = false;
            settingsForm.groupBox2.Enabled = false;
            StopProcessBtn.Enabled = true;
            StartProcessing();
        }

        private void StopUIProcessing()
        {
            AllowDropNotProcessing = true;
            dirTree.Enabled = true;
            startallBtn.Enabled = true;
            startselectedBtn.Enabled = true;
            removeDirBtn.Enabled = true;
            settingsForm.groupBox1.Enabled = true;
            settingsForm.groupBox2.Enabled = true;
            StopProcessBtn.Enabled = false;
            progressBar.Visible = false;
        }

        private void StartProcessing()
        {
            for (int i = dirTree.Nodes.Count - 1; i >= 0; i--)
            {
                if (dirTree.Nodes[i].Nodes.Count != 0)
                {
                    // Check all branches
                    for (int j = dirTree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
                    {
                        if (dirTree.Nodes[i].Nodes[j].Checked)
                        {
                            dirTree.Nodes[i].BackColor = Color.Yellow;
                            dirTree.Nodes[i].Nodes[j].BackColor = Color.Yellow;
                            backgroundWorker.RunWorkerAsync(dirTree.Nodes[i].Nodes[j].FullPath);
                            statusLbl.Text = "Starting Process with Background Worker";
                            progressBar.Visible = true;
                            return;
                        }
                    }
                }
            }

            // No Selected Node
            StopUIProcessing();
        }
        
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = e.Argument;

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(0, "Process Initiating");
            String sourcefile = (String)e.Argument;
            String dir = Path.GetDirectoryName(sourcefile);
            String filename = Path.GetFileNameWithoutExtension(sourcefile);

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(10, "Loading Photo");
            Image imgPhoto = Image.FromFile(sourcefile, true);
            int FrameThickness = (int)((double)Properties.Settings.Default.FrameThickness * 0.01 * imgPhoto.Width);
            int ImageLengthLimit = 1280; // 1280 pixels

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(30, "Processing Photo");
            Bitmap bOutput = new Bitmap(imgPhoto.Width + 2 * FrameThickness,
                imgPhoto.Height + 2 * FrameThickness,
                imgPhoto.PixelFormat);
            bOutput.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(50, "Processing Photo");
            Graphics gCanvas = Graphics.FromImage(bOutput);
            gCanvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gCanvas.SmoothingMode = SmoothingMode.AntiAlias;
            gCanvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(70, "Processing Photo");
            Pen gPen = new Pen(Color.White, FrameThickness);
            int halfFrameThickness = FrameThickness / 2;

            gCanvas.FillRectangle(new SolidBrush(Properties.Settings.Default.FrameColor), 0, 0, bOutput.Width, bOutput.Height);

            //gCanvas.DrawRectangle(gPen, halfFrameThickness, halfFrameThickness,
            //    bOutput.Width - FrameThickness, bOutput.Height - FrameThickness);
            gCanvas.DrawImageUnscaled(imgPhoto, new Point(FrameThickness, FrameThickness));

            if (backgroundWorker.CancellationPending == true) { e.Cancel = true; return; }
            backgroundWorker.ReportProgress(90, "Saving Processed Photo");
            String outputfile = Path.Combine(dir, filename + "-result.jpg");
            for(int i = 2 ; File.Exists(outputfile) ; i++)
            {
                outputfile = Path.Combine(dir, filename + "-result" + i.ToString() + ".jpg");
            }
            Helper.SaveJpeg(outputfile, bOutput, 100);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            String dir = Path.GetDirectoryName((String)e.Result);
            String filename = Path.GetFileName((String)e.Result);
            dirTree.Nodes[dir].BackColor = Color.Empty;
            dirTree.Nodes[dir].Nodes[filename].BackColor = Color.Empty;

            if (e.Cancelled)
            {
                statusLbl.Text = "Processing Cancelled";

                StopUIProcessing();
            }
            else
            {
                progressBar.Value = 100;
                statusLbl.Text = "Processed Photo Saved";

                if (dirTree.Nodes[dir] != null)
                {
                    if (dirTree.Nodes[dir].Nodes[filename] != null)
                    {
                        dirTree.Nodes[dir].Nodes[filename].Remove();
                    }

                    if (dirTree.Nodes[dir].Nodes.Count == 0)
                    {
                        dirTree.Nodes[dir].Remove();
                    }
                }

                StartProcessing();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            statusLbl.Text = (String)e.UserState;
        }

        private void stopProcessBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        
        private void settingsBtn_Click(object sender, EventArgs e)
        {
            if (settingsBtn.Checked)
            {
                settingsForm.Show(this);
            }
            else
            {
                settingsForm.Hide();
            }
        }

        private void settingsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!((Form)sender).Visible)
            {
                settingsBtn.Checked = false;
            }
        }
    }
}
