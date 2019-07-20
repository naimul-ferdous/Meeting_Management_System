using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    
    public class SecUserRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();

        public int Insert(SecUser secUser)
        {
            context.SecUsers.Add(secUser);
            return context.SaveChanges();
        }
        public int Edit(SecUser secUser)
        {
            var user = context.SecUsers.FirstOrDefault(r => r.SecUserId == secUser.SecUserId);
            user.SecUserId = secUser.SecUserId;
            user.LoginName = secUser.LoginName;
            user.Password = secUser.Password;
            user.EmailId = secUser.EmailId;
            user.Status = secUser.Status;
            user.ModifiedBy = 2;
            user.ModifiedDate = DateTime.Now;
            

            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int secUserId)
        {
            var secUser = context.SecUsers.FirstOrDefault(s => s.SecUserId == secUserId);
            context.Entry(secUser).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }
        public SecUser Get(int id)
        {
            return context.SecUsers.SingleOrDefault(s => s.SecUserId == id);
        }
        public List<SecUser> GetAll()
        {
            return context.SecUsers.ToList();
        }

    }
}