using System;
using System.Collections.Generic;
using System.IO;
using CrmLeadImport.Context;

namespace CrmLeadImport.ExceptionHandler
{
    public class ExceptionHandler
    {
        public static void AddUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                Console.Error.WriteLine(((Exception)e.ExceptionObject).Message.ToString());
                Console.ReadKey();
            };
        }
        public static void OpenFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException($@"Input file doesnsot exists");
        }

        public static void CheckList(List<LeadContext> leads)
        {
            if (!(leads.Count > 0))
                throw new ArgumentException($@"List empty");
        }
    }
}
