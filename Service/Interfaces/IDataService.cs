using Service.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDataService
    {
        float GetHighestAvgTip();
        List<SampleCabDatum> GetLongestTripDistances();
        List<SampleCabDatum> GetLongestTripDuration();
        List<SampleCabDatum> Search(Func<int, bool> func); 
        void Update(SampleCabDatum data);
        void UpdateRange(List<SampleCabDatum> data);
        int GetEntriesCount();
    }
}
