using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_DetailAuthorization
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

        [Key]
        [Column(Order = 4)]
        public short DetailOptionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserRegistration { get; set; }

        public string UserModification { get; set; }

        public virtual SYS_Authorization SYS_Authorization { get; set; }

        public virtual SYS_DetailOption SYS_DetailOption { get; set; }
    }
}