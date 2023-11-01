using CSVParser.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace CSVParser.Implementation
{
    public class CSVToDataTableParser : ICSVToDataTableParser // class where we will prepare data for adding this one to database
    {
        public DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    var neededColumns = new List<string>(); //1. Import the data from the CSV into an MS SQL table. We only want to store the following columns
                                                            // we have a list with needed columns and fill this one

                    neededColumns.Add("tpep_pickup_datetime");
                    neededColumns.Add("tpep_dropoff_datetime");
                    neededColumns.Add("passenger_count");
                    neededColumns.Add("trip_distance");
                    neededColumns.Add("store_and_fwd_flag");
                    neededColumns.Add("PULocationID");
                    neededColumns.Add("DOLocationID");
                    neededColumns.Add("fare_amount");
                    neededColumns.Add("tip_amount");


                    var uniqueEntries = new HashSet<string>(); // we have hash set for filtering duplicates, as we know, we cant add duplicate data to hash set
                    

                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;


                    var colFields = csvReader.ReadFields();
                    var filteredColFiels = new Dictionary<int, string>();

                    var pickupDatetimeIndex = Array.IndexOf(colFields, "tpep_pickup_datetime");
                    var dropoffDatetimeIndex = Array.IndexOf(colFields, "tpep_dropoff_datetime"); // set indexes for columns where we will find duplicates
                    var passengerCountIndex = Array.IndexOf(colFields, "passenger_count");

                    for (int i = 0; i < colFields.Length; i++)
                    {
                        if (neededColumns.Contains(colFields[i]))
                        {
                            filteredColFiels.Add(i, colFields[i]); // create map for filtering columns
                            
                        }
                        
                    }
                    

                    foreach (var column in filteredColFiels)
                    {
                        var datecolumn = new DataColumn(column.Value);
                        datecolumn.AllowDBNull = true;
                        
                        csvData.Columns.Add(datecolumn); //add column names to data table
                    }
                    
                    
                    while (!csvReader.EndOfData) // for this part will be better create special BLL (Buisness Logic Layer), but I have only 4 hours (((
                    {
                        var fieldData = csvReader.ReadFields();
                        
                        var filteredFieldData = new List<string>();

                        var uniqueString = ""; // create value for row wich we will filter

                        for(int i = 0; i < fieldData.Length; i++)
                        {
                            var tempDate = new DateTime();
                            if((filteredColFiels.ContainsKey(i) && filteredColFiels[i] == "tpep_pickup_datetime")
                                || (filteredColFiels.ContainsKey(i) && filteredColFiels[i] == "tpep_dropoff_datetime")) // convert to UTC
                                TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(fieldData[i]));
                            
                            if (fieldData[i] == "") // if field have not data, we change this one to null
                                fieldData[i] = null;

                            if (filteredColFiels.ContainsKey(i) && filteredColFiels[i] == "store_and_fwd_flag") // change Y/N to Yes/No
                            {
                                if (fieldData[i] == "Y")
                                    fieldData[i] = "Yes";
                                if (fieldData[i] == "N")
                                    fieldData[i] = "No";
                            }

                            if(i == pickupDatetimeIndex 
                                || i == dropoffDatetimeIndex
                                || i == passengerCountIndex) // generate a row for future check for duplicating
                            {
                                uniqueString += fieldData[i] + ",";
                            }
                            
                            if (filteredColFiels.ContainsKey(i)) // add only require data
                                filteredFieldData.Add(fieldData[i]);
                            
                        }

                        if (!uniqueEntries.Add(uniqueString.Substring(0, uniqueString.Length - 1))) // try to add our row to HashSet, if entry already exist, we write this one to duplicate.csv
                        {
                            WriteDuplicates("./duplicates.csv", uniqueString.Substring(0, uniqueString.Length - 1));
                            
                        }
                        else csvData.Rows.Add(filteredFieldData.ToArray()); // else add data to our data table
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return csvData;
        }

        private void WriteDuplicates(string path, string data)
        {
            using(StreamWriter writer = new StreamWriter(path, true)) // simple writer for add entry to declared file
            {
                writer.WriteLineAsync(data);
            }
        }
    }
}

// P.S. впервые работал с CSV файлом, не до конца понял как происходит добавление/чтение что бы это красиво оформить, не судите строго,
// про CSVHelper который можно подключить с нюгета я узнал когда уже все написал, и уже не было времени переделывать =(