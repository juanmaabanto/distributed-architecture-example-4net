using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Entities
{
    public class SYS_Option
    {
        [Key]
        public short OptionId { get; set; }

        public byte ApplicationId { get; set; }

        public Nullable<short> ParentId { get; set; }

        public string Name { get; set; }

        public string Tooltip { get; set; }

        public byte Sequence { get; set; }

        public string ViewClass { get; set; }

        public string ViewType { get; set; }

        public string Icon { get; set; }

        public bool Leaf { get; set; }

        public bool Active { get; set; }

        public virtual SYS_Application SYS_Application { get; set; }

    }
}