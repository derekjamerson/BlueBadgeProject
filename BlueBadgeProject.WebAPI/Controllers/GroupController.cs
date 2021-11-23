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
    public class GroupController : ApiController
    {

        // GET: Group
        public GroupService CreateGroupService()
        {
            var userId = User.Identity.GetUserId();
            var groupService = new GroupService(userId);
            return groupService;

        }
        //[HttpPost]
        public IHttpActionResult Post(GroupCreate group)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGroupService();

            if (!service.CreateGroup(group))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult GetAll()
        {
            GroupService groupService = CreateGroupService();
            var groups = groupService.GetGroups();
            return Ok(groups);
        }
        public IHttpActionResult Put(GroupItem group)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGroupService();

            if (!service.UpdateGroup(group))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult GetByGroupId()
        {
            var userId = User.Identity.GetUserId();
            var groupService = new GroupService(userId);
            return Ok();

        }
    }
}