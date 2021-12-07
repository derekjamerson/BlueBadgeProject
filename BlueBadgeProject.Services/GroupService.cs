using BlueBadgeProject.Data;
using BlueBadgeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Services
{
    public class GroupService
    {
        private readonly string _userId;
        public GroupService(string userId)
        {
            _userId = userId;
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
        public bool CreateGroup(GroupCreate model)
        {
            var entity = new Group()
            {
                Name = model.Name,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Groups.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public GroupItemFull GetGroupById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupId == id);
                var recEntity =
                    ctx
                        .Recommendations
                        .Include(nameof(Recommendation.UserProfile))
                        .Where(e => e.GroupId == id)
                        .AsEnumerable()
                        .Select(
                            e =>
                                new RecViewModel
                                {
                                    RecommendationId = e.RecommendationId,
                                    SongTitle = GetSongFromRec(e).Title,
                                    SongArtist = GetSongFromRec(e).Artist,
                                    RecommendedBy = GetFullName(e.UserProfile)
                                })
                        .ToList();

                return
                    new GroupItemFull
                    {
                        GroupId = entity.GroupId,
                        Name = entity.Name,
                        ListOfUsers = ListOfGroupsUsers(entity),
                        ListOfRecs = recEntity
                    };
            }
        }
        public IEnumerable<GroupItemPartial> GetGroups()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Groups
                        .Include(nameof(Group.ListOfUsers))
                        .AsEnumerable()
                        .Select(
                            e =>
                                new GroupItemPartial
                                {
                                    GroupId = e.GroupId,
                                    Name = e.Name,
                                    MemberCount = e.ListOfUsers.Count
                                });
                return query.ToList();
            }
        }
        public bool UpdateGroup(GroupUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .SingleOrDefault(e => e.GroupId == model.GroupId);

                if (entity == null)
                    return false;

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        private ICollection<UserProfileViewModel> ListOfGroupsUsers(Group group)
        {
            ICollection<UserProfileViewModel> _users = new List<UserProfileViewModel>();
            foreach (UserProfile user in group.ListOfUsers)
            {
                _users.Add(
                    new UserProfileViewModel
                    {
                        UserProfileId = user.UserProfileId,
                        Name = GetFullName(user)
                    });
            }
            return _users;
        }
        private string GetFullName(UserProfile user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
        private Song GetSongFromRec(Recommendation rec)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == rec.SongId);
                return entity;
            }
        }
    }
}
