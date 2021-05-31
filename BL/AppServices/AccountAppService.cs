using AutoMapper;
using BL.Bases;
using BL.DTOs.AccountDTO;
using BL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class AccountAppService:BaseAppService
    {
        public AccountAppService(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public async Task<ApplicationUserIdentity> Register(RegisterAccountDTO registerAccountDTO)
        {
            ApplicationUserIdentity registerUser = Mapper.Map<ApplicationUserIdentity>(registerAccountDTO);
            await TheUnitOfWork.AccountRepo.Register(registerUser);
            TheUnitOfWork.SaveChanges();

            return registerUser;
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

    }
}
