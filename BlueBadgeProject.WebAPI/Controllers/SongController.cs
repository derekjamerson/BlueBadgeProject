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
            if (service.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                     return BadRequest(ModelState);

                

                 if (!service.CreateSong(song))
                     return InternalServerError();

                     return Ok();
            }
            else return BadRequest("this is wrong");
    }
        public IHttpActionResult GetSongById(int id)
        {
            SongService songService = CreateSongService();
            if (songService.CheckUserProfile())
            {
                var song = songService.GetSongById(id);
            return Ok(song);
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult GetAll()
        {

            SongService songService = CreateSongService();
            if (songService.CheckUserProfile())
            {
                IEnumerable<SongItem> _songs = songService.GetSongs();
            return Ok();
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult Put(SongItem song)
        {
            var service = CreateSongService();
            if (service.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            if (!service.UpdateSong(song))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
    }
}
