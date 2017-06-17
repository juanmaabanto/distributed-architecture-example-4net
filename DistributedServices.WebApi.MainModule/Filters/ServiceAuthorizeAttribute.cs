using CatSolution.DistributedServices.WebApi.MainModule.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CatSolution.DistributedServices.WebApi.MainModule.Filters
{

    public class ServiceAuthorizeAttribute : AuthorizeAttribute
    {
        #region Variables

        private AuthTypes authType;

        #endregion

        #region Overrides

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                bool result = AuthorizeRequest(actionContext);

                if (result)
                {
                    return;
                }
                else
                {
                    authType = AuthTypes.Authorize;
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                authType = AuthTypes.Authenticate;
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (authType == AuthTypes.Authenticate)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(ParamsConfig.MsgUnauthenticate, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(ParamsConfig.MsgUnauthorize, Encoding.UTF8, "application/json")
                };
            }
        }

        #endregion

        #region Methods

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            string action = actionContext.ActionDescriptor.ActionName;
            string controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string token = actionContext.Request.Headers.Authorization.Parameter;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ParamsConfig.UriSvcAuth);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(actionContext.Request.Headers.Authorization.Scheme, token);

                HttpResponseMessage response = client.GetAsync(ParamsConfig.PathSvcAuthorize + "?controllerName=" + controller + "&actionName=" + action).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

    }
}