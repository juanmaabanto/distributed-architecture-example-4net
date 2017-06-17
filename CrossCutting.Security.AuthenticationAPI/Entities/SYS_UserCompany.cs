using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_UserCompany
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte CompanyId { get; set; }

        public bool Principal { get; set; }

        public virtual SYS_Company SYS_Company { get; set; }
    }
}