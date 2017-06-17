namespace CatSolution.Application.Core.Helpers
{
    public class MsgConfig
    {
        public const string MsgGetError = "Ha ocurrido un error obteniendo la información.";
        public const string MsgAddError = "Ha ocurrido un error al intentar registrar la información.";
        public const string MsgModifyError = "Ha ocurrido un error al intentar actualizar la información.";
        public const string MsgDeleteError = "Ha ocurrido un error al eliminar.";

        public const  string MsgMaxCompaniesError = "Ha superado el máximo de compañias permitidas.";
        public const string MsgMaxCompaniesWarning = "Intento de registrar una compañia adicional.";
        public const string MsgCodeCompany = "Error al guardar, ya existe una compañia con el mismo código.";
    }
}
