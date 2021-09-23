using DistributionPortal.Data.Entities;
using DistributionPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data
{
    public interface IDPRepository
    {
        Task<object> createUser(UserDetailsViewModel userdata);
        string GetLoggedInUserDetails(string userID);
        object UpdateLoggedInUserDetails(UserProfileViewModel profile);
        string GetAllUserRoles();
        string GetAllUsers();

    }
}
