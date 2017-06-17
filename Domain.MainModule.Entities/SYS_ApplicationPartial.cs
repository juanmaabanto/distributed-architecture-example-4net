namespace CatSolution.Domain.MainModule.Entities
{
    public partial class SYS_Application
    {
        private string moduleName;
        public string ModuleName
        {
            get
            {
                if (moduleName == null || moduleName.Length == 0)
                {
                    return (SYS_Module == null ? "" : SYS_Module.Name);
                }
                else
                {
                    return moduleName;
                }
            }
            set
            {
                moduleName = value;
            }
        }
    }
}
