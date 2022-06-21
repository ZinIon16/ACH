using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddBank : Form
    {
        string bankName;
        int banktype;
        public AddBank(string Bankname,int banktypeindex)
        {
            this.banktype = banktypeindex;
            this.bankName = Bankname;
            InitializeComponent();
        }
        DataSet dataSet2 = new DataSet();
        DataSet dataSet1 = new DataSet();
        int counter;
        private void AddBank_Load(object sender, EventArgs e)
        {
            dataSet1.ReadXml("Bank.xml");
            counter = dataSet1.Tables["Banks"].Rows.Count;
            MessageBox.Show("Click on Proceed button to go to next step");
            if (banktype == 0)
            {
                lblBankType.Text = "Bank Type is : RBC";
                lblDebit.Visible = false;
                lblCredit.Visible = false;
                txtDebit.Visible = false;
                txtCredit.Visible = false;
            }
            else
            {
                lblBankType.Text = "Bank Type is : BMO";
                lblOrID.Visible = false;
                lblHeader.Visible = false;
                txtOrID.Visible = false;
                txtHeader.Visible = false;

            }

            lblBank.Text = "Bank Name is : " + bankName;
            
        }

        private void btnAddNewBank_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
           
            //cboBankType.Items.Add("RBC");
            //cboBankType.Items.Add("BMO");
            dataSet2.ReadXml("Bank.xml");
            lblBank.Text = "Bank Name is : " + bankName;
            DialogResult result = MessageBox.Show("Are you sure you want to add a new bank?", "Add New Bank", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                DataRow x = dataSet2.Tables["Banks"].NewRow();
                dataSet2.Tables["Banks"].Rows.Add(x);
                //counter++;
            }
            if (banktype == 0)
            {
                //counter = counter - 1;
                dataSet2.Tables["Banks"].Rows[counter][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                dataSet2.Tables["Banks"].Rows[counter][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                dataSet2.Tables["Banks"].Rows[counter][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                dataSet2.Tables["Banks"].Rows[counter][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                dataSet2.Tables["Banks"].Rows[counter][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                dataSet2.Tables["Banks"].Rows[counter][5] = Eramake.eCryptography.Encrypt(txtOrID.Text);
                dataSet2.Tables["Banks"].Rows[counter][6] = Eramake.eCryptography.Encrypt(txtHeader.Text);
                dataSet2.Tables["Banks"].Rows[counter][9] = "";

                dataSet2.WriteXml("Bank.xml");
            }
            else if (banktype == 1)
            {
                //counter = counter - 1;
                dataSet2.Tables["Banks"].Rows[counter][0] = Eramake.eCryptography.Encrypt(txtCName.Text);
                dataSet2.Tables["Banks"].Rows[counter][1] = Eramake.eCryptography.Encrypt(txtCBNo.Text);
                dataSet2.Tables["Banks"].Rows[counter][2] = Eramake.eCryptography.Encrypt(txtCBrNo.Text);
                dataSet2.Tables["Banks"].Rows[counter][3] = Eramake.eCryptography.Encrypt(txtAcc.Text);
                dataSet2.Tables["Banks"].Rows[counter][4] = Eramake.eCryptography.Encrypt(txtlDesDataCenter.Text);
                dataSet2.Tables["Banks"].Rows[counter][7] = Eramake.eCryptography.Encrypt(txtCredit.Text);
                dataSet2.Tables["Banks"].Rows[counter][8] = Eramake.eCryptography.Encrypt(txtDebit.Text);
                dataSet2.Tables["Banks"].Rows[counter][9] = "";

                dataSet2.WriteXml("Bank.xml");
             
            }
            MessageBox.Show("Bank has been added successfully");
            this.Close();
        }
    }
}
