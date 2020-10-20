using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class Referral
    {
        public int referralId { get; set; }
        [DisplayName("Referral By")]
        public string referralName { get; set; }
        public bool isActive { get; set; }
    }
}