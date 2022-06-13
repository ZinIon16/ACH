using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace BMO
{
    public class BMO 
    {
        public string MainHeader;
        public string SubHeader;
        public string TailPrefix;
        public string Tail;
        public string RecordPrefix;
        public string Record;
        public string spaces = "";
        public string TotAmount = "";
        public string FileNo, CompanyBank, CompanyBranch, CompanyAccount, Header, DestinationDataCenter, OriginatorID, CompanyName;
        public string FileType;
        public string FileName;
        public string NoOfDays;

        public void Export(DataTable dt)
        {

            FileName = Path.GetFileName(FileName);
            FileName = FileName.Replace(".xlsx", ".txt");
            StreamWriter File = new StreamWriter(FileName);
            int FalseRows = (dt.Rows.Count - 1);
            string Rows = (FalseRows.ToString());
            string Rows2 = Rows;
            long TotalAmount = 0;
            String CompanyName2;
            string PrefixVar = "";

            DateTime datetime;

            //FileNumber and Number of Days
            FileNo = "0000" + FileNo;
            FileNo = FileNo.Substring(FileNo.Length - 4);
            datetime = Convert.ToDateTime(NoOfDays);
            NoOfDays = datetime.DayOfYear.ToString();
            NoOfDays = "000" + NoOfDays;
            NoOfDays = NoOfDays.Substring(NoOfDays.Length - 3);

            //CompanyName
            CompanyName2 = CompanyName;
            CompanyName2 = CompanyName2 + "               ";
            CompanyName2 = CompanyName2.Substring(0, 15);
            CompanyName = CompanyName + "                              ";
            CompanyName = CompanyName.Substring(0, 30);

            //TOTALAMOUNT
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
            string AccountNumber = "";
            string TransitC = "";
            string Amount = "";
            string EntityID = "";
            string EntityName = "";
            string TransactionCode = "";

            //_______________________________________________________________________________________________________
            //File Write
            //MAINHEADER

            MainHeader = MainHeader.Replace("Originator", OriginatorID);
            MainHeader = MainHeader.Replace("File", FileNo);
            MainHeader = MainHeader.Replace("Day", NoOfDays);
            MainHeader = MainHeader.Replace("DeDaC", DestinationDataCenter);
            File.Write("A" + MainHeader);

            //Prefix
            if (FileType == "C")
            {
                PrefixVar = "C";
            }
            else if (FileType == "D")
            {
                PrefixVar = "D";
            }

            //SUBHEADER
            File.WriteLine("");
            SubHeader = SubHeader.Replace("?", PrefixVar);

            //TRANSACTIONCODE
            for (int k = 1; k < ((dt.Rows.Count)); k++)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j].ToString() == "TranCode")
                        {
                            TransactionCode = (dt.Rows[k][j].ToString());
                            TransactionCode = "000" + TransactionCode;
                            TransactionCode = TransactionCode.Substring(TransactionCode.Length - 3);
                            //Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                            Record = Record.Replace("TCO", TransactionCode);
                            //File.Write(TransactionCode);
                        }
                    }
                }
            }
            SubHeader = SubHeader.Replace("TCO", TransactionCode);
            SubHeader = SubHeader.Replace("Day", NoOfDays);
            SubHeader = SubHeader.Replace("CompanyNameName", CompanyName2);
            SubHeader = SubHeader.Replace("CompanyName1234CompanyName1234", CompanyName);
            SubHeader = SubHeader.Replace("Bank", CompanyBank);
            SubHeader = SubHeader.Replace("Brnch", CompanyBranch);
            SubHeader = SubHeader.Replace("Account", CompanyAccount);
            File.Write(SubHeader);

            //ROWS1
            Rows = "00000000" + Rows;
            Rows = Rows.Substring(Rows.Length - 8);

            //ROWS2
            Rows2 = "00000" + Rows2;
            Rows2 = Rows2.Substring(Rows2.Length - 5);

            //TOTAL AMOUNT
            TotAmount = Convert.ToString(TotalAmount);
            TotAmount = "00000000000000" + TotAmount;
            TotAmount = TotAmount.Substring(TotAmount.Length - 14);

            for (int k = dt.Rows.Count; k > 0; k--)
            {
                AccountNumber = "";
                TransitC = "";
                Amount = "";
                EntityID = "";
                EntityName = "";

                Record = "AMOUNT_INDTRANSITCOACCOUNT" + "EntityName" + "ID";

                if (k > dt.Rows.Count - 1)
                {
                    //File.Write("                                                                                                                                                                                                             ");
                    //File.Write("00000000000");
                }
                else
                {
                    //Account Number
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Account")
                            {
                                AccountNumber = dt.Rows[k][j].ToString();

                                AccountNumber = AccountNumber + "            ";
                                AccountNumber = AccountNumber.Substring(0, 12);
                                Record = Record.Replace("ACCOUNT", AccountNumber);

                                //File.Write(AccountNumber);
                            }
                        }
                    }

                    //TRANSITCODE

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "TransitCode")
                            {
                                TransitC = (dt.Rows[k][j].ToString());
                                TransitC = "000000000" + TransitC;
                                TransitC = TransitC.Substring(TransitC.Length - 9);
                                Record = Record.Replace("TRANSITCO", TransitC);
                            }
                        }
                    }

                    //AMOUNT
                    long Am = 0;
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Amount")
                            {
                                Amount = (dt.Rows[k][j].ToString());
                                Am = ((Convert.ToInt64(Convert.ToDecimal(Amount) * 100)));
                                Amount = "0000000000" + (Am).ToString();
                                Amount = Amount.Substring(Amount.Length - 10);
                                Record = Record.Replace("AMOUNT_IND", Amount);

                                //File.Write(Amount);
                            }
                        }
                    }
                    //ENTITYID1

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "EntityID")
                            {
                                EntityID = (dt.Rows[k][j].ToString());
                                EntityID = EntityID + "                   ";
                                EntityID = EntityID.Substring(0, 19);
                                Record = Record.Replace("ID", EntityID);

                                //File.Write(EntityID);
                            }
                        }
                    }
                    //ENTITYNAME

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "EntityName")
                            {
                                EntityName = (dt.Rows[k][j].ToString());
                                EntityName = EntityName + "                             ";
                                EntityName = EntityName.Substring(0, 29);
                                Record = Record.Replace("EntityName", EntityName);

                                //File.Write(EntityName);
                            }
                        }
                    }
                    //DESCRIPTION

                    //for (int i = 0; i < dt.Rows.Count - 1; i++)
                    //{
                    //    for (int j = 0; j < dt.Columns.Count; j++)
                    //    {
                    //        if (dt.Rows[i][j].ToString() == "Description")
                    //        {
                    //            Description = (dt.Rows[k][j].ToString());
                    //            Description = (dt.Rows[k][j].ToString());
                    //            Description = "00000000000000000000000000000000000000000000000000" + Description;
                    //            Description = Description.Substring(Description.Length - 50);
                    //            //File.Write(Description);
                    //        }
                    //    }
                    //}
                    File.WriteLine("");
                    File.Write(PrefixVar + Record);
                }
            }

            //Tail Prefix

            Tail = Tail.Replace("?", PrefixVar);
            //Insert
            Tail = Tail.Insert(2, Rows);
            Tail = Tail.Insert(10, TotAmount);
            if (FileType == "C")
            {
                TailPrefix = TailPrefix.Insert(0, spaces.PadRight(19, '0'));
                TailPrefix = TailPrefix.Insert(19, TotAmount);
                TailPrefix = TailPrefix.Insert(TotAmount.Length + 19, Rows2);
            }
            else if (FileType == "D")
            {
                TailPrefix = TailPrefix.Insert(0, TotAmount);
                TailPrefix = TailPrefix.Insert(TotAmount.Length + 0, Rows2);
                TailPrefix = TailPrefix.Insert((TotAmount.Length + 0 + Rows2.Length), spaces.PadRight(19, '0'));
            }
            File.WriteLine("");
            File.WriteLine(Tail);
            File.WriteLine("Z" + TailPrefix);
            File.Close();
        }

        public BMO()
        {
            MainHeader = "OriginatorFile022DayDeDaC" + spaces.PadRight(54);
            SubHeader = "X?TCO022Day" + "CompanyNameName" + "CompanyName1234CompanyName1234" + "BankBrnchAccount" + spaces.PadRight(8);
            Record = "AMOUNT_INDTRANSITCOACCOUNT" + "EntityName" + "ID";
            Tail = "Y?" + spaces.PadRight(56);
            TailPrefix = spaces.PadRight(41);
        }
    }
}

