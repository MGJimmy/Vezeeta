using AutoMapper;

using BL.Bases;
using BL.DTOs.AccountDTO;
using BL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class AccountAppService : BaseAppService
    {
        IConfiguration _configuration;
        public AccountAppService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : base(unitOfWork, mapper)
        {
            this._configuration = configuration;
        }
        public async Task<ApplicationUserIdentity> Register(RegisterAccountDTO registerAccountDTO)
        {
            ApplicationUserIdentity registerUser = Mapper.Map<ApplicationUserIdentity>(registerAccountDTO);
            await TheUnitOfWork.AccountRepo.Register(registerUser);
            TheUnitOfWork.SaveChanges();

            return registerUser;
        }
        public async Task<IdentityResult> AssignToRole(string userid, string rolename)
        {
            if (userid == null || rolename == null)
                return null;
            return await TheUnitOfWork.AccountRepo.AssignToRole(userid, rolename);
        }
        public async Task<bool> checkUsernameExist(string userName)
        {
            var user = await TheUnitOfWork.AccountRepo.FindByName(userName);
            return user == null ? false : true;
        }
        public async Task<bool> checkEmailExist(string email)
        {
            var user = await TheUnitOfWork.AccountRepo.FindByEmail(email);
            return user == null ? false : true;
        }
        public async Task<ApplicationUserIdentity> Find(string name, string password)
        {
            ApplicationUserIdentity user = await TheUnitOfWork.AccountRepo.Find(name, password);

            if (user != null)
                return user;
            return null;
        }
        public async Task<IEnumerable<string>> GetUserRoles(ApplicationUserIdentity user)
        {
            return await TheUnitOfWork.AccountRepo.GetUserRoles(user);
        }
        public async Task<ApplicationUserIdentity> GetUserById(string userId)
        {
            return await TheUnitOfWork.AccountRepo.FindById(userId);
        }

        public async Task<dynamic> CreateToken(ApplicationUserIdentity user)
        {

            var userRoles = await GetUserRoles(user);
            string role = "No Role";
            if (userRoles.FirstOrDefault() != null)
            {
                role = userRoles.FirstOrDefault();
            }

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                   new Claim(ClaimTypes.Role,role),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            //foreach (var userRole in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };


        }

        public async Task<UpdateUserDto> GetUserByIdForUpdate(string userId)
        {
            var userDto = new UpdateUserDto();
            var user = await TheUnitOfWork.AccountRepo.FindById(userId);
            userDto.FullName = user.FullName;
            userDto.Email = user.Email;
            userDto.PhoneNumber = user.PhoneNumber;
            userDto.isDoctor = user.IsDoctor;

            if (user.IsDoctor)
            {
                var doctor = TheUnitOfWork.DoctorRepo.GetById(userId);
                userDto.TitleDgree = doctor.TitleDegree;
                userDto.DoctorInfo = doctor.doctorInfo;
                userDto.SpecialtyId = doctor.specialtyId;
            }

            return userDto;

        }

        public async Task<UpdateUserDto> UpdateAccount(string userid, UpdateUserDto model)
        {
            var user = await TheUnitOfWork.AccountRepo.FindById(userid);
            if (user != null)
            {
                if (user.IsDoctor)
                {
                    var doctor = TheUnitOfWork.DoctorRepo.GetById(userid);

                    doctor.doctorInfo = model.DoctorInfo;
                    doctor.TitleDegree = model.TitleDgree;
                    doctor.specialtyId = model.SpecialtyId ?? doctor.specialtyId;
                    var result = TheUnitOfWork.SaveChanges();
                    if (result < new int())
                        return null;
                }


                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                await TheUnitOfWork.AccountRepo.UpdateAccount(user);
                return model;
            }
            return null;

        }



        public async Task<bool> ForgetPassword(string email)
        {
           return await TheUnitOfWork.AccountRepo.forgetPasswordAsync(email);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO model)
        {
            var result= await TheUnitOfWork.AccountRepo.ResetPasswordAsync(model);
            TheUnitOfWork.SaveChanges();
            return result;
        }

    }
}
