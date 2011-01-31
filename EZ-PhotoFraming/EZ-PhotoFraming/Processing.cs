using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
                return;
            }
        }

        private void dirTree_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var fullname in (String[])e.Data.GetData(DataFormats.FileDrop))
            {
                String dir = Path.GetDirectoryName(fullname);
                String filename = Path.GetFileName(fullname);
                String ext = Path.GetExtension(fullname); // '' or '.zip' or '.msi'

                if (dir == String.Empty) return; // Invalid Path

                if (ext == String.Empty) // This is a Folder
                {
                    foreach (TreeNode node in dirTree.Nodes)
                    {
                        if (fullname == node.Text)
                        {
                            // Duplicate Directory Found - remove all listed files
                            node.Nodes.Clear();

                            // Adding files to the Directory Node
                            foreach (String tN in Directory.GetFiles(fullname))
                            {
                                node.Nodes.Add(Path.GetFileName(tN));
                            }
                            return;
                        }
                    }

                    // No Duplicate Directory
                    List<TreeNode> neonode = new List<TreeNode>();
                    foreach (String neofilename in Directory.GetFiles(fullname))
                    {
                        neonode.Add(new TreeNode(Path.GetFileName(neofilename)));
                    }
                    dirTree.Nodes.Add(new TreeNode(fullname,neonode.ToArray()));
                }
                else
                {
                    foreach (TreeNode node in dirTree.Nodes)
                    {
                        if (dir == node.Text)
                        {
                            foreach (TreeNode subnode in node.Nodes)
                            {
                                if (filename == subnode.Text)
                                {
                                    return; // Duplicate File Found
                                }
                            }

                            node.Nodes.Add(new TreeNode(filename));
                            return; // Duplicate Directory Found
                        }
                    }

                    // No Duplicate Directory or File Found
                    dirTree.Nodes.Add(new TreeNode(dir, new TreeNode[1] { new TreeNode(filename) }));
                }
            }

        }
    }
}
