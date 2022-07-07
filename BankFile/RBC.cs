using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace BankFile
{
        public class RBC : BankFile
        {
            public override void Export(DataTable dt)
            {
                FileName = FileName.Replace(".xlsx", ".txt");
                StreamWriter File = new StreamWriter(FileName);
                int FalseRows = (dt.Rows.Count - 1);
                string Rows = (FalseRows.ToString());
                long TotalAmount = 0;
                DateTime datetime;
                int CountRec = 0;
                string TotRec = "000000000" + CountRec.ToString();
                TotRec = TotRec.Substring(TotRec.Length - 9);

                //FileNumber and Number of Days
                FileNo = "0000" + FileNo;
                FileNo = FileNo.Substring(FileNo.Length - 4);
                datetime = Convert.ToDateTime(NoOfDays);
                NoOfDays = datetime.DayOfYear.ToString();
                NoOfDays = "000" + NoOfDays;
                NoOfDays = NoOfDays.Substring(NoOfDays.Length - 3);

            //CompanyName
                string CompanyName2;
                CompanyName2 = CompanyName;
                CompanyName = CompanyName + "                              ";
                CompanyName = CompanyName.Substring(0, 30);
                CompanyName2 = CompanyName2 + "               ";
                CompanyName2 = CompanyName2.Substring(0, 15);
            //TOTALAMOUNT
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j].ToString() == "Amount")
                        //{
                            for (int x = i + 1; x < dt.Rows.Count; x++)
                            {
                                TotalAmount = Convert.ToInt64(Convert.ToDecimal(dt.Rows[x][2].ToString()) * 100) + TotalAmount;
                            }
                        //}
                    }
                }
                string AccountNumber = "";
                string TransitC = "";
                string Amount = "";
                string EntityID = "";
                string Description = "";
                string TransactionCode = "";

                //_______________________________________________________________________________________________________
                //File Write
                //HEADER

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
                TotAmount = "0000000000000000000000" + TotAmount;
                TotAmount = TotAmount.Substring(TotAmount.Length - 14);

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
                    Description = "";
                    TransactionCode = "";
                    base.Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "CompanyName1234ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + "     000000000000000                        00000000000";

                    base.Record = base.Record.Replace("Day", NoOfDays);
                    base.Record = base.Record.Replace("Bank", CompanyBank);
                    base.Record = base.Record.Replace("Brnch", CompanyBranch);
                    base.Record = base.Record.Replace("Account", CompanyAccount);
                    base.Record = base.Record.Replace("Originator", OriginatorID);
                    base.Record = base.Record.Replace("CompanyName12345", CompanyName);
                if (CompanyName == "MB ENTERPRISES I")
                {
                    base.Record = base.Record.Replace("CompanyName1234", "MB ENTERPRISES");
                }
                else
                {
                    base.Record = base.Record.Replace("CompanyName1234", CompanyName2);
                }
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
                        File.Write(base.FileType + base.RecordPrefix);
                    }

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
                                //if (dt.Rows[i][j].ToString() == "Account")
                                //{
                                    AccountNumber = dt.Rows[k][4].ToString();

                                    AccountNumber = AccountNumber + "            ";
                                    AccountNumber = AccountNumber.Substring(0, 12);
                                    //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNT     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("ACCOUNT", AccountNumber);
                                //}
                            }
                        }

                        //TRANSITCODE

                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "TransitCode")
                                //{
                                    TransitC = (dt.Rows[k][3].ToString());
                                    TransitC = "000000000" + TransitC;
                                    TransitC = TransitC.Substring(TransitC.Length - 9);
                                    //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("TRANSITCO", TransitC);
                                //}
                            }
                        }

                        //AMOUNT
                        long Am;
                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "Amount")
                                //{
                                    Amount = (dt.Rows[k][2].ToString());
                                    Am = ((Convert.ToInt64(Convert.ToDecimal(Amount) * 100)));
                                    Amount = "0000000000" + (Am).ToString();
                                    Amount = Amount.Substring(Amount.Length - 10);
                                    //base.Record = "TCOAMOUNT_IND022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("AMOUNT_IND", Amount);
                                //}
                            }
                        }
                        //ENTITYID1

                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "EntityID")
                                //{
                                    EntityID = (dt.Rows[k][0].ToString());
                                    EntityID = EntityID + "                              ";
                                    EntityID = EntityID.Substring(0, 30);
                                    //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("ID", EntityID);
                                //}
                            }
                        }
                        //ENTITYID2

                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "EntityID")
                                //{
                                    EntityID = EntityID + "                   ";
                                    EntityID = EntityID.Substring(0, 19);
                                    //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000ID                 0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("Id", EntityID);

                                    //File.Write(EntityName);
                                //}
                            }
                        }
                        //TRANSACTIONCODE
                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "TranCode")
                                //{
                                    TransactionCode = (dt.Rows[k][5].ToString());
                                    TransactionCode = "000" + TransactionCode;
                                    TransactionCode = TransactionCode.Substring(TransactionCode.Length - 3);
                                    //base.Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                    base.Record = base.Record.Replace("TCO", TransactionCode);
                                //}
                            }
                        }
                     
                        //DESCRIPTION

                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //if (dt.Rows[i][j].ToString() == "Description")
                                //{
                                    Description = (dt.Rows[k][6].ToString());
                                    //Description = (dt.Rows[k][j].ToString());
                                    Description = "00000000000000000000000000000000000000000000000000" + Description;
                                    Description = Description.Substring(Description.Length - 50);
                                //}
                            }
                        }
                        File.Write(base.Record);
                    }
                }

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
            if (base.FileType == "C")
            {
                base.TailPrefix = base.TailPrefix.Insert(23, TotAmount);
                base.TailPrefix = base.TailPrefix.Insert(23 + TotAmount.Length, Rows);
                File.WriteLine("");
                File.WriteLine("Z" + base.TailPrefix);
                File.Close();
            }
            else if (base.FileType == "D")
            {
                base.TailPrefix = base.TailPrefix.Insert(23, TotAmount);
                base.TailPrefix = base.TailPrefix.Insert(23 + TotAmount.Length, Rows
                + spaces.PadRight(1418, '0'));
                File.WriteLine("");
                File.WriteLine("Z" + base.TailPrefix);
                File.Close();
            }

        }

            public RBC()
            {
                base.SubHeader = "TOTALRECDOriginatorFile022DayDeDaC" + spaces.PadRight(20) + "CAD" + spaces.PadRight(1406);
                base.RecordPrefix = "TOTALRECDOriginatorFile";
                base.Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "CompanyName1234ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + spaces.PadRight(5) + spaces.PadRight(15, '0') + spaces.PadRight(24);
                base.TailPrefix = "TOTALRECDOriginatorFile" + spaces.PadRight(22, '0');
            }
        }
    }
