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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRecommendationService();

            if (!service.CreateRecommendation(rec))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult GetById(int id)
        {
            RecommendationService recService = CreateRecommendationService();
            var rec = recService.GetRecById(id);
            return Ok(rec);
        }
        public IHttpActionResult GetByGroupId(int id)
        {
            RecommendationService recService = CreateRecommendationService();
            var recs = recService.GetRecsByGroupId(id);
            return Ok(recs);
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateRecommendationService();

            if (!service.DeleteRecommendation(id))
                return InternalServerError();

            return Ok();
        }
    }
}
