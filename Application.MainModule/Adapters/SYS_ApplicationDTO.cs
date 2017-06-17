using System;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_ApplicationDTO
    {
        public byte ApplicationId { get; set; }
        public byte ModuleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string HostName { get; set; }
        public string HostUri { get; set; }
        public string PathName { get; set; }
        public string PathUri { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }

        public string ModuleName { get; set; }
    }
}
