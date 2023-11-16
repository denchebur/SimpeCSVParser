using Service.Database;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Implementation
{
    public class DataService : IDataService 
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

        public List<SampleCabDatum> GetLongestTripDistances() 
        {
            var result = ctx.SampleCabData.OrderByDescending(x => x.TripDistance).Take(100).ToList(); 
            return result;
        }

        public List<SampleCabDatum> GetLongestTripDuration() 
        {
            var data = ctx.SampleCabData.ToList();  
            var result = data.OrderByDescending(x => (x.TpepDropoffDatetime - x.TpepPickupDatetime)).Take(100).ToList(); 
            return result;
        }

        public List<SampleCabDatum> Search(Func<int, bool> func) 
        {

            var result = ctx.SampleCabData.Where(x => func.Invoke((int)x.PUlocationId) == true);
            return result.ToList();
        }

        public void Update(SampleCabDatum data)
        {
            ctx.SampleCabData.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChangesAsync();
        }

        public void UpdateRange(List<SampleCabDatum> data) 
        {
            foreach(var v in data)
                ctx.SampleCabData.Entry(v).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChangesAsync();
        }

        public int GetEntriesCount() 
        {
            var result = ctx.SampleCabData.Count();
            return result;
        }
    }
}
