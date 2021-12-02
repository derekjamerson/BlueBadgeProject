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
                        .Where(e => e.GroupId == id)
                        .ToArray();
                return
                    new GroupItemFull
                    {
                        GroupId = entity.GroupId,
                        Name = entity.Name,
                        ListOfUsers = entity.ListOfUsers,
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
                        .Select(
                            e =>
                                new GroupItemPartial
                                {
                                    GroupId = e.GroupId,
                                    Name = e.Name,
                                    ListOfUsers = e.ListOfUsers
                                });
                return query.ToArray();
            }
        }
        public bool UpdateGroup(GroupUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupId == model.GroupId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
