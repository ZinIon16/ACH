using ExcelDataReader;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //OpenFile();
        }
        //private void OpenFile()
        //{
        //    Excel excel = new Excel(@"demo.xlsx", 1);
        //    int i, j;

        //    for (i = 0; i < 5; i++)
        //    {
        //        for (j = 0; j < 2; j++)
        //        {
        //            //if ( (excel.(i, j) = null) ){
        //                MessageBox.Show((excel.ReadCell(i, j)));
        //            //}
        //            //else
        //            //{

        //            //}
        //        }
        //    }
        //}
        DataTableCollection tableCollection;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog() { Filter="Excel Workbook|*.xlsx" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.FileName;
                    using(var stream = File.Open(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using(IExcelDataReader reader =ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_)=>new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                            tableCollection =result.Tables;
                            cboSheet.Items.Clear();
                            foreach (DataTable table in tableCollection)
                            cboSheet.Items.Add(table);
                            
                        }
                    }
                }
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
            dt.CreateDataReader();
        }
    }
}
