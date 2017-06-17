using CatSolution.CrossCutting.Security.AuthenticationAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Controllers
{
    [RoutePrefix("api/Authorization")]
    public class AuthorizationController : ApiController
    {
        #region Variables

        private AuthRepository _repository = null;

        #endregion

        #region Builders

        public AuthorizationController()
        {
            _repository = new AuthRepository();
        }

        #endregion

        #region Gets

        [Authorize]
        [HttpGet]
        [Route("IsAuthorized")]
        public IHttpActionResult IsAuthorized(string controllerName, string actionName)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            string userId = claims[ClaimTypes.NameIdentifier];
            byte companyId = Convert.ToByte(claims["CompanyId"]);

            if (claims[ClaimTypes.Role].Equals("Administrador"))
            {
                return Ok(true);
            }
            else
            {
                if (_repository.IsAuthorize(userId, companyId, controllerName, actionName))
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("No tiene autorización.");
                }
            }
            
        }

        [Authorize]
        [HttpGet]
        [Route("GetModulesForPortfolio")]
        public IHttpActionResult GetModulesForPortfolio()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var details = _repository.GetDetailsWorkSpace(workSpaceId, claims[ClaimTypes.Name]);

            if (details == null)
            {
                return BadRequest("Ha Ocurrido un error.");
            }

            var list = (from d in details
                        select new
                        {
                            ModuleId = d.SYS_Application.ModuleId,
                            Name = d.SYS_Application.SYS_Module.Name,
                            Description = d.SYS_Application.SYS_Module.Description,
                            Icon = d.SYS_Application.SYS_Module.Icon
                        }).Distinct();

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetDetailWorkSpace")]
        public IHttpActionResult GetDetailWorkSpace()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var details = _repository.GetDetailsWorkSpace(workSpaceId, claims[ClaimTypes.Name]);

            if (details == null)
            {
                return BadRequest("Ha Ocurrido un error.");
            }

            var list = (from d in details
                        select new
                        {
                            ApplicationId = d.ApplicationId,
                            Name = d.SYS_Application.Name,
                            Description = d.SYS_Application.Description,
                            HostName = d.SYS_Application.HostName,
                            HostUri = d.SYS_Application.HostUri,
                            PathName = d.SYS_Application.PathName,
                            PathUri = d.SYS_Application.PathUri,
                            Icon = d.SYS_Application.Icon
                        });

            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("CompanyData")]
        public IHttpActionResult CompanyData()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            string userId = claims[ClaimTypes.NameIdentifier];
            byte companyId = Convert.ToByte(claims["CompanyId"]);

            var company = _repository.CompanyData(companyId, userId, claims[ClaimTypes.Name]);

            if (company == null)
            {
                return BadRequest("Ha Ocurrido un error.");
            }

            var data =  new
            {
                CompanyId = company.CompanyId,
                Code = company.SYS_Company.Code,
                BusinessName = company.SYS_Company.BusinessName
            };

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("GetModuleAccess")]
        public IHttpActionResult GetModuleAccess(byte moduleId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var module = _repository.HasModuleAccess(workSpaceId, moduleId);

            if (module == null)
            {
                return BadRequest("No tiene acceso al módulo o no existe.");
            }

            if (!module.Active)
            {
                return BadRequest("El módulo no se encuentra activo.");
            }

            var data = new
            {
                ModuleId = module.ModuleId,
                Name = module.Name,
                ShortName = module.ShortName,
                Icon = module.Icon
            };

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("GetOptionAccess")]
        public IHttpActionResult GetOptionAccess(byte moduleId, string viewType)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);
            string userId = claims[ClaimTypes.NameIdentifier];
            byte companyId = Convert.ToByte(claims["CompanyId"]);

            var app = _repository.HasModuleAccess(workSpaceId, moduleId);

            if ( app == null )
            {
                return BadRequest("No tiene acceso al módulo.");
            }

            if ( !app.Active )
            {
                return BadRequest("El módulo no se encuentra activo.");
            }

            var option = _repository.OptionData(viewType, moduleId, claims[ClaimTypes.Name]);

            if ( option == null )
            {
                return BadRequest("La opción solicitada no existe.");
            }

            if ( !option.Active )
            {
                return BadRequest("La opción no se encuentra activa.");
            }


            if (!claims[ClaimTypes.Role].Equals("Administrador") && !_repository.HastOptionAccess(userId, option.OptionId, companyId) )
            {
                return BadRequest("No tiene acceso a la opción solicitada");
            }

            var data = new
            {
                OptionId = option.OptionId,
                Name = option.Name,
                ViewClass = option.ViewClass,
                ViewType = option.ViewType,
                Icon = option.Icon
            };

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("GetOptions")]
        public IHttpActionResult GetOptions(byte moduleId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);
            string userId = claims[ClaimTypes.NameIdentifier];
            byte companyId = Convert.ToByte(claims["CompanyId"]);

            var options = _repository.GetOptions(workSpaceId, claims[ClaimTypes.Role], userId, companyId, moduleId, claims[ClaimTypes.Name]);

            if (options == null)
            {
                return BadRequest("Ha Ocurrido un error.");
            }

            var data = Helper.GetTreeList(options, null);

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("GetActionAccess")]
        public IHttpActionResult GetActionAccess(short optionId)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            string userId = claims[ClaimTypes.NameIdentifier];
            byte companyId = Convert.ToByte(claims["CompanyId"]);

            var access = _repository.GetActionAccess(claims[ClaimTypes.Role], optionId, userId, companyId);

            return Ok(access);
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

    }
}