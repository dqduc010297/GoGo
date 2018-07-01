﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Identity.Models;
using Domains.Identity.Services;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.Identities
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("list")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser([FromBody]UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.CreateUserAsync(model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }

        [Route("edit")]
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody]UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.UpdateUserAsync(id, model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }

        //Get the value of user need to update
        [Route("edit")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ViewUserUpdate(long id)
        {
            return Ok(await _userService.GetUserUpdateAsync(id));
        }

        [Route("profile/edit")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile(long id, [FromBody]UserProfileUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.UpdateUserProfileAsync(id, model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }

        [Route("profile/edit")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewUserProfileUpdate(long id)
        {
            return Ok(await _userService.GetUserUpdateAsync(id));
        }

        [Route("profile")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            //var result = await _userService.GetUserProfileAsync(GetCurrentUserId<long>());
            return Ok(await _userService.GetUserProfileAsync(GetCurrentUserId<long>()));
        }

        [Route("detail")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserDetail(long? id)
        {
            return Ok(await _userService.GetUserDetailAsync(id));
        }
    }
}