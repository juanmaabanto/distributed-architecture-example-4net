using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_Module
    {
        [Key]
        public byte ModuleId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortName { get; set; }

        public string Tooltip { get; set; }

        public string Icon { get; set; }

        public bool Active { get; set; }

    }
}