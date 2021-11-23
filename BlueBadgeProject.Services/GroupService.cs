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
        //post
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
        public GroupItem GetGroupById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupId == id);
                return
                    new GroupItem
                    {
                        GroupId = entity.GroupId,
                        Name = entity.Name,
                    };
            }
        }
        public IEnumerable<GroupItem> GetGroups()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Groups
                        .Select(
                            e =>
                                new GroupItem
                                {
                                    GroupId = e.GroupId,
                                });
                return query.ToArray();
            }
        }
        public bool UpdateGroup(GroupItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupId == model.GroupId);

                entity.Name = model.Name;
                entity.GroupId = model.GroupId;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
