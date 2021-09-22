using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DistributionPortal.Data;
using DistributionPortal.Data.Entities;
using DistributionPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistributionPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IDPRepository distributionRepository;
        private readonly IHostingEnvironment env;
        private readonly UserManager<UserDetails> userManager;

        public UserProfileController(IDPRepository distributionRepository, IHostingEnvironment env, UserManager<UserDetails> userManager)
        {
            this.distributionRepository = distributionRepository;
            this.env = env;
            this.userManager = userManager;
        }

        // GET All user related details
        [HttpGet]
        [Route("getLoggedInUserDetails/{userID}")]
        public IActionResult GetLoggedInUserDetails(string userID)
        {
            try
            {
                return Ok(distributionRepository.GetLoggedInUserDetails(userID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update user related details
        [HttpPut]
        [Route("updateLoggedInUserDetails")]
        public IActionResult UpdateLoggedInUserDetails([FromBody] UserProfileViewModel profile)
        {
            try
            {
                return Ok(distributionRepository.UpdateLoggedInUserDetails(profile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}