using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class CVSummaryCollection
    {
        public List<CV> CVs { get; set; }
        public int totalCount { get; set; }
        public int rowsToSkip { get; set; }
        public int rowsToGet { get; set; }
        public int pageNumber { get; set; }
    }
}
