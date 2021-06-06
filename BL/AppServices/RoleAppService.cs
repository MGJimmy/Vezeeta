using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Identity;
using BL.Interfaces;
using AutoMapper;
using BL.DTOs.RoleDTO;

namespace BL.AppServices
{
    public class RoleAppService : BaseAppService
    {
        public RoleAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public async Task CreateRoles()
        {
            await TheUnitOfWork.RoleRepo.CreateRoles();
            TheUnitOfWork.SaveChanges();
            TheUnitOfWork.CommitTransaction();
        }
        public RoleDTO GetRoleById(string id)
        {
            if (id == null || id == "")
                throw new ArgumentNullException();

            return Mapper.Map<RoleDTO>(TheUnitOfWork.RoleRepo.GetRoleByID(id));
        }
        public IdentityResult Create(string rolename)
        {
            return TheUnitOfWork.RoleRepo.Create(rolename);
        }
        public async Task <IdentityResult> Update(RoleDTO roleViewModel)
        {
            if (roleViewModel == null)
                throw new ArgumentNullException();
            if (roleViewModel.Id == null || roleViewModel.Id == string.Empty)
                throw new ArgumentException();

            var role = Mapper.Map<IdentityRole>(roleViewModel);
            return await TheUnitOfWork.RoleRepo.UpdateRole(role);
        }
        //public List<RoleDTO> GetAllRoles()
        //{
        //    return Mapper.Map<List<RoleDTO>>(TheUnitOfWork.Role.getAllRoles());
        //}
        public bool DeleteRole(string id)
        {
            if (id == null || id == "")
                throw new ArgumentNullException();
            bool result = false;

            TheUnitOfWork.RoleRepo.DeleteRole(id);
            result = TheUnitOfWork.SaveChanges() > new int();

            return result;
        }
        //public List<RegisterViewodel> getAllUsers(string id)
        //{
        //    List<ApplicationUserIdentity> users = new List<ApplicationUserIdentity>();
        //   var role= TheUnitOfWork.Role.getAllRoles().FirstOrDefault(r => r.Id == id);
        //    //role.users is about table relation called usersRoles in Db

        //    foreach(var userRole in role.Users)
        //    {
        //        var user = TheUnitOfWork.Account.GetAccountById(userRole.UserId);
        //         if(user.isDeleted == false)
        //              users.Add(user);
        //    }
                
        //    return Mapper.Map<List<RegisterViewodel>>(users);
        //}
    }
}
