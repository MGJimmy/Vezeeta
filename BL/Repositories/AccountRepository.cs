using BL.Bases;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class AccountRepository:BaseRepository<ApplicationUserIdentity>
    {
        private UserManager<ApplicationUserIdentity> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountRepository(DbContext db, 
            UserManager<ApplicationUserIdentity> userManager,
            RoleManager<IdentityRole> roleManager):base(db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> Register(ApplicationUserIdentity registerUser)
        {
            var identityResult = await _userManager.CreateAsync(registerUser);
            return identityResult;
        }
        public ApplicationUserIdentity GetAccountById(string id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }
        public async Task<ApplicationUserIdentity> FindByName(string userName)
        {
            ApplicationUserIdentity result = await _userManager.FindByNameAsync(userName);
            return result;
        }
        public async Task<ApplicationUserIdentity> FindByEmail(string email)
        {
            ApplicationUserIdentity result = await _userManager.FindByEmailAsync(email);
            return result;
        }
        public async Task<IEnumerable<string>> GetUserRoles(ApplicationUserIdentity user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles;
        }

        public async Task<ApplicationUserIdentity> FindById(string id)
        {
            ApplicationUserIdentity result = await _userManager.FindByIdAsync(id);
            return result;
        }
        public async Task<ApplicationUserIdentity> Find(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }
        public async Task<IdentityResult> AssignToRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _roleManager.RoleExistsAsync(roleName))
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, roleName);
                return result;
            }
            return null;

        }
        public async Task<bool> updatePassword(ApplicationUserIdentity user)
        {
            _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            IdentityResult result = await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UpdateAccount(ApplicationUserIdentity user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);
            return true;
        }
    }
}
