using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser.Interfaces
{
    public interface ICSVToDataTableParser
    {
        DataTable GetDataTabletFromCSVFile(string csv_file_path);
    }
}
