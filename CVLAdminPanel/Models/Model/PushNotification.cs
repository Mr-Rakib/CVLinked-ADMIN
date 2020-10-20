using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class PushNotification
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Recipient { get; set; }

    }
}
