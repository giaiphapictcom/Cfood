using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Respository;

namespace ImportAffilate
{
    class Program
    {
        private static void AddAffilateCode(string code)
        {
            AffilateCodeRespository affilateCodeRespository = new  AffilateCodeRespository();
             affilateCodeRespository.Insert(code, (byte) StateEnum.Disable, DateTime.Now);

        }
        static void Main(string[] args)
        {
            var excelFile = "Ma-AFFILIATE.xlsx";

            var connectionString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={excelFile};Extended Properties=\"Excel 12.0;HDR=Yes;\"";
            var extension = Path.GetExtension(excelFile);
            if (extension.ToLower().Trim() == ".xls")
                connectionString =
                    $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={excelFile};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";
            else if (extension.ToLower().Trim() == ".xlsx")
                connectionString =
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={excelFile};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";
            List<string> listCode = new List<string>();
            using (var conn = new OleDbConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                var dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count <= 0) return;
                          
                var totalColumn = 10;
              
                var endRow = 200;
                var countRow = 0;
                using (var command = new OleDbCommand("SELECT * FROM [" + dbSchema.Rows[0]["TABLE_NAME"] + "]", conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null && reader.Read() && countRow <= endRow)
                        {
                            var baseIndex = 1;
                            for (int col = 0; col < totalColumn; col++)
                            {
                                string code = "";
                               
                                if (col == 0)
                                {
                                    code = reader[baseIndex].ToString();
                                    baseIndex += 2;
                                }
                               
                                else if (col == totalColumn -1)
                                {
                                   
                                    code = reader[baseIndex] + reader[baseIndex + 1].ToString() +
                                           reader[baseIndex + 2];
                                }
                                else
                                {
                                  
                                    code = reader[baseIndex].ToString() + reader[baseIndex + 1];
                                    baseIndex += 3;
                                }
                                listCode.Add(code);
                            }
                            countRow += 1;



                        }

                    }


                }

            }

            if (listCode.Count>0)
            {
                
                foreach (var code in listCode)
                {
                    var newCode = code;
                    if (!code.Contains("M"))
                    {
                        newCode = StringHelper.GenerateUpperCasePrefix() + code;
                    }
                    AddAffilateCode(newCode.Replace(".","").Replace(" ",""));
                }

                
            }
            Console.WriteLine("Done !");
            Console.ReadKey();

        }
       
    }
}
