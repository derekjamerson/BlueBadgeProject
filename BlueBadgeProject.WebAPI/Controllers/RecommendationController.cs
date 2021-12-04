using BlueBadgeProject.Data;
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
    public class RecommendationController : ApiController
    {
        private RecommendationService CreateRecommendationService()
        {
            var userId = User.Identity.GetUserId();
            var recService = new RecommendationService(userId);
            return recService;
        }
        public IHttpActionResult Post(RecCreate rec)
        {
            var service = CreateRecommendationService();
            if (service.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            if (!service.CreateRecommendation(rec))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult GetById(int id)
        {
            RecommendationService recService = CreateRecommendationService();
            if (recService.CheckUserProfile())
            {
                var rec = recService.GetRecById(id);
            return Ok(rec);
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult GetByGroupId(int id)
        {
            RecommendationService recService = CreateRecommendationService();
            if (recService.CheckUserProfile())
            {
                var recs = recService.GetRecsByGroupId(id);
            return Ok(recs);
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateRecommendationService();
            if (service.CheckUserProfile())
            {
                if (!service.DeleteRecommendation(id))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
    }
}
