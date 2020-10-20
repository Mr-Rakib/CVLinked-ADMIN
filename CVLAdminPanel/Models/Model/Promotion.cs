using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float PackagePrice { get; set; }
        public int CvDownloads { get; set; }
        public int JobAnnouncements { get; set; }
        public int Validity { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public bool active { get; set; }
    }
}
