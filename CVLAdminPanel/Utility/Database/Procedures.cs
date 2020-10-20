using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Utility.Database
{
    public static class Procedures
    {
        public static string GetAllPersonalInformation          = "SP_GetAllPersonalInformation";
        public static string GetPersonalInformationByLoginId    = "SP_GetPersonalInformationByLoginId";
        public static string SavePersonalInformation            = "SP_SavePersonalInformation";
        public static string UpdatePersonalInformation          = "SP_UpdatePersonalInformation";
        public static string DeletePersonalInformation          = "SP_DeletePersonalInformation ";
    }
}
