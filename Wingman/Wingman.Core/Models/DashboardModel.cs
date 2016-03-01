using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingman.Core.Models
{
    public class DashboardModel
    {
        public IEnumerable<SubmissionModel> RecentlyPickedSubmissions { get; set; }
        public IEnumerable<WingmanUserModel> TopWingmen { get; set; }
    }
}
