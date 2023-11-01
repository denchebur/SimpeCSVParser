using CSVParser.Implementation;
using CSVParser.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Database;
using Service.Implementation;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            var ctx = new ApplicationContext();
            var data = ctx.tableParser.GetDataTabletFromCSVFile("./sample-cab-data.csv");
            ctx.dbBuilder.InsertDataIntoSQLServerUsingSQLBulkCopy(data); // show result (Number of rows in your table after running the program)

            Console.WriteLine(ctx.dataService.GetEntriesCount());

            /*Console.WriteLine("Top 100 distances: ");
            foreach(var v in ctx.dataService.GetLongestTripDistances())
            {
                Console.WriteLine($"| {v.TripDistance}   | {v.PUlocationId}   |");
            }
            Console.WriteLine("Top 100 durations: ");
            foreach (var v in ctx.dataService.GetLongestTripDuration())
            {
                Console.WriteLine($"| {v.TpepDropoffDatetime - v.TpepPickupDatetime}    | {v.PUlocationId}   |");
            }
            Console.WriteLine($"Average Tip : {ctx.dataService.GetHighestAvgTip}");
*/
        }
    }
}

// P.S все протестил если что, работает, оставлю тестовые строки в коментариях на всякий