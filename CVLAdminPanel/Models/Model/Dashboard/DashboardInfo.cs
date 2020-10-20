using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class DashboardInfo
    {
        public int count { get; set; }
        public int confirmed { get; set; }
        public int unconfirmed { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
