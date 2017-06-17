using AutoMapper;
using CatSolution.Application.Core;
using CatSolution.Application.Core.Helpers;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Contracts.Companies;
using CatSolution.Domain.MainModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatSolution.Application.MainModule.Services.Company
{
    public class CompanyManagementService : ManagementService<SYS_Company, SYS_CompanyDTO>, ICompanyManagementService
    {
        #region Variables

        Logger _log = null;
        ICompanyRepository _CompanyRepository;

        #endregion

        #region Builder

        public CompanyManagementService(ICompanyRepository companyRepository) : base(companyRepository)
        {
            _log = new Logger();
            _CompanyRepository = companyRepository;
        }

        #endregion

        #region Read

        public IEnumerable<SYS_UserCompanyDTO> GetCompanyByUser(string userId)
        {
            IUnitOfWork unitOfWork = _CompanyRepository.UnitOfWork;
            IEnumerable<SYS_UserCompanyDTO> list = null;

            try
            {
                var origin = _CompanyRepository.GetCompanyByUser(userId);

                list = (from o in origin
                        select new SYS_UserCompanyDTO()
                        {
                            UserId = o.UserId,
                            CompanyId = o.CompanyId,
                            Principal = o.Principal,
                            UserName = o.AspNetUser.UserName,
                            Code = o.SYS_Company.Code,
                            BusinessName = o.SYS_Company.BusinessName
                        });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return list;
        }

        public IEnumerable<SYS_CompanyDTO> GetCompanyThatNotAssign(string userId, int workSpaceId)
        {
            IUnitOfWork unitOfWork = _CompanyRepository.UnitOfWork;
            IEnumerable<SYS_CompanyDTO> list = null;

            try
            {
                var origin = _CompanyRepository.GetCompanyThatNotAssign(userId, workSpaceId);

                list = Mapper.Map<IEnumerable<SYS_CompanyDTO>>(origin);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return list;
        }

        #endregion

        #region Write

        public SYS_CompanyDTO AddCompany(SYS_Company item, string source)
        {
            IUnitOfWork unitOfWork = _CompanyRepository.UnitOfWork;
            SYS_CompanyDTO entityDTO = null;

            int count = _CompanyRepository.FindBy(c => c.WorkSpaceId == item.WorkSpaceId && c.Code == item.Code && !c.Canceled).Count();

            if (count > 0)
            {
                throw new Exception(MsgConfig.MsgCodeCompany);
            }

            if (_CompanyRepository.IsValidAdd(item.WorkSpaceId))
            {
                try
                {
                    _CompanyRepository.Add(item);
                    unitOfWork.Commit();
                    entityDTO = Mapper.Map<SYS_CompanyDTO>(item);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex.Source, ex.StackTrace, source);
                    throw new Exception(MsgConfig.MsgAddError);
                }
            }
            else
            {
                _log.Warning(MsgConfig.MsgMaxCompaniesWarning, typeof(CompanyManagementService).Assembly.FullName, source);
                throw new Exception(MsgConfig.MsgMaxCompaniesError);
            }

            return entityDTO;
        }

        public int Cancel(byte companyId)
        {
            IUnitOfWork unitOfWork = _CompanyRepository.UnitOfWork;
            int result = 0;

            try
            {
                var entity = _CompanyRepository.GetById(companyId);
                entity.Canceled = true;

                _CompanyRepository.Modify(entity);
                result = unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public SYS_CompanyDTO ModifyCompany(SYS_Company item)
        {
            IUnitOfWork unitOfWork = _CompanyRepository.UnitOfWork;
            SYS_CompanyDTO entityDTO = null;

            int count = _CompanyRepository.FindBy(c => c.WorkSpaceId == item.WorkSpaceId && c.Code == item.Code && c.CompanyId != item.CompanyId && !c.Canceled).Count();

            if (count > 0)
            {
                throw new Exception(MsgConfig.MsgCodeCompany);
            }

            try
            {
                var origin = _CompanyRepository.GetById(item.CompanyId);

                if (origin == null)
                {
                    throw new ArgumentNullException("origin");
                }
                origin.Code = item.Code;
                origin.BusinessName = item.BusinessName;
                origin.AlternateCode = item.AlternateCode;
                origin.Active = item.Active;

                _CompanyRepository.Modify(origin);
                unitOfWork.Commit();
                entityDTO = Mapper.Map<SYS_CompanyDTO>(origin);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception(MsgConfig.MsgModifyError);
            }

            return entityDTO;
        }

        #endregion

    }
}
