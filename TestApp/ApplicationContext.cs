using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVParser;
using CSVParser.Implementation;
using CSVParser.Interfaces;
using Service.Database;
using Service.Implementation;
using Service.Interfaces;

namespace TestApp
{
    public class ApplicationContext
    {
        public ApplicationContext()
        {
            dbContext = new TestAppDbContext();
            tableParser = new CSVToDataTableParser();
            dbBuilder = new DBBuilder();
            dataService = new DataService(dbContext);

        }
        public ICSVToDataTableParser tableParser { get; set; }
        public IDBBuilder dbBuilder { get; set; }
        public IDataService dataService { get; set; }
        public TestAppDbContext dbContext { get; set; }
        
    }
}
