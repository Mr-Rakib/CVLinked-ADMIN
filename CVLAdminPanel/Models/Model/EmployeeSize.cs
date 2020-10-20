using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class EmployeeSize
    {
        public int employeeSizeId { get; set; }
        [DisplayName("Employee Size")]
        public string employeeSizeName { get; set; }
        public bool isActive { get; set; }
    }
}