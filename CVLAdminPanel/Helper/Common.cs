using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CallCenterPanel.Helper
{
    public static class Common
    {
        public static string RemoveCountryCode(string phonenumber)
        {
            List<string> countries_list = new List<string>();
            countries_list.Add("+88");
            countries_list.Add("88");

            foreach (var country in countries_list)
            {
                phonenumber = phonenumber.Replace(country, "");
            }

            return phonenumber;
        }
    }

}