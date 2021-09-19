using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DistributionPortal.Data;
using DistributionPortal.Data.Entities;
using DistributionPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace DistributionPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IDPRepository distributorRepository;
        private readonly IMapper mapper;
        private readonly UserManager<UserDetails> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly SignInManager<UserDetails> signInManager;
        private readonly IHostingEnvironment hostingEnvironment;

        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController(IDPRepository distributorRepository, IMapper mapper, UserManager<UserDetails> userManager, RoleManager<IdentityRole> roleManager, IHostingEnvironment environment, ILogger<AccountController> logger, IConfiguration config, SignInManager<UserDetails> signInManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.distributorRepository = distributorRepository;
            this.hostingEnvironment = environment;
            this.config = config;
            this.signInManager = signInManager;
        }

        //dont use Authorize tag
        public IActionResult Login()
        {
            if (Request.Query.Keys.Contains("ReturnUrl"))
            {
                return Redirect(Request.Query["ReturnUrl"].First());
            }
            else
            {
                return RedirectToAction("Login", "Index");
            }
        }


        [HttpPost]
        [Route("createUser")]
        public IActionResult createUser([FromBody]UserDetailsViewModel user)
        {
            try
            {
                return Ok(distributorRepository.createUser(user));
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


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(loginmodel.UserName);
                    if (user != null && !user.isDeleted && !user.isActive)
                    {
                        var result = await signInManager.CheckPasswordSignInAsync(user, loginmodel.Password, false);
                        if (result.Succeeded)
                        {
                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            var _options = new IdentityOptions();
                            var claims = new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                            var token = new JwtSecurityToken(
                            config["Tokens:Issuer"],
                            config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(8),
                            signingCredentials: credentials
                            );

                            var results = new LoginViewModel
                            {
                                Token = new JwtSecurityTokenHandler().WriteToken(token),
                                TokenExpiration = token.ValidTo,
                                UserRole = user.UserRole,
                                UserId = user.UserName,
                                Customer1Id = user.Customer1Id,
                                Customer1Name = user.Customer1Name,
                                Customer2Id = user.Customer2Id,
                                Customer2Name = user.Customer2Name
                            };
                             return Created("", results);
                        }
                        else
                        {
                            return Unauthorized();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok(true);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

    }
}