namespace CatSolution.Domain.MainModule.Entities
{
    public partial class SYS_Option
    {

        private string applicationName;
        public string ApplicationName
        {
            get
            {
                if ( applicationName == null || applicationName.Length == 0 )
                {
                    return (SYS_Application == null ? "" : SYS_Application.Name);
                }
                else
                {
                    return applicationName;
                }
            }
            set
            {
                applicationName = value;
            }
        }

        private string parentName;
        public string ParentName
        {
            get
            {
                if ( parentName == null || parentName.Length == 0 )
                {
                    string name = "/";

                    short? parent = ParentId;
                    SYS_Option option = this;

                    if (option.SYS_Option2 != null)
                    {
                        while (parent != null)
                        {
                            option = option.SYS_Option2;
                            parent = option.ParentId;
                            name = name + option.Name + "/";
                        }
                    }

                    return name;
                }
                else
                {
                    return parentName;
                }
            }
            set
            {
                parentName = value;
            }
        }
    }
}
