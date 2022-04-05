using System;
using System.Data;
using System.IO;

namespace WindowsFormsApp1
{
    internal class RBC : BankFile
    {
        public override void Export(DataTable dt)
        {
            StreamWriter File = new StreamWriter("demo.txt");
            String[] NoOfRows = new string[5];
            int FalseRows = ((dt.Rows.Count) - 1);
            String Rows = (FalseRows.ToString());
            long TotalAmount = 0;
            String TotAmount = "";

            //TOTALAMOUNT
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "Amount")
                    {
                        for (int x = i + 1; x < dt.Rows.Count - 1; x++)
                        {
                            TotalAmount = Convert.ToInt64(dt.Rows[x][j].ToString()) + TotalAmount;
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

            File.Write("H" + base.Header);
            Rows = "00000" + Rows;
            Rows = Rows.Substring(Rows.Length - 5);
            File.Write(Rows);
            //TOTAL AMOUNT
            TotAmount = Convert.ToString(TotalAmount);
            TotAmount = "0000000000" + TotAmount;
            TotAmount = TotAmount.Substring(TotAmount.Length - 10);
            File.Write(TotAmount);

            for (int k = 1; k < dt.Rows.Count; k++)
            {
                AccountNumber = "";
                BankID = "";
                Amount = "";
                EntityID = "";
                EntityName = "";
                TransactionCode = "";
                Description = "";

                if (k % 2 == 0)
                {
                    File.Write("   ");
                }
                else
                {
                    File.WriteLine("");
                    File.Write("R");
                }

                for (int l = 1; l < 2; l++)
                {
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Account Number")
                            {
                                AccountNumber = dt.Rows[k][j].ToString();

                                AccountNumber = "00000000000000000000" + AccountNumber;
                                AccountNumber = AccountNumber.Substring(AccountNumber.Length - 20);
                                File.Write(AccountNumber);
                            }
                        }
                    }

                    //BANKID
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Bank ID")
                            {
                                BankID = (dt.Rows[k][j].ToString());
                                BankID = "0000000000" + BankID;
                                BankID = BankID.Substring(BankID.Length - 10);
                                File.Write(BankID);
                            }
                        }
                    }

                    //AMOUNT
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Amount")
                            {
                                Amount = (dt.Rows[k][j].ToString());
                                Amount = "0000000000" + Amount;
                                Amount = Amount.Substring(Amount.Length - 10);
                                File.Write(Amount);
                            }
                        }
                    }
                    //ENTITYID
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Entity ID")
                            {
                                EntityID = (dt.Rows[k][j].ToString());
                                EntityID = "00000000000000000000" + EntityID;
                                EntityID = EntityID.Substring(EntityID.Length - 20);
                                File.Write(EntityID);
                            }
                        }
                    }
                    //ENTITYNAME

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Entity Name")
                            {
                                EntityName = (dt.Rows[k][j].ToString());
                                EntityName = "00000000000000000000" + EntityName;
                                EntityName = EntityName.Substring(EntityName.Length - 20);
                                File.Write(EntityName);
                            }
                        }
                    }
                    //string TransactionCode = "";

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Transaction Code")
                            {
                                TransactionCode = (dt.Rows[k][j].ToString());
                                TransactionCode = "00000" + TransactionCode;
                                TransactionCode = TransactionCode.Substring(TransactionCode.Length - 5);
                                File.Write(TransactionCode);
                            }
                        }
                    }
                    //DESCRIPTION

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Description")
                            {
                                Description = (dt.Rows[k][j].ToString());
                                Description = (dt.Rows[k][j].ToString());
                                Description = "00000000000000000000000000000000000000000000000000" + Description;
                                Description = Description.Substring(Description.Length - 50);
                                File.Write(Description);
                            }
                        }
                    }
                }
            }
            File.Close();
        }

        public RBC()
        {
            base.Header = "RBCHeader";
            base.TailPrefix = "RBCTail";
        }
    }
}