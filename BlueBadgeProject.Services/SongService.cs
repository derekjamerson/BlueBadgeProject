using BlueBadgeProject.Data;
using BlueBadgeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Services
{
    public class SongService
    {
        private readonly string _userId;
        public SongService(string userId)
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
        public bool CreateSong(SongCreate model)
        {
            var entity = new Song()
            {
                Title = model.Title,
                Artist = model.Artist,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Songs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SongItem> GetSongs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Select(
                            e =>
                                new SongItem
                                {
                                    SongId = e.SongId,
                                    Title = e.Title,
                                    Artist = e.Artist
                                });
                return query.ToArray();
            }
        }
        public SongItem GetSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == id);
                return
                    new SongItem
                    {
                        SongId = entity.SongId,
                        Title = entity.Title,
                        Artist = entity.Artist
                    };
            }
        }
        public bool UpdateSong(SongItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == model.SongId);

                entity.Title = model.Title;
                entity.Artist = model.Artist;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
