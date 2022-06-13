using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BankFile
{
    public abstract class BankFile
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

        public abstract void Export(DataTable dt);
    }
}