using CatSolution.Application.MainModule.Services.Module;
using CatSolution.DistributedServices.WebApi.MainModule.Common;
using CatSolution.DistributedServices.WebApi.MainModule.Filters;
using CatSolution.Domain.MainModule.Entities;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace CatSolution.DistributedServices.WebApi.MainModule.Controllers
{
    [RoutePrefix("api/Module")]
    public class ModuleController : ApiController
    {
        IUnityContainer _unityContainer = UnityConfig.GetConfiguredContainer();

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            var app = _unityContainer.Resolve<IModuleManagementService>();
            var list = app.GetAll();

            if (list == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [ServiceAuthorize]
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]SYS_Module item)
        {
            var app = _unityContainer.Resolve<IModuleManagementService>();
            var newItem = app.Add(item);

            if (newItem == null)
            {
                return BadRequest(ParamsConfig.MsgPostError);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("Modify")]
        public IHttpActionResult Modify([FromBody]SYS_Module item)
        {
            var app = _unityContainer.Resolve<IModuleManagementService>();
            var newItem = app.Modify(item);

            if (newItem == null)
            {
                return BadRequest(ParamsConfig.MsgPutError);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpDelete]
        [Route("Remove")]
        public IHttpActionResult Remove(byte moduleId)
        {
            var app = _unityContainer.Resolve<IModuleManagementService>();

            int result = app.Remove(moduleId);

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgDeleteError);
            }

            return Ok(moduleId);
        }
    }
}
