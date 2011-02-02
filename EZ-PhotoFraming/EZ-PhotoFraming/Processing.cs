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
using IWshRuntimeLibrary;

namespace PhotoFraming
{
    public partial class Processing : Form
    {
        int ptBoxDefault_Width, ptBoxDefault_Height;

        public Processing()
        {
            InitializeComponent();

            InitUI();
        }

        private void InitUI()
        {
            ptBoxDefault_Width = ptBox.Width;
            ptBoxDefault_Height = ptBox.Height;
        }

        private void dirTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
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
                if (Helper.ParsePath(fullname, out dir, out filename, out ext) == false) return;

                if (ext == ".lnk")
                {
                    WshShell wsh = new WshShell();
                    IWshShortcut wshort = (IWshShortcut)wsh.CreateShortcut(fullname);
                    if (Helper.ParsePath(wshort.TargetPath, out dir, out filename, out ext) == false) return;
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
                return;
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
            for (int i = dirTree.Nodes.Count-1; i >= 0; i--)
            {
                if (dirTree.Nodes[i].Checked)
                {
                    dirTree.Nodes[i].Remove();
                }
                else
                {
                    for (int j = dirTree.Nodes[i].Nodes.Count-1; j >= 0; j--)
                    {
                        if (dirTree.Nodes[i].Nodes[j].Checked)
                        {
                            dirTree.Nodes[i].Nodes[j].Remove();
                        }
                    }
                }

                if (dirTree.Nodes[i].Nodes.Count == 0)
                {
                    dirTree.Nodes[i].Remove();
                }
            }
        }


        Boolean ptBoxAutoScale;

        private void dirTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                ptBox.ImageLocation = e.Node.FullPath;
            }
            else
            {
                ptBox.CancelAsync();
                ptBox.ImageLocation = null;
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

        }

        private void startselectedBtn_Click(object sender, EventArgs e)
        {
            if (dirTree.SelectedNode.Parent != null)
            {
                backgroundWorker.RunWorkerAsync(dirTree.SelectedNode.FullPath);
                statusLbl.Text = "Stating Process with Background Worker";
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(10, "Process Initiating");
            String sourcefile = (String)e.Argument;
            String dir = Path.GetDirectoryName(sourcefile);
            String filename = Path.GetFileNameWithoutExtension(sourcefile);
            

            backgroundWorker.ReportProgress(20, "Loading Photo");
            Image imgPhoto = Image.FromFile(sourcefile, true);
            int FrameThickness = (int)(0.03 * imgPhoto.Width); // 10 pixels
            int ImageLengthLimit = 1280; // 1280 pixels

            backgroundWorker.ReportProgress(30, "Processing Photo");
            Bitmap bOutput = new Bitmap(imgPhoto.Width + 2 * FrameThickness,
                imgPhoto.Height + 2 * FrameThickness,
                imgPhoto.PixelFormat);
            bOutput.SetResolution(imgPhoto.HorizontalResolution,imgPhoto.VerticalResolution);

            Graphics gCanvas = Graphics.FromImage(bOutput);
            gCanvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gCanvas.SmoothingMode = SmoothingMode.AntiAlias;
            gCanvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Pen gPen = new Pen(Color.White, FrameThickness);
            int halfFrameThickness = FrameThickness / 2;
            gCanvas.DrawRectangle(gPen, halfFrameThickness, halfFrameThickness,
                bOutput.Width - FrameThickness, bOutput.Height - FrameThickness);
            gCanvas.DrawImageUnscaled(imgPhoto, new Point(FrameThickness, FrameThickness));

            backgroundWorker.ReportProgress(60, "Saving Processed Photo");
            String outputfile = Path.Combine(dir,filename +"-result.jpg");
            Helper.SaveJpeg(outputfile, bOutput, 100);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLbl.Text = "Processed Photo Saved";
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLbl.Text = (String)e.UserState;
        }

        
    }
}
