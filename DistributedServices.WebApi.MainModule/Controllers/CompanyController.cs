using CatSolution.Application.MainModule.Adapters;
using CatSolution.Application.MainModule.Services.Company;
using CatSolution.DistributedServices.WebApi.MainModule.Common;
using CatSolution.DistributedServices.WebApi.MainModule.Filters;
using CatSolution.Domain.MainModule.Entities;
using Microsoft.Practices.Unity;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace CatSolution.DistributedServices.WebApi.MainModule.Controllers
{
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        IUnityContainer _unityContainer = UnityConfig.GetConfiguredContainer();

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var app = _unityContainer.Resolve<ICompanyManagementService>();
            var list = app.FindBy(c => c.WorkSpaceId == workSpaceId && !c.Canceled);

            if ( list == null )
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetCompanyByUser")]
        public IHttpActionResult GetCompanyByUser(string userId)
        {
            var app = _unityContainer.Resolve<ICompanyManagementService>();
            var list = app.GetCompanyByUser(userId);

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetCompanyThatNotAssign")]
        public IHttpActionResult GetCompanyThatNotAssign(string userId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var app = _unityContainer.Resolve<ICompanyManagementService>();
            var list = app.GetCompanyThatNotAssign(userId, workSpaceId);

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [ServiceAuthorize]
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]SYS_Company item)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            SYS_CompanyDTO newItem = null;
            var app = _unityContainer.Resolve<ICompanyManagementService>();

            item.WorkSpaceId = workSpaceId;
            item.UserRegistration = claims[ClaimTypes.Name];
            item.RegistrationDate = DateTime.UtcNow;

            try
            {
                newItem = app.AddCompany(item, claims[ClaimTypes.Name]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("Modify")]
        public IHttpActionResult Modify([FromBody]SYS_Company item)
        {
            var app = _unityContainer.Resolve<ICompanyManagementService>();
            SYS_CompanyDTO newItem = null;

            try
            {
                newItem = app.ModifyCompany(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("Cancel")]
        public IHttpActionResult Cancel(byte companyId)
        {
            var app = _unityContainer.Resolve<ICompanyManagementService>();

            int result = app.Cancel(companyId);

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgDeleteError);
            }

            return Ok(companyId);
        }
    }
}