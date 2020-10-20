using Nancy.Bootstrapper;
using System;
using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class LoginInfo
    {
        [DisplayName("Login ID")]
        public int loginId { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Phone Number")]
        public string phoneNo { get; set; }
        [DisplayName("Active Status")]
        public bool isActive { get; set; }
        public Role role { get; set; }
        [DisplayName("Last Login Date")]
        public DateTime? lastLoginDate { get; set; }
        [DisplayName("Confirmed Email")]
        public bool confirmedEmail { get; set; }
    }
}