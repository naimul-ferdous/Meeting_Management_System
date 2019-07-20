using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class SecResourcePermissionRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();

        public int Insert(SecResourcePermission secResourcePermission)
        {
            context.SecResourcePermissions.Add(secResourcePermission);
            return context.SaveChanges();
        }
        public int Edit(SecResourcePermission secResourcePermission)
        {
            context.Entry(secResourcePermission).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int secResourcePermissionId)
        {
            var secResourcePermission = context.SecResourcePermissions.FirstOrDefault(s => s.SecResourcePermissionId == secResourcePermissionId);
            context.Entry(secResourcePermission).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }
        public SecResourcePermission Get(int id)
        {
            return context.SecResourcePermissions.SingleOrDefault(s => s.SecResourcePermissionId == id);
        }
        public List<SecResourcePermission> GetAll()
        {
            return context.SecResourcePermissions.ToList();
        }
    }
}