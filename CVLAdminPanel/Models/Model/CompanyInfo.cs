using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class CompanyInfo
    {
        [DisplayName("Company Name")]
        public string companyName { get; set; }
        [DisplayName("Address")]
        public string address { get; set; }
        [DisplayName("Billing Address")]
        public string billingAddress { get; set; }
        [DisplayName("Company Contact Number")]
        public string companyContactNo { get; set; }
        [DisplayName("Company TIN")]
        public string companyTin { get; set; }
        [DisplayName("Support Email")]
        public string supportEmail { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [DisplayName("Company Logo")]
        public string companyLogoName { get; set; }
        public ContactPerson contactPerson { get; set; }
        public BusinessCategory businessCategory { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        [DisplayName("Zip Code")]
        public string zipCode { get; set; }
        [DisplayName("Establish Year")]
        public int establishmentYear { get; set; }
        public EmployeeSize employeeSize { get; set; }
        [DisplayName("Website Url")]
        public string websiteUrl { get; set; }
    }
}
