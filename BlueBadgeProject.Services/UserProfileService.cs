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
        private readonly Guid _userId;
        public UserProfileService(Guid userId)
        {
            _userId = userId;
        }
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

        public bool UpdateUserProfile(UserProfileCreate model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                return ctx.SaveChanges() == 1;
            }
        }

        public UserProfileItem GetUserProfileById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == id && e.UserId == _userId);
                return
                    new UserProfileItem
                    {
                        UserProfileId = entity.UserProfileId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    };
            }
        }
    }
}
