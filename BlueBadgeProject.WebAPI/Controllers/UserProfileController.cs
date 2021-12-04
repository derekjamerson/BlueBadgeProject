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

        public IHttpActionResult Post(UserProfileCreate userProfile)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserProfileService();

            if (!service.CreateUserProfile(userProfile))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
        [Route("api/UserProfile/all")]
        public IHttpActionResult GetAll()
        {
            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
               
            var userProfiles = userProfileService.GetUserProfiles();
            return Ok(userProfiles);
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult Get()
        {

            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
                var userProfile = userProfileService.GetUserProfile();
                return Ok(userProfile);
            }
            else return BadRequest("this is wrong");
        }
        public IHttpActionResult Get(string id)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
                
                var userProfile = userProfileService.GetUserProfileById(id);
                return Ok(userProfile);
            }
            else return BadRequest("this is wrong");
        }
      
        [HttpPut]
        public IHttpActionResult UpdateUserProfile(UserProfileCreate userProfile)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserProfileService();

            if (!service.UpdateUserProfile(userProfile))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
        [HttpPut]
        public IHttpActionResult AddUserToGroup(int groupId)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            if (userProfileService.CheckUserProfile())
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserProfileService();

            if (!service.AddUserToGroup(groupId))
                return InternalServerError();

            return Ok();
            }
            else return BadRequest("this is wrong");
        }
    }
}
