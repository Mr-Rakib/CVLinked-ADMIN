using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class PackageSubscriptionHistory
    {
        public int subscriptionId { get; set; }
        public int corporateId { get; set; }
        public int packageId { get; set; }
        public string packageName { get; set; }
        public double packagePrice { get; set; }
        public int cvDownloads { get; set; }
        public int remainingCVDownloads { get; set; }
        public int jobAnnouncements { get; set; }
        public int remainingJobAnnouncements { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int validity { get; set; }
        public bool cvBrowsing { get; set; }
        public bool skillTest { get; set; }
        public bool examTest { get; set; }
        public int cvDownloaded { get; set; }
        public int jobAnnounced { get; set; }
    }
}
