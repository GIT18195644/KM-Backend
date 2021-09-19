using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.ViewModels
{
    public class LoginViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Customer1Id { get; set; }
        public string Customer1Name { get; set; }
        public string Customer2Id { get; set; }
        public string Customer2Name { get; set; }
        public string UserRole { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool AccountStatus { get; set; }

        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }

    }
}