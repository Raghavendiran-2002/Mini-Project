﻿using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.UserProfile;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class UserProfileRepositoy : BaseRepository<int, User>, IRepository<int, User>
    {
        protected readonly DBPharmacyContext _context;
        public UserProfileRepositoy(DBPharmacyContext context) : base(context)
        {
               _context = context;
        }
        public async override Task<User> Get(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserID == key);
            if(user == null)
            {
                throw new NoUserFound($"User not found with Id : {key}");
            }
            return user;
        }
    }
}