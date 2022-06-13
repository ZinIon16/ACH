using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;

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
            cboSheet.Enabled = false;
            cboOrID.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(dt,cboBank.SelectedIndex,cboOrID.SelectedIndex, cboBank.Text,cboOrID.Text, textBox1.Text);
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
        DataTable dt;

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tableCollection[cboSheet.SelectedItem.ToString()];

            GridView.DataSource = dt;

            bool error = false;

            //Validation Check for Transaction Code
            for (int i = 0; i < ((dt.Rows.Count)-1 ); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TranCode")
                    {
                        for (int x = i + 1; x < ((dt.Rows.Count) ); x++)
                        {
                            if (GridView.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (GridView.Rows[x].Cells[j].Value.ToString() != "TranCode")
                            {
                                if (GridView.Rows[x].Cells[j].Value.ToString() != TransactionCode)
                                {
                                    error = true;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                    GridView.Rows[x].Cells[j].Value = GridView.Rows[x].Cells[j].Value.ToString();
                                }
                            }
                        }
                    }
                }
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
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "Amount" || dt.Rows[i][j].ToString() == "Account")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (GridView.Rows[x].Cells[j].Value.ToString().Contains(" ") == true)
                            {
                                error = true;
                                GridView.Rows[x].Cells[j].Style.ForeColor = Color.Red;
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
                    MessageBox.Show("Click on Next to proceed!", "Click on OK");
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
                            if (GridView.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (GridView.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                            {
                                if (GridView.Rows[x].Cells[j].Value.ToString().Length != 9)
                                {
                                    error = true;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                    GridView.Rows[x].Cells[j].Value = GridView.Rows[x].Cells[j].Value.ToString();
                                }
                            }
                        }
                    }
                }
            }
            if (error == true)
            {
                MessageBox.Show("The Transit Code must be of 9 digits, change it and then click on" + " Next Button" + " in order to proceed!", " Transit Code Error");
                btnNext.Enabled = false;
            }



        }
        string TransactionCode = "";
        
        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboOrID.Enabled = true;
        }

        private void cboOrID_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TranCode")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (GridView.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (GridView.Rows[x].Cells[j].Value.ToString() != "TranCode")
                            {
                                if ((GridView.Rows[x].Cells[j].Value.ToString() != TransactionCode) || GridView.Rows[x].Cells[j].Value.ToString().Contains("*"))
                                {
                                    error = true;
                                    btnNext.Enabled = false;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                }
                                else
                                {
                                    
                                    check++;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
            if (error == true)
            {
                MessageBox.Show("The Transaction Code must match with the File type, change it and then click on" + "Update Button" + " in order to proceed!", " Transit Code Error");
            }
            if (dt.Rows.Count-1 == check)
            {
                btnNext.Enabled = true;
                MessageBox.Show("The Transaction Code has been updated successfully!", " Success");
            }

            //Validation check for TransitCode after changes
            
            error = false;
            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                for (int j = 0; j < (dt.Columns.Count); j++)
                {
                    if (dt.Rows[i][j].ToString() == "TransitCode")
                    {
                        for (int x = i + 1; x < dt.Rows.Count; x++)
                        {
                            if (GridView.Rows[x].Cells[j].Value == null)
                            {
                                break;
                            }
                            if (GridView.Rows[x].Cells[j].Value.ToString() != "TransitCode")
                            {
                                if ((GridView.Rows[x].Cells[j].Value.ToString().Length != 9) || GridView.Rows[x].Cells[j].Value.ToString().Contains("*"))
                                {
                                    error = true;
                                    btnNext.Enabled = false;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Red;
                                }
                                else
                                {
                                    check++;
                                    GridView.Rows[x].Cells[j].Style.ForeColor = Color.Black;
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
                btnNext.Enabled = true;
                MessageBox.Show("The Transit Code has been updated successfully!", " Success");
            }
        }
    }
}
