using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.CheckSum.DTO;

namespace Fanex.Affiliate.DataService.CheckSum.ViewModel
{
    public class DailyMemberIncomeAccessViewModel
    {
        public string DateString { get; set; }
        public IList<DailyMemberIncomeAccess> Data { get; set; }
    }
}