using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Notification
    {
        public int notificationId { get; set; }
        public string notificationType { get; set; }
        public int releventId { get; set; }
        public bool isRead { get; set; }
        public string imagePath { get; set; }
        public DateTime createDate { get; set; }
        public string text { get; set; }
        public string idType { get; set; }
    }
}
