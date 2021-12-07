using BlueBadgeProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBadgeProject.Models;

namespace BlueBadgeProject.Services
{
    public class RecommendationService
    {
        private readonly string _userId;
        public RecommendationService(string userId)
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
        public bool CreateRecommendation(RecCreate model)
        {
            if (!UserInGroup(model.GroupId))
                return false;

            var entity = new Recommendation()
            {
                SongId = model.SongId,
                UserProfileId = _userId,
                GroupId = model.GroupId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Recommendations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public RecItem GetRecById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Recommendations
                        .Single(e => e.RecommendationId == id);
                return
                    new RecItem
                    {
                        RecommendationId = entity.RecommendationId,
                        UserProfileId = entity.UserProfileId,
                        GroupId = entity.GroupId,
                        SongId = entity.SongId
                    };
            }
        }
        public IEnumerable<RecItem> GetRecsByUserProfileId(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Recommendations
                        .Where(e => e.UserProfileId == id)
                        .Select(
                            e =>
                                new RecItem
                                {
                                    RecommendationId = e.RecommendationId,
                                    UserProfileId = e.UserProfileId,
                                    SongId = e.SongId,
                                    GroupId = e.GroupId
                                });
                return query.ToArray();
            }
        }
        public IEnumerable<RecItem> GetRecsByGroupId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Recommendations
                        .Where(e => e.GroupId == id)
                        .Select(
                            e =>
                                new RecItem
                                {
                                    RecommendationId = e.RecommendationId,
                                    UserProfileId = e.UserProfileId,
                                    SongId = e.SongId,
                                    GroupId = e.GroupId
                                });
                return query.ToArray();
            }
        }
        public bool DeleteRecommendation(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Recommendations
                        .SingleOrDefault(e => e.RecommendationId == id && e.UserProfileId == _userId);

                if (entity == null)
                    return false;

                ctx.Recommendations.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        private bool UserInGroup(int groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return
                    ctx
                        .UserProfiles
                        .Single(e => e.UserProfileId == _userId)
                        .ListOfGroups
                        .SingleOrDefault(e => e.GroupId == groupId)
                        != null;
            }
        }
    }
}
