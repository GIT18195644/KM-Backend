using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.ViewModels
{
    public class UserDetailsViewModel
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string roletype { get; set; }
        public string Email { get; set; }
        public string customer_1 { get; set; }
        public string customer_1Id { get; set; }
        public string customer_2 { get; set; }
        public string customer_2Id { get; set; }
        public string createdby { get; set; }
        public ProfileViewModel profile { get; set; } 
        public WorkViewModel work { get; set; }
        public ContactsViewModel contacts { get; set; }
        public SocialViewModel social { get; set; }
        public SettingsViewModel settings { get; set; }
        //public bool UserStatus { get; set; }

    }
}
