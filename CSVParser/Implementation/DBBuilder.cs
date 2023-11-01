using CSVParser.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser.Implementation
{
    public class DBBuilder : IDBBuilder // class where we will add data to database
    {
        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Data Source=<type your>;Initial Catalog=TestAppDB;Integrated Security=SSPI;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection)) // bulk insert into database, as said in task
                {
                    s.DestinationTableName = "SampleCabData";
                    foreach (var column in csvFileData.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }
                        
                    s.WriteToServer(csvFileData);
                }
            }
        }
    }
}

// P.S на самом деле можно было вынести строку подключения в конфигурационный файл, но опять же, время...