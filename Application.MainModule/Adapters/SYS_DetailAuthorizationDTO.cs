using System;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_DetailAuthorizationDTO
    {
        public string UserId { get; set; }
        public short OptionId { get; set; }
        public byte CompanyId { get; set; }
        public short DetailOptionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string DetailName { get; set; }
        public bool Allowed { get; set; }
    }
}
