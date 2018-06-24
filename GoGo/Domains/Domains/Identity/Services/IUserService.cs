﻿using Domains.Identity.Entities;
using Domains.Identity.Models;
using Groove.AspNetCore.Common.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Identity.Services
{
    public interface IUserService
    {
        Task<long> CreateUserAsync(UserCreateModel model, UserIdentity<long> issuer);
        Task<IEnumerable<UserListModel>> GetUsersAsync(long? id);
        Task<UserReadModel> GetUserProfileAsync(long? id);
        Task<UserReadModel> GetUserDetailAsync(long? id);
    }
}
