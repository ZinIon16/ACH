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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

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
        }

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBank.SelectedIndex == 0)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                txtCName.Text = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                txtCBNo.Text = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                txtCBrNo.Text = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                txtAcc.Text = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                txtlDesDataCenter.Text = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                txtOrID.Text = dataSet2.Tables["Banks"].Rows[0][5].ToString();
            }
            else if (cboBank.SelectedIndex == 1)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                txtCName.Text = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                txtCBNo.Text = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                txtCBrNo.Text = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                txtAcc.Text = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                txtlDesDataCenter.Text = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                txtOrID.Text = dataSet2.Tables["Banks"].Rows[1][5].ToString();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
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
            objBank.FileNo = txtFileNo.Text;
            objBank.CompanyBank = txtCBNo.Text;
            objBank.CompanyBranch = txtCBrNo.Text;
            objBank.CompanyAccount = txtAcc.Text;
            objBank.Header = txtHeader.Text;
            objBank.DestinationDataCenter = txtlDesDataCenter.Text;
            objBank.OriginatorID = txtOrID.Text;    
            objBank.CompanyName = txtCName.Text;

            //Adding to XML
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Banks";
            dataTable.Columns.Add("CompanyName");
            dataTable.Columns.Add("CompanyBankNumber");
            dataTable.Columns.Add("CompanyBranchNumber");
            dataTable.Columns.Add("CompanyAccountNumber");
            dataTable.Columns.Add("DestinationDataCenter");
            dataTable.Columns.Add("OriginatorID");
            dataSet.Tables.Add(dataTable);

            DataRow row1 = dataSet.Tables["Banks"].NewRow();
            DataRow row2 = dataSet.Tables["Banks"].NewRow();
            dataSet.Tables["Banks"].Rows.Add(row1);
            dataSet.Tables["Banks"].Rows.Add(row2);
            DataSet dataSet2 = new DataSet();
            if (File.Exists("C:\\Users\\Latitude\\Downloads\\Bank.xml"))
            {
                dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                if (dataSet2.Tables["Banks"].Rows.Count <= 1)
                {
                    row1["CompanyName"] = "";
                    row1["CompanyBankNumber"] = "";
                    row1["CompanyBranchNumber"] = "";
                    row1["CompanyAccountNumber"] = "";
                    row1["DestinationDataCenter"] = "";
                    row1["OriginatorID"] = "";

                    row2["CompanyName"] = "";
                    row2["CompanyBankNumber"] = "";
                    row2["CompanyBranchNumber"] = "";
                    row2["CompanyAccountNumber"] = "";
                    row2["DestinationDataCenter"] = "";
                    row2["OriginatorID"] = "";

                    dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                }
            }
            else
            {
                row1["CompanyName"] = "";
                row1["CompanyBankNumber"] = "";
                row1["CompanyBranchNumber"] = "";
                row1["CompanyAccountNumber"] = "";
                row1["DestinationDataCenter"] = "";
                row1["OriginatorID"] = "";

                row2["CompanyName"] = "";
                row2["CompanyBankNumber"] = "";
                row2["CompanyBranchNumber"] = "";
                row2["CompanyAccountNumber"] = "";
                row2["DestinationDataCenter"] = "";
                row2["OriginatorID"] = "";

                dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
            }

            if (cboBank.SelectedIndex == 0)
            {
                row1["CompanyName"] = txtCName.Text;
                row1["CompanyBankNumber"] = txtCBNo.Text;
                row1["CompanyBranchNumber"] = txtCBrNo.Text;
                row1["CompanyAccountNumber"] = txtAcc.Text;
                row1["DestinationDataCenter"] = txtlDesDataCenter.Text;
                row1["OriginatorID"] = txtOrID.Text;

                row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
            }
            else if (cboBank.SelectedIndex == 1)
            {
                row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                row1["OriginatorID"] = dataSet2.Tables["Banks"].Rows[0][5].ToString();

                row2["CompanyName"] = txtCName.Text;
                row2["CompanyBankNumber"] = txtCBNo.Text;
                row2["CompanyBranchNumber"] = txtCBrNo.Text;
                row2["CompanyAccountNumber"] = txtAcc.Text;
                row2["DestinationDataCenter"] = txtlDesDataCenter.Text;
                row2["OriginatorID"] = txtOrID.Text;

                dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
            }

            objBank.Export(dt);
        }
    }
}