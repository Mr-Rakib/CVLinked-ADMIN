using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models
{
    public class Logs
    {
        public DateTime date { get; set; }
        public string fileName { get; set; }
        public string methodsName { get; set; }
        public int errorLineNo { get; set; }
        public int columnNumber { get; set; }
        public string errorMessage { get; set; }
        public string exceptiontype { get; set; }
        public string errorMessageDescriptions { get; set; }
    }
}
