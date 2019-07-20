using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace MeetingManagementSystem.Repositories
{
    public class SecUserRoleRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();

        public int Insert(SecUserRole secUserRole)
        {
            

            context.SecUserRoles.Add(secUserRole);

            return context.SaveChanges();
        }
        public int Edit(SecUserRole secUserRole)
        {
            var userRole = context.SecUserRoles.FirstOrDefault(r => r.SecUserRoleId == secUserRole.SecUserRoleId);
            if (userRole != null)
            {
                userRole.SecRoleId = secUserRole.SecRoleId;
                userRole.ModifiedBy = secUserRole.ModifiedBy;
                userRole.ModifiedDate = secUserRole.ModifiedDate;
                context.Entry(userRole).State = System.Data.Entity.EntityState.Modified;
            }

            return context.SaveChanges();
        }
        public int Delete(int secUserRoleId)
        {
            var secUserRole = context.SecUserRoles.FirstOrDefault(s => s.SecUserRoleId == secUserRoleId);
            context.Entry(secUserRole).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }
        public SecUserRole Get(int id)
        {
            return context.SecUserRoles.SingleOrDefault(s => s.SecUserRoleId == id);
        }
        public List<SecUserRole> GetAll()
        {
            return context.SecUserRoles.ToList();
        }
    }
}