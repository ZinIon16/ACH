using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace RBC
{
    public class RBC
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
            SubHeader = SubHeader.Replace("File", FileNo);
            SubHeader = SubHeader.Replace("Day", NoOfDays);
            SubHeader = SubHeader.Replace("TOTALRECD", TotRec);
            SubHeader = SubHeader.Replace("DeDaC", DestinationDataCenter);
            SubHeader = SubHeader.Replace("Originator", OriginatorID);
            File.Write("A" + SubHeader);

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
                Description = "";
                TransactionCode = "";
                Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "MB ENTERPRISES ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + "     000000000000000                        00000000000";

                Record = Record.Replace("Day", NoOfDays);
                Record = Record.Replace("Bank", CompanyBank);
                Record = Record.Replace("Brnch", CompanyBranch);
                Record = Record.Replace("Account", CompanyAccount);
                Record = Record.Replace("Originator", OriginatorID);
                Record = Record.Replace("CompanyName12345", CompanyName);

                if (k % 6 == 1)
                {
                    CountRec += 1;
                    RecordPrefix = "TOTALRECDOriginatorFile";
                    TotRec = "";
                    TotRec = "000000000" + CountRec.ToString();
                    TotRec = TotRec.Substring(TotRec.Length - 9);

                    RecordPrefix = RecordPrefix.Replace("TOTALRECD", TotRec);
                    RecordPrefix = RecordPrefix.Replace("File", FileNo);
                    RecordPrefix = RecordPrefix.Replace("Originator", OriginatorID);

                    File.WriteLine("");
                    File.Write("C" + RecordPrefix);
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
                            if (dt.Rows[i][j].ToString() == "Account")
                            {
                                AccountNumber = dt.Rows[k][j].ToString();

                                AccountNumber = AccountNumber + "            ";
                                AccountNumber = AccountNumber.Substring(0, 12);
                                //Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNT     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                Record = Record.Replace("ACCOUNT", AccountNumber);
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
                                //Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                Record = Record.Replace("TRANSITCO", TransitC);
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
                                Am = ((Convert.ToInt64(Convert.ToDecimal(Amount) * 100)));
                                Amount = "0000000000" + (Am).ToString();
                                Amount = Amount.Substring(Amount.Length - 10);
                                //Record = "TCOAMOUNT_IND022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB STARTOFENTITYNAMEENDOFENTITYNAME      MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                Record = Record.Replace("AMOUNT_IND", Amount);
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
                                //Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000STARTOFENTITYNAMEENDOFENTITYNAME0003000021139658     000000000000000                        ";
                                Record = Record.Replace("ID", EntityID);
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
                                //Record = "TCOAMOUNTTT00022088TRANSITCOACCOUNTNUMBER     0000000000000000000000000MB ID                            MB ENTERPRISES I              2689620000ID                 0003000021139658     000000000000000                        ";
                                Record = Record.Replace("Id", EntityID);

                                //File.Write(EntityName);
                            }
                        }
                    }
                    //TRANSACTIONCODE
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
                            }
                        }
                    }
                    //TCO
                    //Record = Record.Replace("TCO", TCO);
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
                            }
                        }
                    }
                    File.Write(Record);
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
            TailPrefix = TailPrefix.Replace("File", FileNo);
            //TailPrefix = TailPrefix.Replace("ROWSTOTA", Rows);
            TailPrefix = TailPrefix.Replace("TOTALRECD", TotRec);
            TailPrefix = TailPrefix.Replace("Originator", OriginatorID);
            //Insert
            TailPrefix = TailPrefix.Insert(23, TotAmount);
            TailPrefix = TailPrefix.Insert(23 + TotAmount.Length, Rows);
            File.WriteLine("");
            File.WriteLine("Z" + TailPrefix);
            File.Close();
        }

        public RBC()
        {
            SubHeader = "TOTALRECDOriginatorFile022DayDeDaC" + spaces.PadRight(20) + "CAD" + spaces.PadRight(1406);
            RecordPrefix = "TOTALRECDOriginatorFile";
            Record = "TCOAMOUNT_IND022DayTRANSITCOACCOUNT" + spaces.PadRight(25, '0') + "MB ENTERPRISES ID" + "CompanyName12345" + "OriginatorIdBankBrnchAccount" + spaces.PadRight(5) + spaces.PadRight(15, '0') + spaces.PadRight(24);
            TailPrefix = "TOTALRECDOriginatorFile" + spaces.PadRight(1396, '0');
        }
    }
}

