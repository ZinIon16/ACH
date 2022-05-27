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

        private bool ExportBtnClicked = false;
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
                            cboBank.Items.Add("MBE POS INC");
                            cboBank.Items.Add("MB ENTERPIRSES RBC");
                            cboBank.Items.Add("MB ENTERPIRSES");
                            cboBank.Items.Add("2570993 ONT INC DEBIT EFT");
                            cboBank.Items.Add("2570993 ONTARIO INC OR THE SENATORS HOTEL");
                            cboBank.Items.Add("GLOBAL PROCESSING CENTRE");
                            cboBank.Items.Add("GREAT POS");
                            cboBank.Items.Add("MANSOOR BROTHER ENT 744");
                            cboBank.Items.Add("MBBP");
                            cboBank.Items.Add("MBE US ACCOUNT");
                            cboBank.Items.Add("M-RIDES");
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
            lblTotAmount.Visible = true;
            TotAmount.Visible = true;
            //Getting the excel sheet data inside the data table so that we can display it in grid view
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];

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
            for (int i = 0; i < ((dt.Rows.Count) - 1); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TransitCode")
                    {
                        for (int x = i + 1; x < ((dt.Rows.Count) - 1); x++)
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
            dataTable.Columns.Add("Header");
            dataTable.Columns.Add("FileNumber");
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
            DataSet dataSet2 = new DataSet();
            if (File.Exists("Bank.xml"))
            {
                dataSet2.ReadXml("Bank.xml");
                if (dataSet2.Tables["Banks"].Rows.Count <= 1)
                {
                    //MB POS
                    row1["CompanyName"] = "";
                    row1["CompanyBankNumber"] = "";
                    row1["CompanyBranchNumber"] = "";
                    row1["CompanyAccountNumber"] = "";
                    row1["DestinationDataCenter"] = "";
                    row1["Credit"] = "";
                    row1["Debit"] = "";
                    row1["FileNumber"] = "";

                    //RBC
                    row2["CompanyName"] = "";
                    row2["CompanyBankNumber"] = "";
                    row2["CompanyBranchNumber"] = "";
                    row2["CompanyAccountNumber"] = "";
                    row2["DestinationDataCenter"] = "";
                    row2["OriginatorID"] = "";
                    row2["Header"] = "";
                    row2["FileNumber"] = "";

                    //MB ENT
                    row3["CompanyName"] = "";
                    row3["CompanyBankNumber"] = "";
                    row3["CompanyBranchNumber"] = "";
                    row3["CompanyAccountNumber"] = "";
                    row3["DestinationDataCenter"] = "";
                    row3["Credit"] = "";
                    row3["Debit"] = "";
                    row3["FileNumber"] = "";

                    //2570993 ONT INC
                    row4["CompanyName"] = "";
                    row4["CompanyBankNumber"] = "";
                    row4["CompanyBranchNumber"] = "";
                    row4["CompanyAccountNumber"] = "";
                    row4["DestinationDataCenter"] = "";
                    row4["Credit"] = "";
                    row4["Debit"] = "";
                    row4["FileNumber"] = "";

                    //2570993 ONTARIO
                    row5["CompanyName"] = "";
                    row5["CompanyBankNumber"] = "";
                    row5["CompanyBranchNumber"] = "";
                    row5["CompanyAccountNumber"] = "";
                    row5["DestinationDataCenter"] = "";
                    row5["Credit"] = "";
                    row5["Debit"] = "";
                    row5["FileNumber"] = "";

                    //GLOBAL PROCESSING CENTRE
                    row6["CompanyName"] = "";
                    row6["CompanyBankNumber"] = "";
                    row6["CompanyBranchNumber"] = "";
                    row6["CompanyAccountNumber"] = "";
                    row6["DestinationDataCenter"] = "";
                    row6["OriginatorID"] = "";
                    row6["Header"] = "";
                    row6["FileNumber"] = "";

                    //GREAT POS
                    row7["CompanyName"] = "";
                    row7["CompanyBankNumber"] = "";
                    row7["CompanyBranchNumber"] = "";
                    row7["CompanyAccountNumber"] = "";
                    row7["DestinationDataCenter"] = "";
                    row7["OriginatorID"] = "";
                    row7["Header"] = "";
                    row7["FileNumber"] = "";

                    //MB ENTERPRISES 7
                    row8["CompanyName"] = "";
                    row8["CompanyBankNumber"] = "";
                    row8["CompanyBranchNumber"] = "";
                    row8["CompanyAccountNumber"] = "";
                    row8["DestinationDataCenter"] = "";
                    row8["Credit"] = "";
                    row8["Debit"] = "";
                    row8["FileNumber"] = "";

                    //MBBP
                    row9["CompanyName"] = "";
                    row9["CompanyBankNumber"] = "";
                    row9["CompanyBranchNumber"] = "";
                    row9["CompanyAccountNumber"] = "";
                    row9["DestinationDataCenter"] = "";
                    row9["Credit"] = "";
                    row9["Debit"] = "";
                    row9["FileNumber"] = "";

                    //MBE USA ACCOUNT
                    row10["CompanyName"] = "";
                    row10["CompanyBankNumber"] = "";
                    row10["CompanyBranchNumber"] = "";
                    row10["CompanyAccountNumber"] = "";
                    row10["DestinationDataCenter"] = "";
                    row10["Credit"] = "";
                    row10["Debit"] = "";
                    row10["FileNumber"] = "";

                    //M-RIDES
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

                row2["CompanyName"] = "";
                row2["CompanyBankNumber"] = "";
                row2["CompanyBranchNumber"] = "";
                row2["CompanyAccountNumber"] = "";
                row2["DestinationDataCenter"] = "";
                row2["OriginatorID"] = "";
                row2["Header"] = "";
                row2["FileNumber"] = "";

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
                row6["OriginatorID"] = "";
                row6["Header"] = "";
                row6["FileNumber"] = "";

                row7["CompanyName"] = "";
                row7["CompanyBankNumber"] = "";
                row7["CompanyBranchNumber"] = "";
                row7["CompanyAccountNumber"] = "";
                row7["DestinationDataCenter"] = "";
                row7["OriginatorID"] = "";
                row7["Header"] = "";
                row7["FileNumber"] = "";

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
        }

        private long FileNumber = 0;

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboOrID.Text = "Credit/Debit";
            txtOrID.Clear();
            if (cboBank.SelectedIndex == 0)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[0][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[0][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[0][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 1)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][4].ToString());
                txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][5].ToString());
                txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[1][6].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[1][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[1][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[1][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 2)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[2][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[2][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[2][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 3)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[3][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[3][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[3][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 4)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[4][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[4][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[4][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 5)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][4].ToString());
                txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][5].ToString());
                txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[5][6].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[5][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[5][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[5][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }
            else if (cboBank.SelectedIndex == 6)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][4].ToString());
                txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][5].ToString());
                txtHeader.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[6][6].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[6][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[6][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[6][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }

            if (cboBank.SelectedIndex == 7)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[7][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[7][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[7][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }

            if (cboBank.SelectedIndex == 8)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[8][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[8][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[8][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }

            if (cboBank.SelectedIndex == 9)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[9][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[9][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[9][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }

            if (cboBank.SelectedIndex == 10)
            {
                DataSet dataSet2 = new DataSet();
                dataSet2.ReadXml("Bank.xml");
                txtCName.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][0].ToString());
                txtCBNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][1].ToString());
                txtCBrNo.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][2].ToString());
                txtAcc.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][3].ToString());
                txtlDesDataCenter.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][4].ToString());
                if (Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[10][9]).ToString()) == "")
                {
                    txtFileNo.Text = Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[10][9]).ToString());
                }
                else
                {
                    FileNumber = Convert.ToInt32(Eramake.eCryptography.Decrypt((dataSet2.Tables["Banks"].Rows[10][9]).ToString()));
                    FileNumber = FileNumber + 1;
                    txtFileNo.Text = FileNumber.ToString();
                }
            }

            //condition to check the bank and display originator id accordingly

            if (cboBank.SelectedIndex == 0 || cboBank.SelectedIndex == 2 || cboBank.SelectedIndex == 3 || cboBank.SelectedIndex == 4 || cboBank.SelectedIndex == 7 || cboBank.SelectedIndex == 8 || cboBank.SelectedIndex == 9 || cboBank.SelectedIndex == 10)
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

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewCell item in this.dataGridView1.SelectedCells)
            {
                dataGridView1.Rows.RemoveAt(item.RowIndex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportBtnClicked = true;
            BankFile objBank = null;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.AcceptChanges();

            if (cboBank.SelectedIndex == 0 || cboBank.SelectedIndex == 2 || cboBank.SelectedIndex == 3 || cboBank.SelectedIndex == 4 || cboBank.SelectedIndex == 7 || cboBank.SelectedIndex == 8 || cboBank.SelectedIndex == 9 || cboBank.SelectedIndex == 10)
            {
                objBank = new BMO();
            }
            else if (cboBank.SelectedIndex == 1 || cboBank.SelectedIndex == 5 || cboBank.SelectedIndex == 6)
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

            if (cboOrID.SelectedIndex == 0)
            {
                objBank.FileType = "C";
            }
            else if (cboOrID.SelectedIndex == 1)
            {
                objBank.FileType = "D";
            }

            objBank.FileName = textBox1.Text;

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
            dataTable.Columns.Add("Header");
            dataTable.Columns.Add("FileNumber");
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
            DataSet dataSet2 = new DataSet();

            dataSet2.ReadXml("Bank.xml");

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
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row1["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row1["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row1["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row1["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row1["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();
                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 1)
            {
                row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                row2["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                row2["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                row2["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                row2["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                row2["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                row2["OriginatorID"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                row2["Header"] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                row2["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                dataSet.WriteXml("Bank.xml");
            }
            else if (cboBank.SelectedIndex == 2)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row3["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row3["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row3["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row3["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row3["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row3["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row3["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row3["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row3["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row3["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 3)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row4["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row4["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row4["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row4["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row4["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row4["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row4["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row4["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row4["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row4["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 4)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row5["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row5["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row5["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row5["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row5["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row5["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row5["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row5["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row5["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row5["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 5)
            {
                row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                row6["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                row6["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                row6["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                row6["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                row6["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                row6["OriginatorID"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                row6["Header"] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                row6["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                dataSet.WriteXml("Bank.xml");
            }
            else if (cboBank.SelectedIndex == 6)
            {
                row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                row7["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                row7["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                row7["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                row7["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                row7["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                row7["OriginatorID"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                row7["Header"] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                row7["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                dataSet.WriteXml("Bank.xml");
            }
            else if (cboBank.SelectedIndex == 7)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row8["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row8["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row8["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row8["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row8["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row8["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row8["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row8["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row8["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row8["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 8)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row9["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row9["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row9["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row9["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row9["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row9["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row9["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row9["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row9["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row9["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 9)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row10["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row10["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row10["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row10["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row10["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row10["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row10["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row10["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row10["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row10["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    row11["CompanyName"] = dataSet2.Tables["Banks"].Rows[10][0].ToString();
                    row11["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[10][1].ToString();
                    row11["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[10][2].ToString();
                    row11["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[10][3].ToString();
                    row11["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[10][4].ToString();
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = dataSet2.Tables["Banks"].Rows[10][9].ToString();

                    dataSet.WriteXml("Bank.xml");
                }
            }
            else if (cboBank.SelectedIndex == 10)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row11["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row11["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row11["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row11["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row11["Credit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row11["Debit"] = dataSet2.Tables["Banks"].Rows[10][8].ToString();
                    row11["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    dataSet.WriteXml("Bank.xml");
                }
                else
                {
                    row1["CompanyName"] = dataSet2.Tables["Banks"].Rows[0][0].ToString();
                    row1["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[0][1].ToString();
                    row1["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[0][2].ToString();
                    row1["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[0][3].ToString();
                    row1["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[0][4].ToString();
                    row1["Credit"] = dataSet2.Tables["Banks"].Rows[0][7].ToString();
                    row1["Debit"] = dataSet2.Tables["Banks"].Rows[0][8].ToString();
                    row1["FileNumber"] = dataSet2.Tables["Banks"].Rows[0][9].ToString();

                    row2["CompanyName"] = dataSet2.Tables["Banks"].Rows[1][0].ToString();
                    row2["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[1][1].ToString();
                    row2["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[1][2].ToString();
                    row2["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[1][3].ToString();
                    row2["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[1][4].ToString();
                    row2["OriginatorID"] = dataSet2.Tables["Banks"].Rows[1][5].ToString();
                    row2["Header"] = dataSet2.Tables["Banks"].Rows[1][6].ToString();
                    row2["FileNumber"] = dataSet2.Tables["Banks"].Rows[1][9].ToString();

                    row3["CompanyName"] = dataSet2.Tables["Banks"].Rows[2][0].ToString();
                    row3["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[2][1].ToString();
                    row3["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[2][2].ToString();
                    row3["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[2][3].ToString();
                    row3["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[2][4].ToString();
                    row3["Credit"] = dataSet2.Tables["Banks"].Rows[2][7].ToString();
                    row3["Debit"] = dataSet2.Tables["Banks"].Rows[2][8].ToString();
                    row3["FileNumber"] = dataSet2.Tables["Banks"].Rows[2][9].ToString();

                    row4["CompanyName"] = dataSet2.Tables["Banks"].Rows[3][0].ToString();
                    row4["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[3][1].ToString();
                    row4["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[3][2].ToString();
                    row4["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[3][3].ToString();
                    row4["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[3][4].ToString();
                    row4["Credit"] = dataSet2.Tables["Banks"].Rows[3][7].ToString();
                    row4["Debit"] = dataSet2.Tables["Banks"].Rows[3][8].ToString();
                    row4["FileNumber"] = dataSet2.Tables["Banks"].Rows[3][9].ToString();

                    row5["CompanyName"] = dataSet2.Tables["Banks"].Rows[4][0].ToString();
                    row5["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[4][1].ToString();
                    row5["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[4][2].ToString();
                    row5["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[4][3].ToString();
                    row5["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[4][4].ToString();
                    row5["Credit"] = dataSet2.Tables["Banks"].Rows[4][7].ToString();
                    row5["Debit"] = dataSet2.Tables["Banks"].Rows[4][8].ToString();
                    row5["FileNumber"] = dataSet2.Tables["Banks"].Rows[4][9].ToString();

                    row6["CompanyName"] = dataSet2.Tables["Banks"].Rows[5][0].ToString();
                    row6["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[5][1].ToString();
                    row6["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[5][2].ToString();
                    row6["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[5][3].ToString();
                    row6["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[5][4].ToString();
                    row6["OriginatorID"] = dataSet2.Tables["Banks"].Rows[5][5].ToString();
                    row6["Header"] = dataSet2.Tables["Banks"].Rows[5][6].ToString();
                    row6["FileNumber"] = dataSet2.Tables["Banks"].Rows[5][9].ToString();

                    row7["CompanyName"] = dataSet2.Tables["Banks"].Rows[6][0].ToString();
                    row7["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[6][1].ToString();
                    row7["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[6][2].ToString();
                    row7["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[6][3].ToString();
                    row7["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[6][4].ToString();
                    row7["OriginatorID"] = dataSet2.Tables["Banks"].Rows[6][5].ToString();
                    row7["Header"] = dataSet2.Tables["Banks"].Rows[6][6].ToString();
                    row7["FileNumber"] = dataSet2.Tables["Banks"].Rows[6][9].ToString();

                    row8["CompanyName"] = dataSet2.Tables["Banks"].Rows[7][0].ToString();
                    row8["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[7][1].ToString();
                    row8["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[7][2].ToString();
                    row8["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[7][3].ToString();
                    row8["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[7][4].ToString();
                    row8["Credit"] = dataSet2.Tables["Banks"].Rows[7][7].ToString();
                    row8["Debit"] = dataSet2.Tables["Banks"].Rows[7][8].ToString();
                    row8["FileNumber"] = dataSet2.Tables["Banks"].Rows[7][9].ToString();

                    row9["CompanyName"] = dataSet2.Tables["Banks"].Rows[8][0].ToString();
                    row9["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[8][1].ToString();
                    row9["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[8][2].ToString();
                    row9["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[8][3].ToString();
                    row9["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[8][4].ToString();
                    row9["Credit"] = dataSet2.Tables["Banks"].Rows[8][7].ToString();
                    row9["Debit"] = dataSet2.Tables["Banks"].Rows[8][8].ToString();
                    row9["FileNumber"] = dataSet2.Tables["Banks"].Rows[8][9].ToString();

                    row10["CompanyName"] = dataSet2.Tables["Banks"].Rows[9][0].ToString();
                    row10["CompanyBankNumber"] = dataSet2.Tables["Banks"].Rows[9][1].ToString();
                    row10["CompanyBranchNumber"] = dataSet2.Tables["Banks"].Rows[9][2].ToString();
                    row10["CompanyAccountNumber"] = dataSet2.Tables["Banks"].Rows[9][3].ToString();
                    row10["DestinationDataCenter"] = dataSet2.Tables["Banks"].Rows[9][4].ToString();
                    row10["Credit"] = dataSet2.Tables["Banks"].Rows[9][7].ToString();
                    row10["Debit"] = dataSet2.Tables["Banks"].Rows[9][8].ToString();
                    row10["FileNumber"] = dataSet2.Tables["Banks"].Rows[9][9].ToString();

                    row11["CompanyName"] = Eramake.eCryptography.Encrypt(txtCName.Text);
                    row11["CompanyBankNumber"] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                    row11["CompanyBranchNumber"] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                    row11["CompanyAccountNumber"] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                    row11["DestinationDataCenter"] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                    row11["Credit"] = dataSet2.Tables["Banks"].Rows[10][7].ToString();
                    row11["Debit"] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                    row11["FileNumber"] = Eramake.eCryptography.Encrypt(txtFileNo.Text);

                    dataSet.WriteXml("Bank.xml");
                }
            }

            if (ExportBtnClicked == true)
            {
                MessageBox.Show("Text file has been created", " Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            if (cboBank.SelectedIndex == 0)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[0][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 2)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[2][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 3)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[3][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 4)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[4][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 7)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[7][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 8)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[8][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 9)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[9][8].ToString());
                }
            }
            else if (cboBank.SelectedIndex == 10)
            {
                if (cboOrID.SelectedIndex == 0)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][7].ToString());
                }
                else if (cboOrID.SelectedIndex == 1)
                {
                    DataSet dataSet2 = new DataSet();
                    dataSet2.ReadXml("Bank.xml");
                    txtOrID.Text = Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[10][8].ToString());
                }
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