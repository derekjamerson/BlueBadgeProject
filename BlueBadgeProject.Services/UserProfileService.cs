using BlueBadgeProject.Data;
using BlueBadgeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Services
{
    public class UserProfileService
    {
        public bool CreateUserProfile(UserProfileCreate model)
        {
            var entity =
                new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
            
            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserProfiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
