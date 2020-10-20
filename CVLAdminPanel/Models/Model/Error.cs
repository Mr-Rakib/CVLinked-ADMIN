using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Error
    {
        [DisplayName("Error ID")]
        public string errorId { get; set; }
        [DisplayName("Title")] 
        public string title { get; set; }
        [DisplayName("Details")] 
        public string detail { get; set; }
        [DisplayName("Instance")] 
        public string instance { get; set; }
        [DisplayName("User Agent")] 
        public string userAgent { get; set; }
        [DisplayName("Relevent ID")] 
        public string releventId { get; set; }
        [DisplayName("IP Address")] 
        public string ipAddress { get; set; }
        [DisplayName("Error Time")] 
        public DateTime errorTime { get; set; }
    }
}
