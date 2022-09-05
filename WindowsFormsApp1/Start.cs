// <copyright file="Start.cs" company="">
//    
// </copyright>
// <author></author>
// <copyright file="Start.cs" company="">
//    
// </copyright>
// <author></author>
using ExcelDataReader;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            cboBankType.Items.Add("RBC");
            cboBankType.Items.Add("BMO");
            lblBankType.Visible = false;
            cboBankType.Visible = false;
            cboSheet.Enabled = false;
            cboOrID.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(dt, cboBank.SelectedIndex, cboOrID.SelectedIndex, cboBank.Text, cboOrID.Text, textBox1.Text/*, cboBankType.SelectedIndex*/);
            form1.Show();
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
                            DataSet dataSet2 = new DataSet();

                            if (File.Exists("Bank.xml"))
                            {
                                dataSet2.ReadXml("Bank.xml");

                                for (int x = 0; x < dataSet2.Tables["Banks"].Rows.Count; x++)
                                {
                                    cboBank.Items.Add(Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[x][0].ToString()));
                                    if ((Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[x][0].ToString()).Contains("MB ENTERPRISES I")) == true)
                                    {
                                        cboBank.Items.Remove("MB ENTERPRISES I");
                                        cboBank.Items.Insert(x, "MB ENTERPRISES RBC");
                                    }
                                    if ((Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[x][0].ToString()).Contains("2570993 ONT INC")) == true)
                                    {
                                        cboBank.Items.Remove("2570993 ONT INC");
                                        cboBank.Items.Insert(x, "2570993 ONT INC SENATORS HOTEL DEBIT FILE");
                                    }
                                    if ((Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[x][0].ToString()).Contains("2570993 ONTARIO")) == true)
                                    {
                                        cboBank.Items.Remove("2570993 ONTARIO");
                                        cboBank.Items.Insert(x, "2570993 ONTARIO INC THE SENATORS HOTEL");
                                    }
                                }
                            }
                            else
                            {
                                //File.Create("Bank.xml");
                                MessageBox.Show("File doesn't exists, Add a bank first");
                            }
                            //cboBank.Items.Remove(Eramake.eCryptography.Decrypt(dataSet2.Tables["Banks"].Rows[11][0].ToString()));

                            cboOrID.Items.Clear();
                            cboOrID.Items.Add("Credit");
                            cboOrID.Items.Add("Debit");
                            cboOrID.Text = "Credit/Debit";
                        }
                    }
                }
            }
        }

        private DataTable dt;

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tableCollection[cboSheet.SelectedItem.ToString()];

            GridView.DataSource = dt;

            bool error = false;
            int[] Array = { 0, 1, 4 };
            //Validation for EntityID, Account,Amount and EntityName
            for (int j = 0; j < Array.Length; j++)
            {
                for (int i = 1; i < (dt.Rows.Count); i++)
                {
                    //for (int j = 0; j < (dt.Columns.Count); j++)
                    //{
                    if (!Regex.Match(dt.Rows[i][Array[j]].ToString(), "^[a-zA-Z0-9\\s]+$").Success)
                    {
                        error = true;
                        //btnNext.Enabled = false;
                        GridView.Rows[i].Cells[Array[j]].Style.ForeColor = Color.Red;
                    }
                    //if (!Regex.Match(dt.Rows[i][1].ToString(), "^[a-zA-Z0-9\\s]+$").Success)
                    //{
                    //    error = true;
                    //    btnNext.Enabled = false;
                    //    GridView.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    //}
                    //}
                }
            }
            if (error == true)
            {
                MessageBox.Show("The EntityID/EntityName might have special character(s), change it and then click on" + " Update Button" + " in order to proceed!", " EntityID/Name Error");
                btnNext.Enabled = false;
            }
            error = false;
            //Validation Check for Transaction Code
            for (int i = 0; i < ((dt.Rows.Count) - 1); i++)
            {
                //for (int j = 0; j < (dt.Columns.Count); j++)
                //{
                //if (dt.Rows[i][j].ToString() == "TranCode")
                //{
                for (int x = i + 1; x < ((dt.Rows.Count)); x++)
                {
                    if (GridView.Rows[x].Cells[5].Value == null)
                    {
                        break;
                    }
                    //if (GridView.Rows[x].Cells[j].Value.ToString() != "TranCode")
                    //{
                    if (GridView.Rows[x].Cells[5].Value.ToString() != TransactionCode)
                    {
                        error = true;
                        GridView.Rows[x].Cells[5].Style.ForeColor = Color.Red;
                        GridView.Rows[x].Cells[5].Value = GridView.Rows[x].Cells[5].Value.ToString();
                    }
                    //}
                }
                //}
                //}
            }
            if (error == true)
            {
                MessageBox.Show("The Transaction Code must match with the File type, change it and then click on" + " Update Button" + " in order to proceed!", " Transaction Code Error");
                btnNext.Enabled = false;
            }

            error = false;

            //Validation Check for Amount
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                //for (int j = 0; j < (dt.Columns.Count); j++)
                //{
                //if (dt.Rows[i][j].ToString() == "Amount" || dt.Rows[i][j].ToString() == "Account")
                //{
                for (int x = i + 1; x < dt.Rows.Count; x++)
                {
                    if (GridView.Rows[x].Cells[2].Value.ToString().Contains(" ") == true || GridView.Rows[x].Cells[4].Value.ToString().Contains(" ") == true)
                    {
                        error = true;
                        GridView.Rows[x].Cells[2].Style.ForeColor = Color.Red;
                    }
                    if (GridView.Rows[x].Cells[4].Value.ToString().Contains(" ") == true)
                    {
                        error = true;
                        GridView.Rows[x].Cells[4].Style.ForeColor = Color.Red;
                    }
                }
                //}
                //}
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
                    MessageBox.Show("Click on Next to proceed!", "Click on OK");
                }
            }

            //Validation check for TransitCode
            error = false;
            for (int i = 0; i < ((dt.Rows.Count) - 1); i++)
            {
                //for (int j = 0; j < (dt.Columns.Count); j++)
                //{
                //if (dt.Rows[i][j].ToString() == "TransitCode")
                //{
                for (int x = i + 1; x < ((dt.Rows.Count) - 1); x++)
                {
                    if (GridView.Rows[x].Cells[3].Value == null)
                    {
                        break;
                    }
                    //if (GridView.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                    //{
                    if (GridView.Rows[x].Cells[3].Value.ToString().Length != 9)
                    {
                        error = true;
                        GridView.Rows[x].Cells[3].Style.ForeColor = Color.Red;
                        GridView.Rows[x].Cells[3].Value = GridView.Rows[x].Cells[3].Value.ToString();
                    }
                    //}
                }
                //}
                //}
            }
            if (error == true)
            {
                MessageBox.Show("The Transit Code must be of 9 digits, change it and then click on" + " Next Button" + " in order to proceed!", " Transit Code Error");
                btnNext.Enabled = false;
            }
        }

        private string TransactionCode = "";

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboOrID.Enabled = true;
        }

        private void cboOrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dt = tableCollection[cboSheet.SelectedItem.ToString()];

            //GridView.DataSource = dt;

            //cboSheet.Items.Clear();

            GridView.DataSource = null;

            cboSheet.Enabled = true;
            if (cboOrID.SelectedIndex == 0)
            {
                TransactionCode = "450";
            }
            else if (cboOrID.SelectedIndex == 1)
            {
                TransactionCode = "470";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            //Validation check for TransactionCode after changes
            int check = 0;
            bool error;
            check = 0;
            error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                //for (int j = 0; j < (dt.Columns.Count); j++)
                //{
                //if (dt.Rows[i][j].ToString() == "TranCode")
                //{
                for (int x = i + 1; x < dt.Rows.Count; x++)
                {
                    if (GridView.Rows[x].Cells[5].Value == null)
                    {
                        break;
                    }
                    //if (GridView.Rows[x].Cells[j].Value.ToString() != "TranCode")
                    //{
                    if ((GridView.Rows[x].Cells[5].Value.ToString() != TransactionCode) || GridView.Rows[x].Cells[5].Value.ToString().Contains("*"))
                    {
                        error = true;
                        btnNext.Enabled = false;
                        GridView.Rows[x].Cells[5].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        check++;
                        GridView.Rows[x].Cells[5].Style.ForeColor = Color.Black;
                    }
                    //}
                    //}
                    //}
                }
            }
            //if (error == true)
            //{
            //    MessageBox.Show("The Transaction Code must match with the File type, change it and then click on" + "Update Button" + " in order to proceed!", " Transit Code Error");
            //}
            //if (dt.Rows.Count - 1 == check)
            //{
            //    btnNext.Enabled = true;
            //    MessageBox.Show("The Transaction Code has been updated successfully!", " Success");
            //}

            //Validation check for TransitCode after changes
            check = 0;
            //error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                //for (int j = 0; j < (dt.Columns.Count); j++)
                //{
                //if (dt.Rows[i][j].ToString() == "TransitCode")
                //{
                for (int x = i + 1; x < dt.Rows.Count; x++)
                {
                    if (GridView.Rows[x].Cells[3].Value == null)
                    {
                        break;
                    }
                    //if (GridView.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                    //{
                    if ((GridView.Rows[x].Cells[3].Value.ToString().Length != 9) || GridView.Rows[x].Cells[3].Value.ToString().Contains("*"))
                    {
                        error = true;
                        btnNext.Enabled = false;
                        GridView.Rows[x].Cells[3].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        check++;
                        GridView.Rows[x].Cells[3].Style.ForeColor = Color.Black;
                    }
                    //}
                }
                //}
                //}
            }
            //if (error == true)
            //{
            //    MessageBox.Show("The Transit Code must be of 9 digits, change it and then click on" + "Update Button" + " in order to proceed!", " Transit Code Error");
            //}

            //if (dt.Rows.Count - 1 == check)
            //{
            //    btnNext.Enabled = true;
            //    MessageBox.Show("The Transit Code has been updated successfully!", " Success");
            //}
            check = 0;
            //error = false;
            //Validation for EntityID and EntityName
            int[] Array = { 0, 1, 4 };
            for (int j = 0; j < Array.Length; j++)
            {
                btnNext.Enabled = true;
                for (int i = 0; i < (dt.Rows.Count); i++)
                {
                    //for (int j = 0; j < (dt.Columns.Count); j++)
                    //{
                    if (!Regex.Match(dt.Rows[i][Array[j]].ToString(), "^[a-zA-Z0-9\\s]+$").Success)
                    {
                        error = true;
                        //btnNext.Enabled = false;
                        GridView.Rows[i].Cells[Array[j]].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        check = 0;
                        check++;
                        GridView.Rows[i].Cells[Array[j]].Style.ForeColor = Color.Black;
                    }

                    //}
                }
            }
            if (error == true)
            {
                MessageBox.Show("There are Error(s), fix it and then click on" + " Update Button" + " in order to proceed!", " EntityID/Name Error");
            }
            else
            {
                btnNext.Enabled = true;
                MessageBox.Show("The field(s) has been updated successfully!", " Success");
            }
            //if (dt.Rows.Count - 1 == check)
            //{
            //    btnNext.Enabled = true;
            //    MessageBox.Show("The field(s) has been updated successfully!", " Success");
            //}
        }

        private string NewBankName;

        private void btnAddBank_Click(object sender, EventArgs e)
        {
            NewBankName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the bank", "New Bank", "", 240, 160);
            //string  = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the bank", "New Bank", "", 240, 160);
            if (NewBankName != "")
            {
                MessageBox.Show("Please select the bank type: RBC/BMO");
                lblBankType.Visible = true;
                cboBankType.Visible = true;
                lblBankType.ForeColor = Color.Red;
                //AddBank addBank = new AddBank(NewBankName, cboBankType.SelectedIndex);
                //addBank.Show();
            }
        }

        private void cboBankType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddBank addBank = new AddBank(NewBankName, cboBankType.SelectedIndex);
            addBank.Show();
        }
    }
}