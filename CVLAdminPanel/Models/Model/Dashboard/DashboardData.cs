using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class DashboardData
    {
        public List<DashboardInfo> DashboardInformations { get; set; }
        public List<DashboardInfo> DashboardTodaysInformations { get; set; }
    }
}
