using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zippy
{
    public partial class frmZip : Form
    {
        public frmZip()
        {
            InitializeComponent();
        }
        byte[] bytes;
        string fileName;
        string filePath;
        string fileExt;

        private void btnZip_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                var dr = fbd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string filePath = fbd.SelectedPath;
                    Convert.zipFolder(filePath, filePath + ".zip");
                    MessageBox.Show("DONE");
                }
            }
            catch
            {

                MessageBox.Show("File already exists", "Error");
            }
        }

        private void btnUnzip_Click(object sender, EventArgs e)
        {

            try
            {
                var ofd = new OpenFileDialog();
                var dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string filepath = ofd.FileName;
                    Convert.unzipFolder(filepath, Path.Combine(Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath)));
                    MessageBox.Show("DONE");
                }
            }
            catch
            {

                MessageBox.Show("File already exists", "Error");
            }
        }

        private void btnConvertToByte_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string filepath = ofd.FileName;
                fileExt = Path.GetExtension(ofd.FileName);
                fileName = Path.GetFileNameWithoutExtension(filepath);
                bytes = Convert.convertToByte(filepath);
                MessageBox.Show("The Matrix has been created");
            }
        }

        private void btnByteTo_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                filePath = fbd.SelectedPath;
                string newpath = Path.Combine(filePath, fileName + fileExt);
                Convert.convertByteTo(bytes, newpath);
                MessageBox.Show("You have defeated the final boss", "We got a winner");
            }
        }

        private void btnZip_DragDrop(object sender, DragEventArgs e)
        {
            
            Cursor.Current = Cursors.WaitCursor;
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var filePath in filePaths) {
                Convert.zipFolder(filePath, filePath + ".zip");
            }

            MessageBox.Show("DONE");
        }

        private void btnUnzip_DragDrop(object sender, DragEventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var filePath in filePaths)
            {
                Convert.unzipFolder(filePath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));
            }
            MessageBox.Show("DONE");

        }

        private void btnZip_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnUnzip_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
