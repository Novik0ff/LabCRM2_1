using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace CrmLeadImport.Context
{
    public class FileContext
    {
        static public void ImportFromFile(string path, Encoding encoding, ref List<LeadContext> leads)
        {
            try
            {
                StreamReader objReader = new StreamReader(path, encoding);
                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                    {
                        try
                        {
                            var data = sLine.Split(';');
                            LeadContext temp = new LeadContext
                            {
                                Subject = data[0].ToString(),

                                FirstName = data[1].ToString(),

                                LastName = data[2].ToString(),

                                CompanyName = data[3].ToString()
                            };

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
                            throw new ArgumentException($@"Error file string");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Input file doesnsot exists");
            }
        }
    }
}
