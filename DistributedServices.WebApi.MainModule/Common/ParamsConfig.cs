namespace CatSolution.DistributedServices.WebApi.MainModule.Common
{
    public class ParamsConfig
    {
        public static string UriSvcAuth = "http://localhost:60081/";

        public static string PathSvcAuthorize = "api/Authorization/IsAuthorized";
        
        public static string MsgUnauthenticate = "{ \"message\":\"No se encuentra autenticado o su sesión a expirado.\" }";
        public static string MsgUnauthorize = "{ \"message\":\"No tiene autorización para acceder al recurso solicitado.\" }";

        public static string MsgGetError = "Ha ocurrido un error obteniendo la información.";
        public static string MsgPostError = "Ha ocurrido un error al intentar registrar la información.";
        public static string MsgPutError = "Ha ocurrido un error al intentar actualizar la información.";
        public static string MsgDeleteError = "Ha ocurrido un error al eliminar.";
    }
}