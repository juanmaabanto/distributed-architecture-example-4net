using AutoMapper;
using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Contracts.Users;
using CatSolution.Domain.MainModule.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using CatSolution.Domain.MainModule.Contracts.Companies;
using CatSolution.Domain.MainModule.Contracts.Options;
using CatSolution.Application.Core.Helpers;

namespace CatSolution.Application.MainModule.Services.User
{
    public class UserManagementService : ManagementService<AspNetUser, AspNetUserDTO>, IUserManagementService
    {
        Logger _log = null;
        ICompanyRepository _CompanyRepository;
        IOptionRepository _OptionRepository;
        IUserRepository _UserRepository;
        IUserService _UserService;

        public UserManagementService(IUserRepository userRepository, ICompanyRepository companyRepository, IOptionRepository optionRepository, IUserService userService) :base(userRepository)
        {
            _log = new Logger();
            _CompanyRepository = companyRepository;
            _OptionRepository = optionRepository;
            _UserRepository = userRepository;
            _UserService = userService;
        }

        public int AssignCompanies(IEnumerable<SYS_UserCompany> lstUserCompany, string userId, int workSpaceId)
        {
            IUnitOfWork unitOfWork = _UserRepository.UnitOfWork;
            AspNetUser user = _UserRepository.GetById(userId);
            int result = 0;

            if (user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            if (user.WorkSpaceId != workSpaceId)
            {
                throw new Exception("El usuario no pertenece al mismo espacio de trabajo.");
            }

            if (lstUserCompany != null && lstUserCompany.Count() > 0 && lstUserCompany.Count(uc => uc.Principal) != 1 )
            {
                throw new Exception("El usuario tiene que tener una y solo una compañia asignada como principal.");
            }

            try
            {
                user.SYS_UserCompany.ToList().ForEach(d => { _UserRepository.RemoveUserCompany(d); });

                foreach (var item in lstUserCompany)
                {
                    var company = _CompanyRepository.FindBy(c => c.CompanyId == item.CompanyId && c.WorkSpaceId == workSpaceId);

                    if (company == null)
                    {
                        return 0;
                    }
                    _UserService.AssignCompany(user, item.CompanyId, item.Principal);
                }

                _UserRepository.Modify(user);
                result = unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception(MsgConfig.MsgAddError);
            }
            return result;
        }

        public AspNetUserDTO ModifyUser(AspNetUser item)
        {
            IUnitOfWork unitOfWork = _UserRepository.UnitOfWork;
            AspNetUserDTO entityDTO = null;

            try
            {
                var origin = _UserRepository.GetById(item.Id);

                if (origin == null)
                {
                    throw new ArgumentNullException("origin");
                }
                origin.Email = item.Email;
                origin.Name = item.Name;
                origin.LastName = item.LastName;

                _UserRepository.Modify(origin);
                unitOfWork.Commit();
                entityDTO = Mapper.Map<AspNetUserDTO>(origin);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return entityDTO;
        }

        public IEnumerable<AspNetUserDTO> GetOnlyRoleUser(int workSpaceId)
        {
            IUnitOfWork unitOfWork = _UserRepository.UnitOfWork;
            IEnumerable<AspNetUserDTO> lst = null;

            try
            {
                var role = _UserRepository.GetRole("Usuario");
                
                var users = role.AspNetUsers.Where(u => u.WorkSpaceId == workSpaceId);

                lst = Mapper.Map<IEnumerable<AspNetUserDTO>>(users);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return lst;
        }

        public IEnumerable<SYS_AuthorizationDTO> GetAuthorizationUser(string userId, byte companyId, byte applicationId)
        {
            IUnitOfWork unitOfWork = _UserRepository.UnitOfWork;
            List<SYS_AuthorizationDTO> lst = null;

            try
            {
                var lstOptions = _OptionRepository.FindBy(opt => opt.ApplicationId == applicationId && opt.Active);
                var lstAuths = _UserRepository.GetAuthorizationUser(userId, companyId, applicationId);
                List<SYS_AuthorizationDTO> lstAuthorization = new List<SYS_AuthorizationDTO>();

                var entities = (from opt in lstOptions
                                join auth in lstAuths on opt.OptionId equals auth.OptionId into records
                                from r in records.DefaultIfEmpty()
                                select new { options = opt, authorization = r }).ToList();

                foreach (var item in entities)
                {
                    var detail = new List<SYS_DetailAuthorizationDTO>();

                    foreach (var d in item.options.SYS_DetailOption)
                    {
                        SYS_DetailAuthorization detailAuthorization = null;

                        if (item.authorization!= null )
                        {
                            detailAuthorization = item.authorization.SYS_DetailAuthorization.Where(c => c.DetailOptionId == d.DetailOptionId).FirstOrDefault();
                        }

                        detail.Add(new SYS_DetailAuthorizationDTO()
                        {
                            UserId = userId,
                            CompanyId = companyId,
                            OptionId = item.options.OptionId,
                            DetailOptionId = d.DetailOptionId,
                            StartDate = (detailAuthorization == null ? new DateTime?() : detailAuthorization.StartDate),
                            EndDate = (detailAuthorization == null ? new DateTime?() : detailAuthorization.EndDate),
                            DetailName = d.Name,
                            Allowed = (detailAuthorization == null ? false : true)
                        });
                    }

                    lstAuthorization.Add(new SYS_AuthorizationDTO()
                    {
                        UserId = userId,
                        CompanyId = companyId,
                        OptionId = item.options.OptionId,
                        StartDate = ( item.authorization == null ? new DateTime?() : item.authorization.StartDate.ToUniversalTime()),
                        EndDate = (item.authorization == null ? new DateTime?() : item.authorization.EndDate.ToUniversalTime()),
                        ParentId = item.options.ParentId,
                        OptionName = item.options.Name,
                        Icon = item.options.Icon,
                        Leaf = item.options.Leaf,
                        Allowed = (item.authorization == null ? false : true ),
                        SYS_DetailAuthorization = detail
                    });
                }
                lst = lstAuthorization;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return lst;
        }

        public int SaveAuthorization(List<Dictionary<string, object>> lst, string userId, byte companyId, string userName)
        {
            IUnitOfWork unitOfWork = _UserRepository.UnitOfWork;
            int result = 0;

            try
            {
                foreach (var item in lst)
                {
                    var entity = _UserRepository.FindAuthorization(userId, Convert.ToInt16(item["OptionId"]), companyId);

                    if ( Convert.ToInt16(item["DetailOptionId"]) == 0 )
                    {
                        if (entity == null)
                        {
                            _UserRepository.AddAuthorization(new SYS_Authorization() {
                                UserId = userId,
                                OptionId = Convert.ToInt16(item["OptionId"]),
                                CompanyId = companyId,
                                StartDate = Convert.ToDateTime(item["StartDate"]).Date,
                                EndDate = Convert.ToDateTime(item["EndDate"]).Date,
                                UserRegistration = userName
                            });
                        }
                        else
                        {
                            if ( Convert.ToBoolean(item["Allowed"]) )
                            {
                                entity.StartDate = Convert.ToDateTime(item["StartDate"]).Date;
                                entity.EndDate = Convert.ToDateTime(item["EndDate"]).Date;
                                entity.UserModification = userName;
                                _UserRepository.ModifyAuthorization(entity);
                            }
                            else
                            {
                                entity.SYS_DetailAuthorization.ToList().ForEach(d => { _UserRepository.RemoveDetailAuthorization(d); });
                                _UserRepository.RemoveAuthorization(entity);
                            }
                        }
                    }
                    else
                    {
                        if (entity != null)
                        {
                            var detail = _UserRepository.FindDetailAuthorization(userId, Convert.ToInt16(item["OptionId"]), companyId, Convert.ToInt16(item["DetailOptionId"]));

                            if (detail == null)
                            {
                                _UserRepository.AddDetailAuthorization(new SYS_DetailAuthorization()
                                {
                                    UserId = userId,
                                    OptionId = Convert.ToInt16(item["OptionId"]),
                                    CompanyId = companyId,
                                    DetailOptionId = Convert.ToInt16(item["DetailOptionId"]),
                                    StartDate = Convert.ToDateTime(item["StartDate"]).Date,
                                    EndDate = Convert.ToDateTime(item["EndDate"]).Date,
                                    UserRegistration = userName
                                });
                            }
                            else
                            {
                                if (Convert.ToBoolean(item["Allowed"]))
                                {
                                    detail.StartDate = Convert.ToDateTime(item["StartDate"]).Date;
                                    detail.EndDate = Convert.ToDateTime(item["EndDate"]).Date;
                                    detail.UserModification = userName;
                                    _UserRepository.ModifyDetailAuthorization(detail);
                                }
                                else
                                {
                                    _UserRepository.RemoveDetailAuthorization(detail);
                                }
                            }
                        }
                    }
                }
                unitOfWork.Commit();
                result = 1;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }
            return result;
        }
    }
}
