using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_WorkSpaceType
    {
        [Key]
        public byte WorkSpaceTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}