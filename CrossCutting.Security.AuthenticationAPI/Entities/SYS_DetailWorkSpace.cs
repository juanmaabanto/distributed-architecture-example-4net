using System;
using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_DetailWorkSpace
    {
        [Key]
        public int DetailWorkSpaceId { get; set; }

        public int WorkSpaceId { get; set; }

        public byte ApplicationId { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }

        public bool Active { get; set; }

        public virtual SYS_Application SYS_Application { get; set; }
    }
}