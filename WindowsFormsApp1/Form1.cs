using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

//ACH SOFTWARE

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //Form1 obj = new Form1();
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
        private DataTableCollection tableCollection;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.FileName;
                    using (var stream = File.Open(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = ExcelDataReader => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = false
                                }
                            });
                            tableCollection = result.Tables;
                            cboSheet.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                cboSheet.Items.Add(table.TableName);
                            //String Sheet= cboSheet.Items.Add(table).toString();
                            cboBank.Items.Clear();
                            cboBank.Items.Add("BMO");
                            cboBank.Items.Add("RBC");
                        }
                    }
                }
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
            //var data= dt.AsDataView();
            //----------------------------------------------------
            //----------------------------------------------------
            //StreamWriter File = new StreamWriter("demo.txt");
            //File.Write(data);
            //File.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string FileNo,NoOfDays,CompanyBank,CompanyBranch,CompanyAccount,Header,DestinationDataCenter,OriginatorID,CompanyName,TCO;

            FileNo= txtFileNo.Text;
            NoOfDays = txtNoOfDays.Text;
            CompanyBank = txtCBNo.Text;
            CompanyBranch=txtCBrNo.Text;
            CompanyAccount =txtAcc.Text;
            Header = txtHeader.Text;
            DestinationDataCenter = txtlDesDataCenter.Text;
            OriginatorID = txtOrID.Text;
            CompanyName = txtCName.Text;
            TCO = txtTco.Text;
            BankFile objBank = null;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (cboBank.SelectedIndex == 0)
            {
                objBank = new BMO();
            }
            else if (cboBank.SelectedIndex == 1)
            {
                objBank = new RBC();
            }
            objBank.Export(dt, FileNo, NoOfDays,  CompanyBank, CompanyBranch, CompanyAccount, Header, DestinationDataCenter,OriginatorID, CompanyName,TCO);
        }
    }
}
