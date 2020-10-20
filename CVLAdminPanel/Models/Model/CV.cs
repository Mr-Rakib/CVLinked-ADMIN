using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class CV
    {
        public int LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool Status { get; set; }
        public bool Verified { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public string VerificationStatusText { get; set; }
        public bool ConfirmedEmail { get; set; }
        public int ProfilePercent { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
