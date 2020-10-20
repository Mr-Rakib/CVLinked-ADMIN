using System;
using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class PersonalInformation
    {
        [DisplayName("Login ID")]
        public int loginId { get; set; }
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [DisplayName("Gender")]
        public string gender { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? dateOfBirth { get; set; }
        [DisplayName("Address")]
        public string address{ get; set; }
    }
}  