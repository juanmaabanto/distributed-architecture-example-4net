using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_CompanyDTO
    {
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
