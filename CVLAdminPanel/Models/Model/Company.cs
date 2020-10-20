using CVLAdminPanel.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Company
    {

        [DisplayName("Employee ID")]
        public int employerId { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Confirmed Email")]
        public bool confirmedEmail { get; set; }

        [DisplayName("Registration Date")]
        public DateTime registeredDate { get; set; }

        [DisplayName("Last Login Date")]
        public DateTime? lastLoginDate { get; set; }

        [DisplayName("Registered Phone Number")]
        public string registeredPhoneNo { get; set; }

        [DisplayName("Company Name")]
        public string companyName { get; set; }

        [DisplayName("Company Contact Number")]
        public string companyContactNo { get; set; }

        [DisplayName("Support Email")]
        public string supportEmail { get; set; }

        [DisplayName("Company Logo")]
        public string companyLogoName { get; set; }

        [DisplayName("Contacat Person")]
        public ContactPerson contactPerson { get; set; }
    }
}
