﻿//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Web;
//using Microsoft.Office.Interop.Excel;
//using _Excel = Microsoft.Office.Interop.Excel;
//using Workbook = Microsoft.Office.Interop.Excel.Workbook;
//using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;
//using System.Threading.Tasks;

//namespace WindowsFormsApp1
//{
//    public class Excel
//    {
//        string path = "";
//        _Application excel = new _Excel.Application();
//        Workbook wb;
//        Worksheet ws;


//        public Excel(string path, int Sheet)
//        {
//            this.path = path;
//            wb = excel.Workbooks.Open(path);
//            ws = wb.Worksheets[Sheet];
//        }
//        public string ReadCell(int i, int j)
//        {
//            i++;
//            j++;
//            if (ws.Cells[i, j].Value2 != null)
//                return ws.Cells[i, j].Value2;
//            else
//                return null;
//        }
//    }
//}
