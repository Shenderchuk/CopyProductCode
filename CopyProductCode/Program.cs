using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Deployment.WindowsInstaller.Linq;

namespace CopyProductCode
{
    class Program
    {
        [DllImport("msi.dll", SetLastError = true)]
        static extern uint MsiOpenDatabase(string szDatabasePath, Int32 phPersist, out Int32 phDatabase);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern int MsiDatabaseOpenViewW(Int32 hDatabase, [MarshalAs(UnmanagedType.LPWStr)] string szQuery, out Int32 phView);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern int MsiViewExecute(Int32 hView, Int32 hRecord);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern uint MsiViewFetch(Int32 hView, out Int32 hRecord);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern int MsiRecordGetString(Int32 hRecord, int iField,
           [Out] StringBuilder szValueBuf, ref int pcchValueBuf);

        [DllImport("msi.dll", ExactSpelling = true)]
        static extern Int32 MsiCreateRecord(uint cParams);

        [DllImport("msi.dll", ExactSpelling = true)]
        static extern uint MsiCloseHandle(Int32 hAny);

        public static string GetVersionInfo(string fileName)
        {
            string sqlStatement = "SELECT * FROM Property WHERE Property = 'ProductCode'";
            
            Int32 phDatabase = 0;
            Int32 phView = 0;
            Int32 hRecord = 0;

            StringBuilder szValueBuf = new StringBuilder();
            //Lenght of variable 
            szValueBuf.Capacity = 33;
            int pcchValueBuf = 255;

            // Open the MSI database in the input file 
            try
            {
                uint val = MsiOpenDatabase(fileName, 0, out phDatabase);
            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                throw new ApplicationException("Something wrong happened during opening MSI :", ex);
            }

            hRecord = MsiCreateRecord(3);

            // Open a view on the Property table for the version property 
            try
            {
                int viewVal = MsiDatabaseOpenViewW(phDatabase, sqlStatement, out phView);
            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                throw new ApplicationException("Something wrong happened during opening Property table:", ex);
            }

            // Execute the view query 
            int exeVal = MsiViewExecute(phView, hRecord);

            // Get the record from the view 
            uint fetchVal = MsiViewFetch(phView, out hRecord);

            // Get the version from the data 
            int retVal = MsiRecordGetString(hRecord, 2, szValueBuf, ref pcchValueBuf);

            uint uRetCode;
            uRetCode = MsiCloseHandle(phDatabase);
            uRetCode = MsiCloseHandle(phView);
            uRetCode = MsiCloseHandle(hRecord);
            return szValueBuf.ToString();
        }
             
        public static string GetVersionInfoWIX (string fileName)
        {
            string szProductCode;

            using (var database = new QDatabase(fileName, DatabaseOpenMode.ReadOnly))
            {
                szProductCode = database.ExecutePropertyQuery("ProductCode");
            }
            return szProductCode;
        }

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            string strGUID;

            //Check if here are any parameters
            if (args == null || args.Length == 0)
            {
                System.Windows.Clipboard.SetText("The tool requires an argument.");
                System.Environment.Exit(1);
            }
            
            string strFileName = args[0];
            
            //Check if MSI file exists
            if (System.IO.File.Exists (strFileName)  == true)
            {
                strGUID = GetVersionInfo(strFileName);

                if (string.IsNullOrEmpty(strGUID))
                {
                    System.Windows.Clipboard.SetText("Something went wrong. ProductCode wasn't copied to clipboard.");
                }
                else
                {
                    System.Windows.Clipboard.SetText(strGUID.Trim());
                }
            }
            else
            {
                System.Windows.Clipboard.SetText("The specified path doesn't exist.");
                System.Environment.Exit(1);
            }
        }   
    }
}
