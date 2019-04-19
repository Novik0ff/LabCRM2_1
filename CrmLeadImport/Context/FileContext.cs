using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmLeadImport.Context
{
    public class FileContext
    {
        static public bool TryImportFile(string path, Encoding encoding, ref List<LeadContext> leads)
        {
            try
            {
                StreamReader objReader = new StreamReader(path, encoding);
                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    try
                    {
                        var data = sLine.Split(';');
                        LeadContext temp = new LeadContext();

                        temp.Subject = data[0].ToString();

                        temp.FirstName = data[1].ToString();

                        temp.LastName = data[2].ToString();

                        temp.CompanyName = data[3].ToString();

                        if (int.TryParse(data[4].ToString(), out int number))
                        {
                            temp.NumberOfEmployees = number;
                        }

                        if (decimal.TryParse(data[5].ToString(), out decimal revenue))
                        {
                            temp.Revenue = revenue;
                        }

                        leads.Add(temp);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException($@"Input file doesnsot exists");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
