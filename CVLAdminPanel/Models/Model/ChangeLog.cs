using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class ChangeLog
    {
        public Guid ChangeLogId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Object { get; set; }
        public string ChangeType { get; set; }
        public bool Approved { get; set; }
        public string Description { get; set; }
        public List<ChangeLogDescriptions> ChangeLogDescriptions { get; set; }
    }
}
