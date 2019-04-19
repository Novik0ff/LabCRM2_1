using Microsoft.Xrm.Sdk;
using System;

namespace CrmLeadImport.Context
{
    public partial class LeadContext
    {
        public string Subject { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public int NumberOfEmployees { get; set; }

        public decimal Revenue { get; set; }

        public bool ExportLeadToCRM(IOrganizationService service)
        {
            try
            {
                Entity lead = new Entity("lead");

                lead.Attributes["firstname"] = FirstName;
                lead.Attributes["lastname"] = LastName;
                lead.Attributes["numberofemployees"] = NumberOfEmployees;
                lead.Attributes["revenue"] = Revenue;
                lead.Attributes["subject"] = Subject;
                lead.Attributes["companyname"] = CompanyName;

                service.Create(lead);

                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error export lead to CRM");
            }
        }
    }
}
