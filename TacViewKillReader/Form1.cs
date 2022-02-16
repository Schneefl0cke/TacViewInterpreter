using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TacViewKillReader
{
    public partial class Form1 : Form
    {
        Analyzer analyzer = new Analyzer();
        Output output = new Output();

        public Form1()
        {
            InitializeComponent();
        }

        private void analyzeTacView_button_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = string.Empty;
                var savePath = string.Empty;

                filePath = GetFilePath();

                if (filePath != String.Empty)
                {
                    savePath = GetSavePath(savePath);

                    var result = analyzer.AnalyzeSingleMission(filePath);

                    if (savePath != String.Empty)
                    {
                        output.CreateFile(result, savePath);
                        output.ConvertToExcelFormat(result, savePath);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private static string GetSavePath(string savePath)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savePath = saveFileDialog1.FileName;
            }

            return savePath;
        }

        private static string GetFilePath()
        {
            string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            return filePath;
        }

        private void multipleMissions_button_Click(object sender, EventArgs e)
        {
            try
            {
                var savePath = string.Empty;

                var filePaths = GetFilePaths();
                if (filePaths.Length != 0)
                {
                    savePath = GetSavePath(savePath);

                    var result = analyzer.AnalyzeMultipleFiles(filePaths.ToList());

                    if (savePath != String.Empty)
                    {
                        output.CreateFile(result, savePath);
                        output.ConvertToExcelFormat(result, savePath);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private static string[] GetFilePaths()
        {
            string[] fileNames = new string[0];
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    fileNames = openFileDialog.FileNames;
                }
            }

            return fileNames;
        }
    }
}
