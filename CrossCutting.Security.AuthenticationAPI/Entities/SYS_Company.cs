using System;
using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_Company
    {
        [Key]
        public byte CompanyId { get; set; }

        public int WorkSpaceId { get; set; }

        public string Code { get; set; }

        public string BusinessName { get; set; }

        public string AlternateCode { get; set; }

        public bool Active { get; set; }

        public bool Canceled { get; set; }

        public string UserRegistration { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}