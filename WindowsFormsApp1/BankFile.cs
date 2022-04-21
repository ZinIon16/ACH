using System.Data;

namespace WindowsFormsApp1
{
    internal abstract class BankFile
    {
        //public string Header;
        public string SubHeader;

        public string TailPrefix;
        public string RecordPrefix;
        public string Record;
        public string spaces = "";
        public string TotAmount = "";
        public string FileNo, CompanyBank, CompanyBranch, CompanyAccount, Header, DestinationDataCenter, OriginatorID, CompanyName, TCO;

        public abstract void Export(DataTable dt);
    }
}