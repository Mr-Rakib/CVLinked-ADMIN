using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class Role
    {
        [DisplayName("Role ID")]
        public int roleId { get; set; }
        [DisplayName("Role Name")]
        public string roleName { get; set; }
    }
}