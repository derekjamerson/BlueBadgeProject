using BlueBadgeProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BlueBadgeProject.Models;

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

            return Ok($"");
        }
    }
}