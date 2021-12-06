using BlueBadgeProject.Models;
using BlueBadgeProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;

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
             var service = CreateSongService();

             if (!service.CheckUserProfile())
                return BadRequest();

             if (!ModelState.IsValid)
                     return BadRequest(ModelState);

             if (!service.CreateSong(song))
                     return InternalServerError();

             return Ok();
        }
        [HttpPost]
        public IHttpActionResult CreateRecSong(SongCreate song, int groupId)
        {
            var service = CreateSongService();

            if (!service.CheckUserProfile())
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!service.CreateAndRecommendSong(song, groupId))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult GetSongById(int id)
        {
            SongService service = CreateSongService();

            if (!service.CheckUserProfile())
                return BadRequest();

            var song = service.GetSongById(id);
            return Ok(song);
        }
        public IHttpActionResult GetAll()
        {
            SongService songService = CreateSongService();

            if (!songService.CheckUserProfile())
                return BadRequest();
            
            IEnumerable<SongItem> _songs = songService.GetSongs();
            return Ok(_songs);
        }
        public IHttpActionResult Put(SongItem song)
        {
            var service = CreateSongService();

            if (!service.CheckUserProfile())
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!service.UpdateSong(song))
                return InternalServerError();

            return Ok();
        }
    }
}
