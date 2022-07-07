using ExcelDataReader;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BankFile;

//ACH SOFTWARE

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable dt;
        int cboBankSelectedIndex, cboOrIDSelectedIndex;
        string FileName;
        int BankTypeIndex;
        public Form1(DataTable dt, int bankindex, int filetindex, string BankName, string FileType,string FileName/*,int BankTypeIndex*/)
        {
            InitializeComponent();
            this.dt= dt;
            this.cboBankSelectedIndex= bankindex;
            this.cboOrIDSelectedIndex = filetindex;
            this.txtBankName.Text = BankName;
            this.txtFileType.Text = FileType;
            this.FileName = FileName;
            //this.BankTypeIndex = BankTypeIndex;
            DataSet dataSet2 = new DataSet();
            dataSet2.ReadXml("Bank.xml");
            if (dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][6].ToString() != "")
            {
                BankTypeIndex = 0;
            }
            else
            {
                BankTypeIndex = 1;
            }
            if (BankTypeIndex == 1 || cboBankSelectedIndex == 0 || cboBankSelectedIndex == 2 || cboBankSelectedIndex == 3 || cboBankSelectedIndex == 4 || cboBankSelectedIndex == 7 || cboBankSelectedIndex == 8 || cboBankSelectedIndex == 9 || cboBankSelectedIndex == 10)
            {
                lblFileType.Visible = true;
                //cboOrID.Visible = true;
                txtHeader.Visible = false;
                lblHeader.Visible = false;
            }
            else
            {
                //cboOrID.Visible = false;
                //lblFileType.Visible = false;
                txtHeader.Visible = true;
                lblHeader.Visible = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //btnUpdate.Enabled = false;
            dataGridView1.DataSource = dt;
            dt.AcceptChanges();
            lblTotAmount.Visible = true;
            TotAmount.Visible = true;


            long TotalAmount = 0;
            decimal Total = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "Amount")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            TotalAmount = Convert.ToInt64(Convert.ToDecimal(dt.Rows[x][j].ToString()) * 100) + TotalAmount;
                        }
                    }
                }
            }

            Total = Convert.ToDecimal(TotalAmount);
            Total = Total / 100;
            TotAmount.Text = Total.ToString();


            

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
            dataTable.Columns.Add("HeaderCredit");
            dataTable.Columns.Add("FileNumber");
            dataTable.Columns.Add("Password", typeof(String)).SetOrdinal(10);
            dataTable.Columns.Add("HeaderDebit");
            dataSet.Tables.Add(dataTable);

            DataRow row1 = dataSet.Tables["Banks"].NewRow();
            DataRow row2 = dataSet.Tables["Banks"].NewRow();
            DataRow row3 = dataSet.Tables["Banks"].NewRow();
            DataRow row4 = dataSet.Tables["Banks"].NewRow();
            DataRow row5 = dataSet.Tables["Banks"].NewRow();
            DataRow row6 = dataSet.Tables["Banks"].NewRow();
            DataRow row7 = dataSet.Tables["Banks"].NewRow();
            DataRow row8 = dataSet.Tables["Banks"].NewRow();
            DataRow row9 = dataSet.Tables["Banks"].NewRow();
            DataRow row10 = dataSet.Tables["Banks"].NewRow();
            DataRow row11 = dataSet.Tables["Banks"].NewRow();
            //DataRow row12 = dataSet.Tables["Banks"].NewRow();
            dataSet.Tables["Banks"].Rows.Add(row1);
            dataSet.Tables["Banks"].Rows.Add(row2);
            dataSet.Tables["Banks"].Rows.Add(row3);
            dataSet.Tables["Banks"].Rows.Add(row4);
            dataSet.Tables["Banks"].Rows.Add(row5);
            dataSet.Tables["Banks"].Rows.Add(row6);
            dataSet.Tables["Banks"].Rows.Add(row7);
            dataSet.Tables["Banks"].Rows.Add(row8);
            dataSet.Tables["Banks"].Rows.Add(row9);
            dataSet.Tables["Banks"].Rows.Add(row10);
            dataSet.Tables["Banks"].Rows.Add(row11);
            //dataSet.Tables["Banks"].Rows.Add(row12);

            dataSet.Merge(dataTable);
            DataSet dataSet2 = new DataSet();
            if (File.Exists("Bank.xml"))
            {
                dataSet2.ReadXml("Bank.xml");
               
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
                row1["FileNumber"] = "";
                row1["Password"] = "6hGvVnbkaGpo8mUHOX8EHQ==";

                row2["CompanyName"] = "";
                row2["CompanyBankNumber"] = "";
                row2["CompanyBranchNumber"] = "";
                row2["CompanyAccountNumber"] = "";
                row2["DestinationDataCenter"] = "";
                row2["Credit"] = "";
                row2["Debit"] = "";
                row2["OriginatorID"] = "";
                row2["HeaderCredit"] = "";
                row2["FileNumber"] = "";
                row2["HeaderDebit"] = "";

                row3["CompanyName"] = "";
                row3["CompanyBankNumber"] = "";
                row3["CompanyBranchNumber"] = "";
                row3["CompanyAccountNumber"] = "";
                row3["DestinationDataCenter"] = "";
                row3["Credit"] = "";
                row3["Debit"] = "";
                row3["FileNumber"] = "";

                row3["CompanyName"] = "";
                row3["CompanyBankNumber"] = "";
                row3["CompanyBranchNumber"] = "";
                row3["CompanyAccountNumber"] = "";
                row3["DestinationDataCenter"] = "";
                row3["Credit"] = "";
                row3["Debit"] = "";
                row3["FileNumber"] = "";

                row4["CompanyName"] = "";
                row4["CompanyBankNumber"] = "";
                row4["CompanyBranchNumber"] = "";
                row4["CompanyAccountNumber"] = "";
                row4["DestinationDataCenter"] = "";
                row4["Credit"] = "";
                row4["Debit"] = "";
                row4["FileNumber"] = "";

                row5["CompanyName"] = "";
                row5["CompanyBankNumber"] = "";
                row5["CompanyBranchNumber"] = "";
                row5["CompanyAccountNumber"] = "";
                row5["DestinationDataCenter"] = "";
                row5["Credit"] = "";
                row5["Debit"] = "";
                row5["FileNumber"] = "";

                row6["CompanyName"] = "";
                row6["CompanyBankNumber"] = "";
                row6["CompanyBranchNumber"] = "";
                row6["CompanyAccountNumber"] = "";
                row6["DestinationDataCenter"] = "";
                row6["Credit"] = "";
                row6["Debit"] = "";
                row6["OriginatorID"] = "";
                row6["HeaderCredit"] = "";
                row6["FileNumber"] = "";
                row6["HeaderDebit"] = "";

                row7["CompanyName"] = "";
                row7["CompanyBankNumber"] = "";
                row7["CompanyBranchNumber"] = "";
                row7["CompanyAccountNumber"] = "";
                row7["DestinationDataCenter"] = "";
                row7["Credit"] = "";
                row7["Debit"] = "";
                row7["OriginatorID"] = "";
                row7["HeaderCredit"] = "";
                row7["FileNumber"] = "";
                row7["HeaderDebit"] = "";

                row8["CompanyName"] = "";
                row8["CompanyBankNumber"] = "";
                row8["CompanyBranchNumber"] = "";
                row8["CompanyAccountNumber"] = "";
                row8["DestinationDataCenter"] = "";
                row8["Credit"] = "";
                row8["Debit"] = "";
                row8["FileNumber"] = "";

                row9["CompanyName"] = "";
                row9["CompanyBankNumber"] = "";
                row9["CompanyBranchNumber"] = "";
                row9["CompanyAccountNumber"] = "";
                row9["DestinationDataCenter"] = "";
                row9["Credit"] = "";
                row9["Debit"] = "";
                row9["FileNumber"] = "";

                row10["CompanyName"] = "";
                row10["CompanyBankNumber"] = "";
                row10["CompanyBranchNumber"] = "";
                row10["CompanyAccountNumber"] = "";
                row10["DestinationDataCenter"] = "";
                row10["Credit"] = "";
                row10["Debit"] = "";
                row10["FileNumber"] = "";

                row11["CompanyName"] = "";
                row11["CompanyBankNumber"] = "";
                row11["CompanyBranchNumber"] = "";
                row11["CompanyAccountNumber"] = "";
                row11["DestinationDataCenter"] = "";
                row11["Credit"] = "";
                row11["Debit"] = "";
                row11["FileNumber"] = "";

              
                dataSet.WriteXml("Bank.xml");

                
            }
            DisplayBank();
            GetOriginiatorID();
        }

        private bool ExportBtnClicked = false;
       

        private long FileNumber = 0;

        public void DisplayBank()
        {

            txtDate.Text = DateTime.Now.Date.ToString("d");
            //cboOrID.Text = "Credit/Debit";
            txtOrID.Clear();

            //condition to check the bank and display originator id accordingly

            if (cboBankSelectedIndex != -1)
          {  

            if (BankTypeIndex == 0)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4].ToString());
                    //txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][5].ToString());
                    //txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][6].ToString());
                    if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (BankTypeIndex == 1)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
             }
            }
           
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewCell item in this.dataGridView1.SelectedCells)
                {
                    dataGridView1.Rows.RemoveAt(item.RowIndex);
                }
                foreach (DataGridViewCell item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.RowIndex);
                }
            }
            catch(Exception)
            {
                //nothing
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
           

                ExportBtnClicked = true;
                BankFile.BankFile objBank = null;
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.AcceptChanges();

                if (BankTypeIndex == 1 )
                {
                    objBank = new BMO();
                }
                else if (BankTypeIndex == 0 )
                {
                    objBank = new RBC();
                }
                DateTime date;
                date = Convert.ToDateTime(txtDate.Text);
                if (date < DateTime.Now.Date)
                {
                    MessageBox.Show("Please enter a valid date and then click on update");
                }
                else
                {
                    objBank.FileNo = txtFileNo.Text;
                    objBank.CompanyBank = txtCBNo.Text;
                    objBank.CompanyBranch = txtCBrNo.Text;
                    objBank.CompanyAccount = txtAcc.Text;
                    objBank.Header = txtHeader.Text;
                    objBank.DestinationDataCenter = txtlDesDataCenter.Text;
                    objBank.OriginatorID = txtOrID.Text;
                    objBank.CompanyName = txtCName.Text;
                    objBank.NoOfDays = txtDate.Text;

                if (BankTypeIndex == 1 || BankTypeIndex == 0)
                {
                    if (cboOrIDSelectedIndex == 0)
                    {
                        objBank.FileType = "C";
                    }
                    else if (cboOrIDSelectedIndex == 1)
                    {
                        objBank.FileType = "D";
                    }
                }

                    objBank.FileName = FileName;
                    objBank.Export(dt);
                if (ExportBtnClicked == true)
                {
                    MessageBox.Show("Text file has been created", " Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }


        private void GetOriginiatorID()
        {
            if (BankTypeIndex == 1 && cboBankSelectedIndex != -1)
            {
                if (cboOrIDSelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][7].ToString());
                }
                else if (cboOrIDSelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][8].ToString());
                }
            }
            else if (BankTypeIndex == 0 && cboBankSelectedIndex != -1)
            {
                if (cboOrIDSelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][7].ToString());
                    txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][6].ToString());
                }
                else if (cboOrIDSelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][8].ToString());
                    txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][10].ToString());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
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
                dataTable.Columns.Add("HeaderCredit");
                dataTable.Columns.Add("FileNumber");
                dataTable.Columns.Add("Password", typeof(String)).SetOrdinal(10);
                dataTable.Columns.Add("HeaderDebit");
            dataSet.Tables.Add(dataTable);

                DataRow row1 = dataSet.Tables["Banks"].NewRow();
                DataRow row2 = dataSet.Tables["Banks"].NewRow();
                DataRow row3 = dataSet.Tables["Banks"].NewRow();
                DataRow row4 = dataSet.Tables["Banks"].NewRow();
                DataRow row5 = dataSet.Tables["Banks"].NewRow();
                DataRow row6 = dataSet.Tables["Banks"].NewRow();
                DataRow row7 = dataSet.Tables["Banks"].NewRow();
                DataRow row8 = dataSet.Tables["Banks"].NewRow();
                DataRow row9 = dataSet.Tables["Banks"].NewRow();
                DataRow row10 = dataSet.Tables["Banks"].NewRow();
                DataRow row11 = dataSet.Tables["Banks"].NewRow();
                //DataRow row12 = dataSet.Tables["Banks"].NewRow();
                dataSet.Tables["Banks"].Rows.Add(row1);
                dataSet.Tables["Banks"].Rows.Add(row2);
                dataSet.Tables["Banks"].Rows.Add(row3);
                dataSet.Tables["Banks"].Rows.Add(row4);
                dataSet.Tables["Banks"].Rows.Add(row5);
                dataSet.Tables["Banks"].Rows.Add(row6);
                dataSet.Tables["Banks"].Rows.Add(row7);
                dataSet.Tables["Banks"].Rows.Add(row8);
                dataSet.Tables["Banks"].Rows.Add(row9);
                dataSet.Tables["Banks"].Rows.Add(row10);
                dataSet.Tables["Banks"].Rows.Add(row11);
                //dataSet.Tables["Banks"].Rows.Add(row12);
                dataSet.Merge(dataTable);
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                //DataRow row13 = dataSet2.Tables["Banks"].NewRow();
                //dataSet2.Tables["Banks"].Rows.Add(row13);


            string password = Microsoft.VisualBasic.Interaction.InputBox("Enter the password", "Password", "", 240, 160);
         
            if (/*password != "1"*/ password != Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][11].ToString()))
            {
                MessageBox.Show("Incorrect Password", "Error");
            }
            else
            {
                MessageBox.Show("Correct Password", "Success!");
                //checking new banks
                if(cboBankSelectedIndex != -1)
                {
                    if (BankTypeIndex == 1)
                    {
                        if (cboOrIDSelectedIndex == 0)
                        {
                            //credit
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][7] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            //dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9] = Eramake.eCryptography.Encrypt(txtFileNo.Text);


                            //row12["Password"] = "6hGvVnbkaGpo8mUHOX8EHQ==";
                            dataSet2.WriteXml("Bank.xml");
                        }
                        else
                        {
                            //debit
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                            //row1["Credit"] = dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][7].ToString();
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][8] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9] = Eramake.eCryptography.Encrypt(txtFileNo.Text);


                            dataSet2.WriteXml("Bank.xml");
                        }
                    }
                    else if (BankTypeIndex == 0)
                    {
                        if (cboOrIDSelectedIndex == 0)
                        {
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][7] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            //dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][5] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][6] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9] = Eramake.eCryptography.Encrypt(txtFileNo.Text);


                            dataSet2.WriteXml("Bank.xml");
                        }
                        else
                        {
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][8] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            //dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][5] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][10] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                            dataSet2.Tables["Banks"].Rows[cboBankSelectedIndex][9] = Eramake.eCryptography.Encrypt(txtFileNo.Text);


                            dataSet2.WriteXml("Bank.xml");
                        }
                    }
                }

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Start startform = new Start();
            startform.Show();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Enabledexportbtn();
        }

        private void Enabledexportbtn()
        {
            btnExport.Enabled = false;
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
            {
                btnExport.Enabled = true;
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