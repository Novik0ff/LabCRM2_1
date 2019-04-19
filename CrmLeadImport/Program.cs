using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CrmLeadImport;
using CrmLeadImport.Context;
using CrmLeadImport.ExceptionHandler;
using CrmLeadImport.Service;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;

namespace LeadImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ExceptionHandler.AddUnhandledExceptionHandler();

            string ImportFilePath = @"../../Files/Leads.csv1";

            Console.Write("Import leads from file to CRM\r\n\r\n");
            List<LeadContext> leads = new List<LeadContext>();

            ExceptionHandler.OpenFile(ImportFilePath);

            FileContext.TryImportFile(ImportFilePath,Encoding.GetEncoding(1251), ref leads);

                if (leads.Count > 0)
                {
                    Parallel.ForEach(leads,
                        (r) =>
                        {
                            IOrganizationService taskService;
                            if (Service.TryGetOrganization(out taskService))
                            {
                                Console.WriteLine(String.Format("Current Thread : {0}, Current RecData {1}",
                                Thread.CurrentThread.ManagedThreadId, r.ExportLeadToCRM(taskService)));
                            }
                        });
                }
            Console.Read();
        }

    }
}
