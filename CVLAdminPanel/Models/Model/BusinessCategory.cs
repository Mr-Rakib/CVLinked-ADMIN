using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class BusinessCategory
    {
        [DisplayName("Business Category ID")]
        public int businessCategoryId { get; set; }
        [DisplayName("Company Type")]
        public string businessCategoryName { get; set; }
        [DisplayName("Active Status")]
        public bool isActive { get; set; }
    }
}