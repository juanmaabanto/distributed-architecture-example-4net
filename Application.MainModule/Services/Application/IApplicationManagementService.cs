using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatSolution.Application.MainModule.Services.Application
{
    public interface IApplicationManagementService : IManagementService<SYS_Application,SYS_ApplicationDTO>
    {
    }
}
