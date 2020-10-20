using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class BackOfficeUser: LoginInfo
    {
        public PersonalInformation personalInformation { get; set; }
    }
}
