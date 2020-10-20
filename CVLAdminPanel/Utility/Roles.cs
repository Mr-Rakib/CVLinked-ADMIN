using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Utility
{
    public static class Roles
    {
        public const string Admin                   = "admin";
        public const string Supervisor              = "supervisor";
        public const string Salesman                = "salesman";
        public const string CallCenterAgent         = "callcenteragent";
        public const string Developer               = "developer";
        public const string Employer                = "employer";
        public const string JobSeeker               = "jobseeker";

        public const string ALLUSER                 = Admin + "," + Supervisor + "," + Salesman + "," + CallCenterAgent;
        public const string CallCenterAgentAndAbove = Admin + "," + Supervisor + "," + "," + CallCenterAgent;
        public const string SalesmanAndAbove        = Admin + "," + Supervisor + "," + Salesman;
        public const string SupervisorAndAbove      = Admin + "," + Supervisor;

        public static List<string> AllUsers                     = new List<string>() { Admin, Supervisor, Salesman, CallCenterAgent };
        public static List<string> SuperUsers                   = new List<string>() { Admin, Supervisor };
        public static List<string> SalesmanAndAboveUser         = new List<string>() { Admin, Supervisor, Salesman };
        public static List<string> CallCenterAgentAndAboveUser  = new List<string>() { Admin, Supervisor, CallCenterAgent };

        public static List<string> RestictedAppUser             = new List<string>() { Employer, JobSeeker };
    }
}
