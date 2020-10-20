using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class RequestLog
    {
        [DisplayName("Instance")]
        public string instance { get; set; }
        [DisplayName("Client's IP")]
        public string clientIP { get; set; }
        [DisplayName("Elapsed Time")]
        public string elapsedTime { get; set; }
        [DisplayName("Log Time")]
        public DateTime logTime { get; set; }
        [DisplayName("Response Status")]
        public string responseStatus { get; set; }
    }
}
