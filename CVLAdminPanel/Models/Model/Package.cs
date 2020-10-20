using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Package
    {
        [DisplayName("Package ID")]
        public int packageId { get; set; }

        [DisplayName("Name")]
        public string packageName { get; set; }

        [DisplayName("Price")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float packagePrice { get; set; }

        [DisplayName("Package Details")]
        public string packageDetail { get; set; }

        [DisplayName("CV Browsing?")]
        public bool cvBrowsing { get; set; }

        [DisplayName("Skill Test")]
        public bool skillTest { get; set; }

        [DisplayName("Exam Test")]
        public bool examTest { get; set; }

        [DisplayName("Recommended")]
        public bool recommended { get; set; }

        [DisplayName("Most Popular")]
        public bool mostPopular { get; set; }

        [DisplayName("CV Downloads")]
        public int cvDownloads { get; set; }

        [DisplayName("Job Announcements")]
        public int jobAnnouncements { get; set; }

        [DisplayName("Wallet")]
        public bool wallet { get; set; }

        [DisplayName("Validity")]
        public int validity { get; set; }

        [DisplayName("Is Active?")]
        public bool isActive { get; set; }

        [DisplayName("Is Promotional?")]
        public bool isPromotional { get; set; }

        [DisplayName("Promotion")]
        public string promotion { get; set; }

    }
}
