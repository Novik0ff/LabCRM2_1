using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CrmLeadImport.Service
{
    class Service
    {
        static Uri ServiceUrl = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc");
        static string User = "Administrator";
        static string Password = "Pass@word99";
        public static bool TryGetOrganization(out IOrganizationService organizationService)
        {
            try
            {
                var credentials = new ClientCredentials
                {
                    Windows = { ClientCredential = new NetworkCredential(User, Password) }
                };
                var service = new OrganizationServiceProxy(ServiceUrl, null, credentials, null);
                organizationService = (IOrganizationService)service;
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                organizationService = null;
                return false;
            }
        }
    }
}
