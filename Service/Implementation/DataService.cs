using Service.Database;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Implementation
{
    public class DataService : IDataService // in sthis service we send a queries to database using ORM EF Core, because in future, if we will test this program, it should be easier
    {
        private TestAppDbContext ctx;
        

        public DataService(TestAppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public float GetHighestAvgTip()
        {
            float result = (float)ctx.SampleCabData.Average(x => x.TipAmount);
            return result;
        }

        public List<SampleCabDatum> GetLongestTripDistances() // get top 100 longest trip distances
        {
            var result = ctx.SampleCabData.OrderByDescending(x => x.TripDistance).Take(100).ToList(); // не знаю почему, по оно работает в обратную сторону. поэтому по убыванию
            return result;
        }

        public List<SampleCabDatum> GetLongestTripDuration() // get top 100 longest trip duration
        {
            var data = ctx.SampleCabData.ToList();  
            var result = data.OrderByDescending(x => (x.TpepDropoffDatetime - x.TpepPickupDatetime)).Take(100).ToList(); 
            return result;
        }

        public List<SampleCabDatum> Search(Func<int, bool> func) // custom search by PUlocationId
        {

            var result = ctx.SampleCabData.Where(x => func.Invoke((int)x.PUlocationId) == true);
            return result.ToList();
        }

        public void Update(SampleCabDatum data)
        {
            ctx.SampleCabData.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChangesAsync();
        }

        public void UpdateRange(List<SampleCabDatum> data) // this method for update range of records 
        {
            foreach(var v in data)
                ctx.SampleCabData.Entry(v).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChangesAsync();
        }

        public int GetEntriesCount() // get a number of rows
        {
            var result = ctx.SampleCabData.Count();
            return result;
        }
    }
}

//P.S я не совсем понял что от меня хотят когда надо сделать инсерт обработанных данных, как я понял это типо апдейт(типо мы данные обработали, и обновили базу данных), в любом случае там асихронное сохранение