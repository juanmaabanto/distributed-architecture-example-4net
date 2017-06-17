using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_DetailOptionDTO
    {
        public short DetailOptionId { get; set; }
        public short OptionId { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool Active { get; set; }
    }
}
