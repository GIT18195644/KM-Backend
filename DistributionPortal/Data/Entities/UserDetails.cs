using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data.Entities
{
    public class UserDetails : IdentityUser
    {
        [Column(TypeName = "VARCHAR(50)")]
        public string UserCode { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string Salutation { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string ContactEmail { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string ZipCode { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string Country { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string State { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string UserRole { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Customer1Id { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Customer1Name { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string Customer2Id { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Customer2Name { get; set; }
        public string Password { get; set; }
        public string ContentType { get; set; }
        public byte[] ProfilePicture { get; set; }

        public string Notifications { get; set; }
        public string DistributorCode { get; set; }
        public DateTime LoginTime { get; set; }

        public bool UserStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool PasswordLock { get; set; }
        public string RejectedReason { get; set; }
        public string ApprovalReason { get; set; }

        public bool LoggedStatus { get; set; }
        public int CustomerInseeStatus { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        public long LoginCount { get; set; }


    }
}
