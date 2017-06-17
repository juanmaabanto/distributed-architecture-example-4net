using System;
using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_WorkSpace
    {
        [Key]
        public int WorkSpaceId { get; set; }

        public byte WorkSpaceTypeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public byte MonthsTest { get; set; }

        public short MaxUsers { get; set; }

        public decimal PricePerUser { get; set; }

        public byte MaxCompanies { get; set; }

        public decimal PricePerCompany { get; set; }

        public decimal Discount { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }

        public bool Active { get; set; }

        public virtual SYS_WorkSpaceType SYS_WorkSpaceType { get; set; }

    }
}