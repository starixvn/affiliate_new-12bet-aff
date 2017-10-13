using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.CheckSum.DTO;

namespace Fanex.Affiliate.DataService.CheckSum.ViewModel
{
    public class DailyMemberIncomeAccessViewModel : BaseViewModel
    {
        public IEnumerable<DailyMemberIncomeAccess> Data { get; set; }
    }
}