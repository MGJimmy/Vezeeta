using BL.Bases;
using BL.DTOs.AccountDTO;
using BL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        private IMailService _mailService;

        public AccountRepository(DbContext db, 
            UserManager<ApplicationUserIdentity> userManager, IMailService mailService,
            RoleManager<IdentityRole> roleManager):base(db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }
        public async Task<IdentityResult> Register(ApplicationUserIdentity registerUser)
        {
            var identityResult = await _userManager.CreateAsync(registerUser,registerUser.PasswordHash);
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


        public async Task<bool> forgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"http://localhost:4200/resetForgetPasswod?email={email}&token={validToken}";
            
            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                    $"<p>To reset your password <a href='{url}'>Click here</a></p>");
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            
            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);

            if (result.Succeeded)
                return true;

            return false;
        }
    }
}
