using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_Authorization
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public short OptionId { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte CompanyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserRegistration { get; set; }

        public string UserModification { get; set; }    

        public virtual SYS_Option SYS_Option { get; set; }

    }
}