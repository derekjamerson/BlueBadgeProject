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
    public class UserProfileController : ApiController
    {
        private UserProfileService CreateUserProfileService()
        {
            var userId = User.Identity.GetUserId();
            var userProfileService = new UserProfileService(userId);
            return userProfileService;
        }

        public IHttpActionResult GetAll()
        {
            UserProfileService userProfileService = CreateUserProfileService();
            var userProfiles = userProfileService.GetUserProfiles();
            return Ok(userProfiles);
        }

        public IHttpActionResult Post(UserProfileCreate userProfile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserProfileService();

            if (!service.CreateUserProfile(userProfile))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(string id)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            var userProfile = userProfileService.GetUserProfileById(id);
            return Ok(userProfile);
        }

        public IHttpActionResult Put(UserProfileCreate userProfile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserProfileService();

            if (!service.UpdateUserProfile(userProfile))
                return InternalServerError();

            return Ok();
        }
    }
}
