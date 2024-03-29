﻿using AutoMapper;
using DistributionPortal.Data.Entities;
using DistributionPortal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data
{
    public class DPRepository : IDPRepository
    {
        private readonly DPDBContext ctx;
        private readonly IHostingEnvironment env;
        private readonly IMapper mapper;
        private readonly UserManager<UserDetails> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DPRepository(DPDBContext ctx, IHostingEnvironment env, IMapper mapper, UserManager<UserDetails> userManager)
        {
            this.ctx = ctx;
            this.env = env;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public bool SaveAll()
        {
            try
            {
                return ctx.SaveChanges() > 0;
            }

            catch (Exception ex)
            {
                return false;
            }

        }

        public void AddEntity(object model)
        {
            this.ctx.Add(model);
        }

        public DateTime SriLankanDateTimeNow()
        {
            //get local cambodian time
            var info = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

            return localTime.DateTime;
        }

        public async Task<object> createUser(UserDetailsViewModel userdata)
        {
            try
            {
                var checkUsername = ctx.Users.Where(d => d.UserName == userdata.UserName).ToList();

                var newMail = ctx.Users.Where(d => d.Email == userdata.Email.Trim() && d.isDeleted == false && d.isActive == false).ToList();
                var checkMail1 = ctx.Users.Where(d => d.Email == userdata.Email.Trim() && d.isDeleted == true).ToList();
                var checkMail2 = ctx.Users.Where(d => d.Email == userdata.Email.Trim() && d.isDeleted == false && d.isActive == true).ToList();

                if ((newMail.Count == 0 && checkMail1.Count >= 0 && checkUsername.Count == 0) || (newMail.Count == 0 && checkMail2.Count >= 0 && checkUsername.Count == 0))
                {
                    var user = new UserDetails();

                    user.UserName = userdata.UserName;
                    user.Email = userdata.Email.Trim();
                    // user.Password = userdata.Password;
                    user.UserRole = userdata.roletype;
                    user.Customer1Name = userdata.customer_1;
                    user.Customer1Id = userdata.customer_1Id;
                    user.Customer2Name = userdata.customer_2;
                    user.Customer2Id = userdata.customer_2Id;
                    user.Name = userdata.profile.Name;
                    user.Surname = userdata.profile.Surname;
                    user.Birthday = userdata.profile.Birthday;
                    user.Gender = userdata.profile.Gender;
                    user.Image = userdata.profile.Image;
                    user.Company = userdata.work.Company;
                    user.Position = userdata.work.Position;
                    user.Phone = userdata.contacts.Phone;
                    user.Address = userdata.contacts.Address;
                    user.Facebook = userdata.social.Facebook;
                    user.isActive = userdata.settings.isActive;
                    user.isDeleted = userdata.settings.isDeleted;
                    user.CreatedBy = userdata.createdby;
                    user.CreatedTime = SriLankanDateTimeNow();
                    user.UpdatedBy = userdata.createdby;
                    user.UpdatedTime = SriLankanDateTimeNow();

                    var result2 = await userManager.CreateAsync(user, userdata.Password);
                    if (result2.Succeeded)
                    {
                        var userCreatedSuccess = new
                        {
                            IsSuccess = true,
                            ReturnMsg = "User created successfully"
                        };
                        return userCreatedSuccess;
                    }
                    else
                    {
                        var userCreatedFailed = new
                        {
                            IsSuccess = false,
                            ReturnMsg = "Username already taken"
                        };
                        return userCreatedFailed;
                    }
                }
                else
                {
                    var emailTaken = new
                    {
                        IsSuccess = false,
                        ReturnMsg = "Email already taken"
                    };
                    return emailTaken;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
