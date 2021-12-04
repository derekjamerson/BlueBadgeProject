using BlueBadgeProject.Data;
using BlueBadgeProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    UserProfileId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserProfiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool CheckUserProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == _userId);
                return entity.UserProfileId != null;
            }
        }
        public IEnumerable<UserProfileItem> GetUserProfiles()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.UserProfiles;
                                                
                var items =
                    query
                        .AsEnumerable()
                        .Select(
                            e =>
                                new UserProfileItem
                                {
                                    UserProfileId = e.UserProfileId,
                                    Name = GetFullName(e)
                                }
                        );
                return items.ToArray();
            }
        }
        public UserProfileItem GetUserProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == _userId);

                return
                    new UserProfileItem
                    {
                        UserProfileId = entity.UserProfileId,
                        Name = GetFullName(entity),
                        ListOfGroups = ListOfUsersGroups(entity)
                    };
            }
        }
        public UserProfileItem GetUserProfileById(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == id && e.UserProfileId == _userId);
                return
                    new UserProfileItem
                    {
                        UserProfileId = entity.UserProfileId,
                        Name = GetFullName(entity),
                        ListOfGroups = ListOfUsersGroups(entity)
                    };
            }
        }
        public bool UpdateUserProfile(UserProfileCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddUserToGroup(int groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == _userId);
                var listOfGroups =
                    ctx
                        .Groups
                        .ToArray();
                foreach (Group group in listOfGroups)
                {
                    if (group.GroupId == groupId)
                    {
                        entity.ListOfGroups.Add(group);
                    }
                }

                return ctx.SaveChanges() == 1;
            }
        }
        private List<GroupUpdate> ListOfUsersGroups(UserProfile user)
        {
            List<GroupUpdate> _groups = new List<GroupUpdate>();
            foreach (Group group in user.ListOfGroups)
            {
                _groups.Add(
                    new GroupUpdate
                    {
                        GroupId = group.GroupId,
                        Name = group.Name
                    });
            }
            return _groups;
        }
        private string GetFullName(UserProfile user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
