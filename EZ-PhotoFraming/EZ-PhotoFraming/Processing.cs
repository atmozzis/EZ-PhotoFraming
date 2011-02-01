using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;

namespace PhotoFraming
{
    public partial class Processing : Form
    {
        public Processing()
        {
            InitializeComponent();
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
    }
}
