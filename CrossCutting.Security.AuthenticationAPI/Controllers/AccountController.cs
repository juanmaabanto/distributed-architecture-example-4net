using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.CrossCutting.Security.AuthenticationAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Variables

        private Logger _log = null;
        private AuthRepository _repository = null;

        #endregion

        #region Builders

        public AccountController()
        {
            _repository = new AuthRepository();
            _log = new Logger();
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repository!= null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
                if (_log != null)
                {
                    _log.Dispose();
                    _log = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Gets

        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public async Task<IHttpActionResult> GetUser()
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            var user = await _repository.FindUser(claims[ClaimTypes.Name]);

            if (user == null)
            {
                return BadRequest("Ocurrio un error al obtener los datos del usuario.");
            }

            var list = new { Name = user.Name, LastName = user.LastName, Domain = user.SYS_WorkSpace.Domain };

            return Ok(list);
        }



        #endregion

        #region Posts

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repository.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            _log.Info("se ha registrado un nuevo usuario: " + userModel.UserName);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<IHttpActionResult> Create(RegisterUserModel userModel)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            int workSpaceId = Convert.ToInt32(claims["WorkSpaceId"]);

            var workSpace = _repository.GetWorkSpace(workSpaceId, claims[ClaimTypes.Name]);

            if (workSpace == null)
            {
                return BadRequest("Ocurrio un error al obtener datos del espacio de trabajo.");
            }

            if (!_repository.IsValidCreateUser(workSpaceId,workSpace.MaxUsers))
            {
                return BadRequest("Supero el máximo de usuarios permitidos.");
            }

            userModel.Password = userModel.UserName;
            userModel.ConfirmPassword = userModel.UserName;
            userModel.UserName = userModel.UserName.ToLower() + "@" + workSpace.Domain + ".com";
            userModel.Role = "Usuario";
            userModel.WorkSpaceId = workSpaceId;

            ModelState.Remove("userModel.Password");
            ModelState.Remove("userModel.Role");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repository.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            _log.Info("se ha registrado un nuevo usuario: " + userModel.UserName,"AuthenticationAPI", claims[ClaimTypes.Name]);
            return Ok();
        }

        #endregion

        #region Puts

        [Authorize]
        [HttpPut]
        [Route("Reset")]
        public async Task<IHttpActionResult> ResetPassword(string userName)
        {
            var claims = Helper.GetClaims(User.Identity as ClaimsIdentity);
            var user = await _repository.FindUser(userName);

            string name = userName.Split('@')[0];

            IdentityResult result = await _repository.ResetPassword(user.Id, name);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            _log.Info("se reseteo la contraseña del usuario: " + user.UserName, "AuthenticationAPI", claims[ClaimTypes.Name]);
            return Ok();
        }

        #endregion

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion

        // POST api/Account/Update
        [AllowAnonymous]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(UpdateUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repository.UpdateUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        

        
    }
}