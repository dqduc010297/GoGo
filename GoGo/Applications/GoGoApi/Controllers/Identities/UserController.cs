﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("userroles")]
        [HttpGet]
        public async Task<IActionResult> GetUsers(long? id)
        {
            return Ok(await _userService.GetUsersAsync(id));
        }

        //[Route("")]
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> CreateUser([FromBody]UserCreateModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userId = await _userService.CreateUserAsync(model, GetCurrentIdentity<long>());

        //    return OkValueObject(userId);
        //}

        //[Route("{id}")]
        //[HttpPut]
        //[Authorize]
        //public async Task<IActionResult> UpdateUser(long id, [FromBody]UserUpdateModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await _userService.UpdateUserAsync(id, model, GetCurrentIdentity<long>());

        //    return Ok();
        //}

        [Route("get")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            //var result = await _userService.GetUserProfileAsync(GetCurrentUserId<long>());
            return Ok(await _userService.GetUserProfileAsync(GetCurrentUserId<long>()));
        }
    }
}