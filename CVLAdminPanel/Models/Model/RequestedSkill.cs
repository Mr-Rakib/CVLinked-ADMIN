using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Models.Model
{
    public class RequestedSkill
    {
        public int requestedSkillId { get; set; }
        public string skillType { get; set; }
        public string skillName { get; set; }
        public string details { get; set; }
        public bool approved { get; set; }
        public int approvedBy { get; set; }
        public string approvedByEmail { get; set; }
        public int requestorId { get; set; }
    }
}
