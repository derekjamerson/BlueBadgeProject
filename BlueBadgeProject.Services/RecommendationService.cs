using BlueBadgeProject.Data;
using ElevenNote.Data;
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
        public bool CreateRecommendation(RecCreate model)
        {
            var entity = new Recommendation()
            {
                SongId = model.SongId,
                UserProfileId = model.UserProfileId,
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
        public IEnumerable<RecItem> GetRecsByUserProfileId(int id)
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
    }
}
