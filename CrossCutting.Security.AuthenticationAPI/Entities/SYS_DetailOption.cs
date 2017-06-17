using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_DetailOption
    {
        [Key]
        public short DetailOptionId { get; set; }

        public short OptionId { get; set; }

        public string Name { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool Active { get; set; }

    }
}