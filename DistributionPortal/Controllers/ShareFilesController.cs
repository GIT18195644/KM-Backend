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
    public class ShareFilesController : ControllerBase
    {
        private readonly IDPRepository distributionRepository;
        private readonly IHostingEnvironment env;
        private readonly UserManager<UserDetails> userManager;

        public ShareFilesController(IDPRepository distributionRepository, IHostingEnvironment env, UserManager<UserDetails> userManager)
        {
            this.distributionRepository = distributionRepository;
            this.env = env;
            this.userManager = userManager;
        }

        // Create shared file
        [HttpPost]
        [Route("createFile")]
        public IActionResult CreateFile([FromBody] ShareFilesViewModel file)
        {
            try
            {
                return Ok(distributionRepository.CreateFile(file));
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

        // Get All Shared Files
        [HttpGet]
        [Route("getAllSharedFiles/{user}")]
        public IActionResult GetAllSharedFiles(string user)
        {
            try
            {
                return Ok(distributionRepository.GetAllSharedFiles(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}