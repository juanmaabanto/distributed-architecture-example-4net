using System;
using System.Collections.Generic;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_AuthorizationDTO
    {
        public string UserId { get; set; }
        public short OptionId { get; set; }
        public byte CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<SYS_DetailAuthorizationDTO> SYS_DetailAuthorization { get; set; }

        public short? ParentId { get; set; }
        public string OptionName { get; set; }
        public string Icon { get; set; }
        public bool Leaf { get; set; }
        public bool Allowed { get; set; }
    }
}
