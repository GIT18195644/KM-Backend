using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data.Entities
{
    public class SharePoint
    {
        [Key]
        //[Column(TypeName = "VARCHAR(50)")]
        public long SPId { get; set; }
        [Column(TypeName = "VARCHAR(1000)")]
        public string Topic { get; set; }
        [Column(TypeName = "VARCHAR(256)")]
        public string RoleName { get; set; }
        [Column(TypeName = "VARCHAR(5000)")]
        public string Abstract { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
