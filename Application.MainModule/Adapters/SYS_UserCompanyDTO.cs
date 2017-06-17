namespace CatSolution.Application.MainModule.Adapters
{
    public class SYS_UserCompanyDTO
    {
        public string UserId { get; set; }
        public byte CompanyId { get; set; }
        public bool Principal { get; set; }

        public string UserName { get; set; }
        public string Code { get; set; }
        public string BusinessName { get; set; }
    }
}
