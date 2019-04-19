using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrmLeadImport.Context;
using CrmLeadImport.ExceptionHandler;
using CrmLeadImport.Service;

namespace LeadImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ExceptionHandler.AddUnhandledExceptionHandler();
            string ImportFilePath = @"../../Files/Leads.csv";
            Console.Write("Import leads from file to CRM\r\n\r\n");
            List<LeadContext> leads = new List<LeadContext>();
            ExceptionHandler.OpenFile(ImportFilePath);
            FileContext.ImportFromFile(ImportFilePath, Encoding.GetEncoding(1251), ref leads);
            ExceptionHandler.CheckList(leads);
            Parallel.ForEach(leads, (r) =>
                 {
                     Console.WriteLine(String.Format("Current Thread : {0}, Current RecData {1}",
                     Thread.CurrentThread.ManagedThreadId, r.ExportLeadToCRM(Service.GetOrganization())));
                 });
            Console.Write("Finish");
            Console.Read();
        }
    }
}
