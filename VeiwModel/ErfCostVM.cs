using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication20.Models;

namespace WebApplication20.VeiwModel
{
    public class  ErfCostVM
    {
        public long ErfCostID { get; set; }
        public long PlotTypeID { get; set; }
        public decimal CostPerSqm { get; set; }
        public decimal AdminFees { get; set; }

        public virtual Sys_PlotType Sys_PlotType { get; set; }
    }
}
