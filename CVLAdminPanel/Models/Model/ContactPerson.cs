using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class ContactPerson
    {
        [DisplayName("Name")]
        public string contactPersonName { get; set; }

        [DisplayName("Designation")]
        public string contactPersonDesignation { get; set; }

        [DisplayName("Mobile Number")]
        public string contactPersonMobile { get; set; }
    }
}