using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Contracts.Applications;
using CatSolution.Domain.MainModule.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CatSolution.Application.MainModule.Services.Application
{
    public class ApplicationManagementService : ManagementService<SYS_Application, SYS_ApplicationDTO>, IApplicationManagementService
    {
        IApplicationRepository _ApplicationRepository;

        public ApplicationManagementService(IApplicationRepository applicationRepository) :base(applicationRepository)
        {
            _ApplicationRepository = applicationRepository;
        }
    }
}
