using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace guess_what2.Models
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
