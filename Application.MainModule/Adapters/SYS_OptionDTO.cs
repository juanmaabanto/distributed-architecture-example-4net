using System.Collections.Generic;

namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_OptionDTO
    {
        public short OptionId { get; set; }
        public byte ApplicationId { get; set; }
        public short? ParentId { get; set; }
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public byte Sequence { get; set; }
        public string ViewClass { get; set; }
        public string ViewType { get; set; }
        public string Icon { get; set; }
        public bool Leaf { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<SYS_DetailOptionDTO> SYS_DetailOption { get; set; }

        public string ParentName { get; set; }
        public string ApplicationName { get; set; }
    }
}
