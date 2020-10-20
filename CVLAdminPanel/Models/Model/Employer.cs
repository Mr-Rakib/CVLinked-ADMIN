using CVLAdminPanel.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Employer : EmployerAndPackages
    {
        [DisplayName("Employee ID")]
        public int employerId { get; set; }
        [DisplayName("Email")]
        public string email{ get; set; }
        [DisplayName("Confirmed Email")]
        public bool confirmedEmail{ get; set; }
        [DisplayName("Registered Date")]
        public DateTime registeredDate { get; set; }
        [DisplayName("Last Login Date")]
        public DateTime? lastLoginDate { get; set; }
        [DisplayName("Access Failed Count")]
        public int accessFailedCount { get; set; }
        [DisplayName("Registered Phone No")]
        public string registeredPhoneNo { get; set; }
        [DisplayName("Referral")]
        public Referral referral{ get; set; }
        [DisplayName("Company Information")]
        public CompanyInfo companyInfo { get; set; }

    }
}
