using BL.Bases;
using BL.StaticClasses;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole>
    {
        RoleManager<IdentityRole> manager;
     
        public RoleRepository(DbContext db, RoleManager<IdentityRole> manager) :base(db)
        {
            this.manager = manager;
         
         
        }
        public IdentityRole GetRoleByID(string id)
        {
            return GetFirstOrDefault(r => r.Id == id);
        }

        public async Task CreateRoles()
        {
            if (!await manager.RoleExistsAsync(UserRoles.Admin))
            {
                var x = await manager.CreateAsync(new IdentityRole { Name = UserRoles.Admin });
            }
            if (!await manager.RoleExistsAsync(UserRoles.Doctor))
            {
                var x = await manager.CreateAsync(new IdentityRole(UserRoles.Doctor));
            }
            if (!await manager.RoleExistsAsync(UserRoles.Patient))
            {
                var x = await manager.CreateAsync(new IdentityRole(UserRoles.Patient));
            }

        }
        public IdentityResult Create(string role)
        {
            return manager.CreateAsync(new IdentityRole(role)).Result;
           
        }
        public async Task<IdentityResult> UpdateRole(IdentityRole role)
        {
            var identityRole = await manager.FindByIdAsync(role.Id);
            identityRole.Name = role.Name;
           return await manager.UpdateAsync(identityRole);

     
        }
        public async void DeleteRole(string id)
        {
            var identityRole = await manager.FindByIdAsync(id);
       
            await manager.DeleteAsync(identityRole);
        }
        public List<IdentityRole> getAllRoles()
        {
            return GetAll().ToList();
        }

    }
}
