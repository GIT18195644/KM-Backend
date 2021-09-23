using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributionPortal.Data;
using DistributionPortal.Data.Entities;
using DistributionPortal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistributionPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IDPRepository distributionRepository;
        private readonly IHostingEnvironment env;
        private readonly UserManager<UserDetails> userManager;

        public UsersController(IDPRepository distributionRepository, IHostingEnvironment env, UserManager<UserDetails> userManager)
        {
            this.distributionRepository = distributionRepository;
            this.env = env;
            this.userManager = userManager;
        }

        // Create new user
        [HttpPost]
        [Route("createuser")]
        public IActionResult createUser([FromBody]UserDetailsViewModel userdata)
        {
            try
            {
                return Ok(distributionRepository.createUser(userdata));
            }
            catch (Exception ex)
            {
                var ret_results = new
                {
                    IsSuccess = false,
                    ReturnMsg = ex.Message
                };

                return Ok(ret_results);
            }
        }

        // GET All users
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(distributionRepository.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET User Roles
        [HttpGet]
        [Route("getAllUserRoles")]
        public IActionResult GetAllUserRoles()
        {
            try
            {
                return Ok(distributionRepository.GetAllUserRoles());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}