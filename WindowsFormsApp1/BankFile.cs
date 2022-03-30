using System;
using System.Data;
using System.IO;

namespace WindowsFormsApp1
{
    internal abstract class BankFile
    {
        public string HeaderPrefix;
        public string TailPrefix;
        public abstract void Export(DataTable dt);

    }
}
