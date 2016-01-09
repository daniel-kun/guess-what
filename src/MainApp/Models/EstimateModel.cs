using System.Collections.Generic;

namespace Io.GuessWhat.MainApp.Models
{
    public class EstimateModel
    {
        public int estimate
        {
            get;
            set;
        }

        public List<EstimateAspectItem> preconditions
        {
            get;
            set;
        }
    }
}
