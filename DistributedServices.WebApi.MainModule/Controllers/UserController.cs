using CatSolution.Application.MainModule.Services.User;
using CatSolution.DistributedServices.WebApi.MainModule.Common;
using CatSolution.DistributedServices.WebApi.MainModule.Filters;
using CatSolution.Domain.MainModule.Entities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CatSolution.DistributedServices.WebApi.MainModule.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        IUnityContainer _unityContainer = UnityConfig.GetConfiguredContainer();

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var app = _unityContainer.Resolve<IUserManagementService>();
            var list = app.FindBy(c => c.WorkSpaceId == workSpaceId);

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetOnlyRoleUser")]
        public IHttpActionResult GetOnlyRoleUser()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var app = _unityContainer.Resolve<IUserManagementService>();
            var list = app.GetOnlyRoleUser(workSpaceId);

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAuthorizationOption")]
        public IHttpActionResult GetAuthorizationOption(string userId, byte companyId, byte applicationId)
        {
            var app = _unityContainer.Resolve<IUserManagementService>();
            var list = app.GetAuthorizationUser(userId, companyId, applicationId);

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            var tree = Helper.GetTreeList(list, null);

            return Ok(tree);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("AssignCompanies")]
        public IHttpActionResult AssignCompanies([FromBody]IEnumerable<SYS_UserCompany> item, string userId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var app = _unityContainer.Resolve<IUserManagementService>();
            int result;

            try
            {
                result = app.AssignCompanies(item, userId, workSpaceId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgPutError);
            }

            return Ok(result);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("Modify")]
        public IHttpActionResult Modify([FromBody]AspNetUser item)
        {
            var app = _unityContainer.Resolve<IUserManagementService>();
            var newItem = app.ModifyUser(item);

            if (newItem == null)
            {
                return BadRequest(ParamsConfig.MsgPutError);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpDelete]
        [Route("Remove")]
        public IHttpActionResult Remove(string userId)
        {
            var app = _unityContainer.Resolve<IUserManagementService>();

            int result = app.Remove(userId);

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgDeleteError);
            }

            return Ok(userId);
        }

        [ServiceAuthorize]
        [HttpPost]
        [Route("SaveAuthorization")]
        public IHttpActionResult SaveAuthorization([FromBody]object item, string userId, byte companyId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            string userName = claims[ClaimTypes.Name];

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> lst = js.Deserialize<List<Dictionary<string, object>>>(item.ToString());

            var app = _unityContainer.Resolve<IUserManagementService>();
            int result = app.SaveAuthorization(lst, userId, companyId, userName);

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgPostError);
            }

            return Ok(result);
        }

    }
}