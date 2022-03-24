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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Web;
using System.Web.UI;
using System.Xml;

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
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = false
                                }
                            });
                            tableCollection = result.Tables;
                            cboSheet.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                cboSheet.Items.Add(table.TableName);
                            //String Sheet= cboSheet.Items.Add(table).toString();

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
            StreamWriter File = new StreamWriter("demo.txt");
            String[] NoOfRows = new string[5];
            int FalseRows = ((dataGridView1.Rows.Count) - 1);
            String Rows = (FalseRows.ToString());
            long TotalAmount = 0;
            String TotAmount = "";
            //String str = "Hello World";
            char[] ArrayRows = new char[5] { '0', '0', '0', '0', '0' };
            char[] ArrayTotalAmount = new char[10] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayAccNo = new char[20] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayBankID = new char[10] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayAmount = new char[10] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayEntID = new char[20] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayEntName = new char[20] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            char[] ArrayTCode = new char[5] { '0', '0', '0', '0', '0' };
            char[] ArrayDescription = new char[50] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' , '0', 
                '0', '0', '0', '0', '0', '0', '0', '0', '0' , '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' , 
                '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' , '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };

            //TOTAL ROWS
            int a = (Rows.Length)-1;

            for (int i = 4; i > (4-(Rows.Length)); i--)
            {
                ArrayRows[i] = Rows[a];
                a--;
            }

            //for (int i = 4; i > -1; i--)
            //{
            //    NoOfRows[i] = Rows;

            //}
            //TOTALAMOUNT
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                   
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Amount")
                    {
                        for (int x = i + 1; x < dataGridView1.Rows.Count - 1; x++)
                        {

                            TotalAmount = Convert.ToInt64(dataGridView1.Rows[x].Cells[j].Value.ToString()) + TotalAmount;

                        }
                    }
                }

                
            }
            string AccountNumber = "";
            string BankID = "";
            string Amount = "";
            string EntityID = "";
            string EntityName = "";
            string TransactionCode = "";
            string Description = "";

            //_______________________________________________________________________________________________________
            //File Write
            //TOTAL ROWS

            File.Write("H");
            for (int i = 0; i < 5; i++)
            {

                File.Write(ArrayRows[i]);

            }
            //TOTAL AMOUNT
            TotAmount = Convert.ToString(TotalAmount);


            int b = TotAmount.Length - 1;
            for (int i = 9; i > (9 - TotAmount.Length); i--)
            {
                ArrayTotalAmount[i] = TotAmount[b];
                b--;
            }

            for (int i = 0; i < 10; i++)
            {

                File.Write(ArrayTotalAmount[i]);

            }

            //R format
           
            //ACCOUNTNUMBER

            for (int k = 1; k < dataGridView1.Rows.Count - 1; k++)
            {
                File.WriteLine("");
                File.Write("R");
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Account Number")
                        {

                            AccountNumber = dataGridView1.Rows[k].Cells[j].Value.ToString();

                            int c = (AccountNumber.Length - 1);
                            for (int x = 19; x > (19 - AccountNumber.Length); x--)
                            {
                                ArrayAccNo[x] = AccountNumber[c];
                                c--;
                            }

                            for (int x = 0; x < 20; x++)
                            {

                                File.Write(ArrayAccNo[x]);

                            }

                        }

                    }
                }

                //File.WriteLine(AccountNumber);
                //BANKID
                //string BankID = "";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Bank ID")
                        {

                            BankID = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int d = BankID.Length - 1;

                            for (int x = 9; x > (9 - BankID.Length); x--)
                            {
                                ArrayBankID[x] = BankID[d];
                                d--;
                            }

                            for (int x = 0; x < 10; x++)
                            {

                                File.Write(ArrayBankID[x]);

                            }

                        }
                    }
                }

                //AMOUNT
                //string Amount = "";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Amount")
                        {

                            Amount = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int f = Amount.Length - 1;

                            for (int x = 9; x > (9 - Amount.Length); x--)
                            {
                                ArrayAmount[x] = Amount[f];
                                f--;
                            }

                            for (int x = 0; x < 10; x++)
                            {

                                File.Write(ArrayAmount[x]);

                            }
                        }
                    }
                }
                //ENTITYID
                //string EntityID = "";

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Entity ID")
                        {

                            EntityID = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int g = EntityID.Length - 1;

                            for (int x = 19; x > (19 - EntityID.Length); x--)
                            {
                                ArrayEntID[x] = EntityID[g];
                                g--;
                            }

                            for (int x = 0; x < 20; x++)
                            {

                                File.Write(ArrayEntID[x]);

                            }

                        }
                    }
                }
                //ENTITYNAME
                //string EntityName= "";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Entity Name")
                        {


                            EntityName = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int h = EntityName.Length - 1;

                            for (int x = 19; x > (19 - EntityName.Length); x--)
                            {
                                ArrayEntName[x] = EntityName[h];
                                h--;
                            }

                            for (int x = 0; x < 20; x++)
                            {

                                File.Write(ArrayEntName[x]);

                            }

                        }
                    }
                }
                //string TransactionCode = "";
                ////TRANSACTIONCODE
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Transaction Code")
                        {
                            TransactionCode = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int l = TransactionCode.Length - 1;

                            for (int x = 4; x > (4 - TransactionCode.Length); x--)
                            {
                                ArrayTCode[x] = TransactionCode[l];
                                l--;
                            }

                            for (int x = 0; x < 5; x++)
                            {

                                File.Write(ArrayTCode[x]);

                            }

                        }
                    }
                }
                //DESCRIPTION
                //string Description = "";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Description")
                        {


                            Description = (dataGridView1.Rows[k].Cells[j].Value.ToString());
                            int m = Description.Length - 1;

                            for (int x = 49; x > (49 - Description.Length); x--)
                            {
                                ArrayDescription[x] = Description[m];
                                m--;
                            }

                            for (int x = 0; x < 50; x++)
                            {

                                File.Write(ArrayDescription[x]);

                            }
                        }
                    }
                }
            }
            
            //AccountNumber
            //int c = AccountNumber.Length - 1;

            //for (int i = 19; i > (19 - AccountNumber.Length); i--)
            //{
            //    ArrayAccNo[i] = AccountNumber[c];
            //    c--;
            //}

            //for (int i = 0; i < 20; i++)
            //{

            //    File.Write(ArrayAccNo[i]);

            //}
            //BankID
            //int d = BankID.Length - 1;

            //for (int i = 9; i > (9 - BankID.Length); i--)
            //{
            //    ArrayBankID[i] = BankID[d];
            //    d--;
            //}

            //for (int i = 0; i < 10; i++)
            //{

            //    File.Write(ArrayBankID[i]);

            //}
            //Amount
            //int f = Amount.Length - 1;

            //for (int i = 9; i > (9 - Amount.Length); i--)
            //{
            //    ArrayAmount[i] = Amount[f];
            //    f--;
            //}

            //for (int i = 0; i < 10; i++)
            //{

            //    File.Write(ArrayAmount[i]);

            //}
            //EntityID
            //int g = EntityID.Length - 1;

            //for (int i = 19; i > (19 - EntityID.Length); i--)
            //{
            //    ArrayEntID[i] = EntityID[g];
            //    g--;
            //}

            //for (int i = 0; i < 20; i++)
            //{

            //    File.Write(ArrayEntID[i]);

            //}
            //EntityName
            //int h = EntityName.Length - 1;

            //for (int i = 19; i > (19 - EntityName.Length); i--)
            //{
            //    ArrayEntName[i] = EntityName[h];
            //    h--;
            //}

            //for (int i = 0; i < 20; i++)
            //{

            //    File.Write(ArrayEntName[i]);

            //}
            //TCODE
            //int l = TransactionCode.Length - 1;

            //for (int i = 4; i > (4 - TransactionCode.Length); i--)
            //{
            //    ArrayTCode[i] = TransactionCode[l];
            //    l--;
            //}

            //for (int i = 0; i < 5; i++)
            //{

            //    File.Write(ArrayTCode[i]);

            //}
            //DESCRIPTION
            //int m = Description.Length - 1;

            //for (int i = 49; i > (49 - Description.Length); i--)
            //{
            //    ArrayDescription[i] = Description[m];
            //    m--;
            //}

            //for (int i = 0; i < 50; i++)
            //{

            //    File.Write(ArrayDescription[i]);

            //}
            //File.Write(/*dataGridView1.Rows[x].Cells[j].Value.ToString()*/ TotalAmount);
            File.Close();
        }
    }
}
