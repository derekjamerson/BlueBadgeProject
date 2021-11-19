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
        private readonly string _userId;
        public UserProfileService(string userId)
        {
            _userId = userId;
        }
        public bool CreateUserProfile(UserProfileCreate model)
        {
            var entity =
                new UserProfile()
                {
                    UserId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserProfiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<UserProfileItem> GetUserProfiles()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserProfiles
                        .Select(
                            e =>
                                new UserProfileItem
                                {
                                    FirstName = e.FirstName,
                                    LastName = e.LastName
                                }
                         );

                return query.ToArray();
            }
        }

        public bool UpdateUserProfile(UserProfileItem model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserId == model.UserId && e.UserId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                return ctx.SaveChanges() == 1;
            }
        }

        public UserProfileItem GetUserProfileById(string id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserId == id && e.UserId == _userId);
                return
                    new UserProfileItem
                    {
                        UserId = entity.UserId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    };
            }
        }
    }
}
