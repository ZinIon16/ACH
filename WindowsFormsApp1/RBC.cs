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
            int FalseRows = (dt.Rows.Count - 1);
            string Rows = (FalseRows.ToString());
            long TotalAmount = 0;
            //string TotAmount = "";
            string NoOfDays;
            //string spaces = "";
            int CountRec = 0;
            string TotRec = "000000000" + CountRec.ToString();
            TotRec = TotRec.Substring(TotRec.Length - 9);

            //FileNumber and Number of Days
            FileNo = "0000" + FileNo;
            FileNo = FileNo.Substring(FileNo.Length - 4);
            NoOfDays = (DateTime.Now.DayOfYear).ToString();
            NoOfDays = "000" + NoOfDays;
            NoOfDays = NoOfDays.Substring(NoOfDays.Length - 3);

            //CompanyName
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
            //string EntityName = "";
            //string TransactionCode;
            string Description = "";

            //_______________________________________________________________________________________________________
            //File Write
            //HEADER
            //base.Header = base.Header.Replace("$$AAPDCPA1464[PROD[NL$$", header);
            File.WriteLine(Header + spaces.PadRight(1441));

            //SUBHEADER
            CountRec += 1;
            TotRec = "";
            TotRec = "000000000" + CountRec.ToString();
            TotRec = TotRec.Substring(TotRec.Length - 9);
            base.SubHeader = base.SubHeader.Replace("File", FileNo);
            base.SubHeader = base.SubHeader.Replace("Day", NoOfDays);
            base.SubHeader = base.SubHeader.Replace("TOTALRECD", TotRec);
            base.SubHeader = base.SubHeader.Replace("DeDaC", DestinationDataCenter);
            base.SubHeader = base.SubHeader.Replace("Originator", OriginatorID);
            File.Write("A" + base.SubHeader);

            //ROWS
            Rows = "00000000" + Rows;
            Rows = Rows.Substring(Rows.Length - 8);

            //TOTAL AMOUNT
            TotAmount = Convert.ToString(TotalAmount);
            TotAmount = "000000000000000000000000000000000000" + TotAmount;
            TotAmount = TotAmount.Substring(TotAmount.Length - 36);

            //counter logic
            int counter;
            if ((dt.Rows.Count - 1) % 6 == 0)
            {
                counter = (((dt.Rows.Count - 1) % 6));
            }
            else
            {
                counter = (6 - ((dt.Rows.Count - 1) % 6));
            }
            for (int k = 1; k < ((dt.Rows.Count) + counter); k++)
            {
                AccountNumber = "";
                TransitC = "";
                Amount = "";
                EntityID = "";
                //EntityName = "";
                //TransactionCode = "";
                Description = "";
                base.Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "MB ENTERPRISES ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + "     000000000000000                        00000000000";

                base.Record = base.Record.Replace("Day", NoOfDays);
                base.Record = base.Record.Replace("Bank", CompanyBank);
                base.Record = base.Record.Replace("Brnch", CompanyBranch);
                base.Record = base.Record.Replace("Account", CompanyAccount);
                base.Record = base.Record.Replace("Originator", OriginatorID);
                base.Record = base.Record.Replace("CompanyName12345", CompanyName);

                if (k % 6 == 1)
                {
                    CountRec += 1;
                    base.RecordPrefix = "TOTALRECDOriginatorFile";
                    TotRec = "";
                    TotRec = "000000000" + CountRec.ToString();
                    TotRec = TotRec.Substring(TotRec.Length - 9);

                    base.RecordPrefix = base.RecordPrefix.Replace("TOTALRECD", TotRec);
                    base.RecordPrefix = base.RecordPrefix.Replace("File", FileNo);
                    base.RecordPrefix = base.RecordPrefix.Replace("Originator", OriginatorID);

                    File.WriteLine("");
                    File.Write("C" + base.RecordPrefix);
                }
                //else
                //{
                //}

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
                                //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNT     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                base.Record = base.Record.Replace("ACCOUNT", AccountNumber);

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
                                //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                base.Record = base.Record.Replace("TRANSITCO", TransitC);
                            }
                        }
                    }

                    //AMOUNT
                    long Am;
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "Amount")
                            {
                                Amount = (dt.Rows[k][j].ToString());
                                Am = Convert.ToInt64(Convert.ToDecimal(Amount));
                                Amount = "0000000000" + (Am * 100).ToString();
                                Amount = Amount.Substring(Amount.Length - 10);
                                //base.Record = "TCOAMOUNT_IND022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                base.Record = base.Record.Replace("AMOUNT_IND", Amount);

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
                                EntityID = EntityID + "                              ";
                                EntityID = EntityID.Substring(0, 30);
                                //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                base.Record = base.Record.Replace("ID", EntityID);

                                //File.Write(EntityID);
                            }
                        }
                    }
                    //ENTITYID2

                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString() == "EntityID")
                            {
                                EntityID = EntityID + "                   ";
                                EntityID = EntityID.Substring(0, 19);
                                //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000ID                 0003000021139658     000000000000000                        ";
                                base.Record = base.Record.Replace("Id", EntityID);

                                //File.Write(EntityName);
                            }
                        }
                    }

                    ////TRANSACTIONCODE
                    //for (int i = 0; i < dt.Rows.Count - 1; i++)
                    //{
                    //    for (int j = 0; j < dt.Columns.Count; j++)
                    //    {
                    //        if (dt.Rows[i][j].ToString() == "TranCode")
                    //        {
                    //            TransactionCode = (dt.Rows[k][j].ToString());
                    //            TransactionCode = "000" + TransactionCode;
                    //            TransactionCode = TransactionCode.Substring(TransactionCode.Length - 3);
                    //            //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                    //            base.Record = base.Record.Replace("TCO", TransactionCode);
                    //            //File.Write(TransactionCode);
                    //        }
                    //    }
                    //}

                    //TCO
                    base.Record = base.Record.Replace("TCO", TCO);
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
                                //File.Write(Description);
                            }
                        }
                    }
                    File.Write(base.Record);
                }
            }
            //File.WriteLine("");
            CountRec += 1;
            TotRec = "";
            TotRec = "000000000" + CountRec.ToString();
            TotRec = TotRec.Substring(TotRec.Length - 9);

            //padding for empty records
            switch (counter)
            {
                case 1:
                    File.Write(spaces.PadRight((counter * 240)));
                    break;

                case 2:
                    File.Write(spaces.PadRight((counter * 240)));
                    break;

                case 3:
                    File.Write(spaces.PadRight((counter * 240)));
                    break;

                case 4:
                    File.Write(spaces.PadRight((counter * 240)));
                    break;

                case 5:
                    File.Write(spaces.PadRight((counter * 240)));
                    break;
            }
            //Tail Prefix
            base.TailPrefix = base.TailPrefix.Replace("File", FileNo);
            //base.TailPrefix = base.TailPrefix.Replace("ROWSTOTA", Rows);
            base.TailPrefix = base.TailPrefix.Replace("TOTALRECD", TotRec);
            base.TailPrefix = base.TailPrefix.Replace("Originator", OriginatorID);
            //Insert
            base.TailPrefix = base.TailPrefix.Insert(23, TotAmount);
            base.TailPrefix = base.TailPrefix.Insert(23 + TotAmount.Length, Rows);
            File.WriteLine("");
            File.WriteLine("Z" + base.TailPrefix);
            File.Close();
        }

        public RBC()
        {
            base.SubHeader = "TOTALRECDOriginatorFile022DayDeDaC" + spaces.PadRight(20) + "CAD" + spaces.PadRight(1406);
            base.TailPrefix = "TOTALRECDOriginatorFile" + spaces.PadRight(1396, '0');
            base.RecordPrefix = "TOTALRECDOriginatorFile";
            base.Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "MB ENTERPRISES ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + spaces.PadRight(5) + spaces.PadRight(15, '0') + spaces.PadRight(24);
        }
    }
}