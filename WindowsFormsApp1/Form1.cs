using ExcelDataReader;
using System;
using System.Data;
using System.Drawing;
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
            btnUpdate.Enabled = false;
        }

        private DataTableCollection tableCollection;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Allowing user to choose the excel file
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
                           //Adding items in combo boxes
                            cboBank.Items.Clear();
                            cboBank.Items.Add("BMO");
                            cboBank.Items.Add("RBC");
                            cboOrID.Items.Clear();
                            cboOrID.Items.Add("Credit");
                            cboOrID.Items.Add("Debit");
                            cboOrID.Text = "Credit/Debit";
                        }
                    }
                }
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Getting the excel sheet data inside the data table so that we can display it in grid view
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];

            dataGridView1.DataSource = dt;
            bool error = false;

            //Validation Check for Amount
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "Amount" || dt.Rows[i][j].ToString() == "Account")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains(" ") == true)
                            {
                                error = true;
                                dataGridView1.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            if (error == true)
            {
                DialogResult result = MessageBox.Show("Highlighted field(s) have white spaces, do you want to make changes?", " Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //nothing
                }
                else
                {
                    MessageBox.Show("Click on Export to proceed!", "Click on OK");
                }
            }

            //Validation check for TransitCode
            error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TransitCode")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (dataGridView1.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (dataGridView1.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                            {
                                if (dataGridView1.Rows[x].Cells[j].Value.ToString().Length != 9)
                                {
                                    error = true;
                                    btnExport.Enabled = false;
                                    btnUpdate.Enabled = true;
                                    dataGridView1.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                    dataGridView1.Rows[x].Cells[j].Value = dataGridView1.Rows[x].Cells[j].Value.ToString() + "*";
                                }
                            }
                        }
                    }
                }
            }
            if (error == true)
            {
                MessageBox.Show("The Transit Code must be of 9 digits, change it and then click on" + "Update Button" + " in order to proceed!", " Transit Code Error");
            }

            //Checking if file doesn't exist. If it doesn't exists then the program will create the file automatically. 
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Banks";
            dataTable.Columns.Add("CompanyName");
            dataTable.Columns.Add("CompanyBankNumber");
            dataTable.Columns.Add("CompanyBranchNumber");
            dataTable.Columns.Add("CompanyAccountNumber");
            dataTable.Columns.Add("DestinationDataCenter");
            dataTable.Columns.Add("OriginatorID");
            dataTable.Columns.Add("Credit");
            dataTable.Columns.Add("Debit");
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
                    row1["Credit"] = "";
                    row1["Debit"] = "";

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
                row1["Credit"] = "";
                row1["Debit"] = "";

                row2["CompanyName"] = "";
                row2["CompanyBankNumber"] = "";
                row2["CompanyBranchNumber"] = "";
                row2["CompanyAccountNumber"] = "";
                row2["DestinationDataCenter"] = "";
                row2["OriginatorID"] = "";

                dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
            }
        }

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrID.Clear();
                if (cboBank.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                    txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][0].ToString());
                    txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][1].ToString());
                    txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][2].ToString());
                    txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][3].ToString());
                    txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][4].ToString());

                }
                else if (cboBank.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                    txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][0].ToString());
                    txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][1].ToString());
                    txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][2].ToString());
                    txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][3].ToString());
                    txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][4].ToString());
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][5].ToString());
                }
         

            //condition to check the bank and display originator id accordingly

            if (cboBank.SelectedIndex == 0)
            {

                lblFileType.Visible = true;
                cboOrID.Visible = true;
                txtHeader.Visible = false;
                lblHeader.Visible = false;
            }
            else
            {
                cboOrID.Visible = false;
                lblFileType.Visible = false;
                txtHeader.Visible = true;
                lblHeader.Visible = true;
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
            if (cboBank.SelectedIndex == 0)
            {
                    objBank.OriginatorID = cboOrID.Text;
            }
            else
            {
                objBank.OriginatorID = txtOrID.Text;
            }

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
            dataTable.Columns.Add("Credit");
            dataTable.Columns.Add("Debit");
            dataSet.Tables.Add(dataTable);

            DataRow row1 = dataSet.Tables["Banks"].NewRow();
            DataRow row2 = dataSet.Tables["Banks"].NewRow();
            dataSet.Tables["Banks"].Rows.Add(row1);
            dataSet.Tables["Banks"].Rows.Add(row2);
            DataSet dataSet2 = new DataSet();
       
            dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
  
            if (cboBank.SelectedIndex == 0)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row1["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row1["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row1["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row1["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row1["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row1["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row1["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row1["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row1["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][6].ToString();
                    row1["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                }
            }
            if (cboBank.SelectedIndex == 1)
            {
                row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][6].ToString();
                row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();

                row2["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                row2["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                row2["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                row2["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                row2["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                row2["OriginatorID"] = Eramake.eCryptography.Encrypt(txtOrID.Text);

                dataSet.WriteXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
            }

            objBank.Export(dt);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;

            //Validation check for TransitCode after changes
            int check = 0;
            bool error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TransitCode")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (dataGridView1.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (dataGridView1.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                            {
                                if ((dataGridView1.Rows[x].Cells[j].Value.ToString().Length != 9) || dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("*"))
                                {
                                    error = true;
                                    btnExport.Enabled = false;
                                    dataGridView1.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                }
                                else
                                {
                                    check = check + 1;
                                    dataGridView1.Rows[x].Cells[j].Style.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
            if (error == true)
            {
                MessageBox.Show("The Transit Code must be of 9 digits, change it and then click on" + "Update Button" + " in order to proceed!", " Transit Code Error");
            }
            if (dt.Rows.Count - 1 == check)
            {
                btnExport.Enabled = true;
                MessageBox.Show("The Transit Code has been updated successfully!", " Success");
            }
        }


        private void cboOrID_SelectedIndexChanged(object sender, EventArgs e)
        {
         

            if (cboOrID.SelectedIndex == 0)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][6].ToString());
              
            }
            else if (cboOrID.SelectedIndex == 1)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("C:\\Users\\Latitude\\Downloads\\Bank.xml");
                txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][7].ToString());

            }

   
        }
        private void btnExportxl_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                Excel.Application.Workbooks.Add(Type.Missing);
            
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        for (int k = 0; k < (dataGridView1.Rows.Count) - 1; k++)
                        {
                            Excel.Cells[k + 1, j + 1] = "'" + (dataGridView1.Rows[k].Cells[j].Value.ToString());
                        }
                    }


                Excel.Columns.AutoFit();
                Excel.Visible = true;
            }
        }

    }
}