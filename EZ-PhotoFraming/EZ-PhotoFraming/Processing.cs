using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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
                statusLbl.Text = "Processing with Background Worker";
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String dir = (String)e.Argument;
            int FrameThicnkness = 10; // 10 pixels

            Image imgPhoto = Image.FromFile(dir);
            int phWidth = imgPhoto.Width; int phHeight = imgPhoto.Height;

            Bitmap bmPhoto = new Bitmap(phWidth + 20, phHeight + 20, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.FillRectangle(new Brush(, new Rectangle(0, 0, bmPhoto.Width, bmPhoto.Height));
            grPhoto.DrawImage(imgPhoto, new Rectangle(10, 10, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

            imgPhoto = Image.FromHbitmap(grPhoto);
            //grPhoto.Dispose();
            //grWatermark.Dispose();

            //\\watermark_final.jpg", 
            //imgPhoto.Save(WorkingDirectory + "
            //    ImageFormat.Jpeg);
            //imgPhoto.Dispose();
            //imgWatermark.Dispose();

            e.Result = e.Argument;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLbl.Text = (String)e.Result;
        }

        
    }
}
