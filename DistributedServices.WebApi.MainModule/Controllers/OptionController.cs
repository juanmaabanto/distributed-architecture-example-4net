using CatSolution.Application.MainModule.Services.Option;
using CatSolution.DistributedServices.WebApi.MainModule.Common;
using CatSolution.DistributedServices.WebApi.MainModule.Filters;
using CatSolution.Domain.MainModule.Entities;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace CatSolution.DistributedServices.WebApi.MainModule.Controllers
{
    [RoutePrefix("api/Option")]
    public class OptionController : ApiController
    {
        IUnityContainer _unityContainer = UnityConfig.GetConfiguredContainer();

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            var app = _unityContainer.Resolve<IOptionManagementService>();
            var list = app.GetAll();

            if ( list == null )
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(short optionId)
        {
            var opt = _unityContainer.Resolve<IOptionManagementService>();
            var entity = opt.GetById(optionId);

            if (entity == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(entity);
        }

        [Authorize]
        [HttpGet]
        [Route("GetForParent")]
        public IHttpActionResult GetForParent(byte applicationId, short optionId)
        {
            var opt = _unityContainer.Resolve<IOptionManagementService>();
            var entities = opt.FindBy(o => o.ApplicationId == applicationId && o.OptionId != optionId && !o.Leaf);

            if (entities == null)
            {
                return BadRequest(ParamsConfig.MsgGetError);
            }

            return Ok(entities);
        }

        [ServiceAuthorize]
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]SYS_Option item)
        {
            var app = _unityContainer.Resolve<IOptionManagementService>();
            var newItem =  app.Add(item);

            if ( newItem == null )
            {
                return BadRequest(ParamsConfig.MsgPostError);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpPut]
        [Route("Modify")]
        public IHttpActionResult Modify([FromBody]SYS_Option item)
        {
            var app = _unityContainer.Resolve<IOptionManagementService>();
            var newItem = app.ModifyOption(item);

            if (newItem == null)
            {
                return BadRequest(ParamsConfig.MsgPutError);
            }

            return Ok(newItem);
        }

        [ServiceAuthorize]
        [HttpDelete]
        [Route("Remove")]
        public IHttpActionResult Remove(short optionId)
        {
            var app = _unityContainer.Resolve<IOptionManagementService>();

            int result = app.Remove(optionId);

            if (result == 0)
            {
                return BadRequest(ParamsConfig.MsgDeleteError);
            }

            return Ok(optionId);
        }
    }
}