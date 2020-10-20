using System.ComponentModel;

namespace CVLAdminPanel.Models.Model
{
    public class Country
    {
        [DisplayName("Country Name")]
        public string countryName { get; set; }
        [DisplayName("Country ID")]
        public int countryId{ get; set; }
        [DisplayName("Printable Name")]
        public string printableName { get; set; }
        [DisplayName("Phone Code")]
        public string phoneCode{ get; set; }
    }
}