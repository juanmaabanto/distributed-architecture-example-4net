using CatSolution.Application.MainModule.Services.Application;
using CatSolution.Application.MainModule.Services.Company;
using CatSolution.Application.MainModule.Services.Module;
using CatSolution.Application.MainModule.Services.Option;
using CatSolution.Application.MainModule.Services.User;
using CatSolution.Domain.MainModule.Contracts.Applications;
using CatSolution.Domain.MainModule.Contracts.Companies;
using CatSolution.Domain.MainModule.Contracts.modules;
using CatSolution.Domain.MainModule.Contracts.Options;
using CatSolution.Domain.MainModule.Contracts.Users;
using CatSolution.Domain.MainModule.Services;
using CatSolution.Infrastructure.MainModule.Repositories;
using CatSolution.Infrastructure.MainModule.UnitOfWork;
using Microsoft.Practices.Unity;
using System;

namespace CatSolution.DistributedServices.WebApi.MainModule
{
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IMainUnitOfWork, MainUnitOfWork>();

            container.RegisterType<IApplicationRepository, ApplicationRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IModuleRepository, ModuleRepository>();
            container.RegisterType<IOptionRepository, OptionRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IApplicationManagementService, ApplicationManagementService>();
            container.RegisterType<ICompanyManagementService, CompanyManagementService>();
            container.RegisterType<IModuleManagementService, ModuleManagementService>();
            container.RegisterType<IOptionManagementService, OptionManagementService>();
            container.RegisterType<IUserManagementService, UserManagementService>();

        }
    }
}