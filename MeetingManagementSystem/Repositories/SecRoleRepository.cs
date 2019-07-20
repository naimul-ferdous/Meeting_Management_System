using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class SecRoleRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();

        public int Insert(SecRole secRole)
        {
            context.SecRoles.Add(secRole);
            int Return= context.SaveChanges();
            if (Return == 1) { 
            List<SecResource> secResources = context.SecResources.ToList();
            foreach (SecResource resource in secResources)
            {
                SecResourcePermission secResourcePermission = new SecResourcePermission
                {
                    SecRoleId = secRole.SecRoleId,
                    SecResourceId = resource.SecResourceId,
                    FileName = resource.FileName,
                    MenuName = resource.MenuName,
                    DisplayName = resource.DisplayName,
                    ModuleId = resource.ModuleId,
                    Order = resource.Order,
                    Level = resource.Level,
                    ActionUrl = resource.ActionUrl,
                    Status = resource.Status
                };
                context.SecResourcePermissions.Add(secResourcePermission);
                //inserting data in SecRolePermission Table
                SecRolePermission secRolePermission = new SecRolePermission
                {
                    SecResourceId = resource.SecResourceId,
                    SecRoleId = secRole.SecRoleId,
                    Add = true,
                    Edit = true,
                    Delete = true
                };
                context.SecRolePermissions.Add(secRolePermission);
                context.SaveChanges();
            }
          }
            return Return;
        }
        public int Edit(SecRole secRole)
        {
            var role = context.SecRoles.FirstOrDefault(r => r.SecRoleId == secRole.SecRoleId);
            role.SecRoleId = secRole.SecRoleId;
            role.RoleName = secRole.RoleName;
            role.Status = secRole.Status;
            
            role.ModifiedBy = 2;
            role.ModifiedDate = DateTime.Now;
            context.Entry(role).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int secRoleId)
        {
            var secRole = context.SecRoles.FirstOrDefault(s => s.SecRoleId == secRoleId);
            context.Entry(secRole).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }
        public SecRole Get(int id)
        {
            return context.SecRoles.SingleOrDefault(s => s.SecRoleId == id);
        }
        public List<SecRole> GetAll()
        {
            return context.SecRoles.ToList();
        }
    }
}