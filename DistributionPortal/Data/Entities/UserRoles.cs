using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data.Entities
{
    public class UserRoles : IdentityRole
    {
        [Column(TypeName = "VARCHAR(100)")]
        public string RoleId { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string RoleDescription { get; set; }
        [Column(TypeName = "int")]
        public int RoleType { get; set; }
        [Column(TypeName = "int")]
        public int RoleStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

    }
}
