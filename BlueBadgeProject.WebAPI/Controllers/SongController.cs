using BlueBadgeProject.Models;
using BlueBadgeProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlueBadgeProject.WebAPI.Controllers
{
    [Authorize]
    public class SongController : ApiController
    {
        private SongService CreateSongService()
        {
            var userId = User.Identity.GetUserId();
            var songService = new SongService(userId);
            return songService;
        }

        public IHttpActionResult Post(SongCreate song)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.CreateSong(song))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult GetSongById(int id)
        {
            SongService songService = CreateSongService();
            var song = songService.GetSongById(id);
            return Ok(song);
        }
        public IHttpActionResult GetAll()
        {
            SongService songService = CreateSongService();
            IEnumerable<SongItem> _songs = songService.GetSongs();
            return Ok(_songs);
        }
        public IHttpActionResult Put(SongItem song)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.UpdateSong(song))
                return InternalServerError();

            return Ok();
        }
        [HttpPost]
        public IHttpActionResult CreateRecSong(SongCreate song, int groupId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.CreateAndRecommendSong(song, groupId))
                return InternalServerError();

            return Ok();
        }
    }
}
