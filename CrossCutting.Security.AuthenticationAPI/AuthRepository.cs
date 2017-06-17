using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.CrossCutting.Security.AuthenticationAPI.Entities;
using CatSolution.CrossCutting.Security.AuthenticationAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI
{
    public class AuthRepository : IDisposable
    {
        #region Variables

        private AuthContext _ctx = null;
        private Logger _log = null;
        private UserManager<ApplicationUser> _userManager = null;

        #endregion

        #region Builder

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _log = new Logger();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        #endregion

        #region Authentication

        #region User

        public async Task<ApplicationUser> FindUser(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            try
            {
                user = await _userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return user;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {

            ApplicationUser user = null;

            try
            {
                user = await _userManager.FindAsync(userName, password);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return user;
        }

        public SYS_UserCompany UserInCompany(int workSpaceId, string userId, string companyCode)
        {
            var result = _ctx.UserCompanies.Where(uc => uc.UserId == userId && uc.SYS_Company.WorkSpaceId == workSpaceId && uc.SYS_Company.Code == companyCode).ToList();

            return result.Count > 0 ? result.First() : null;
        }

        public async Task<string> GetRole(string userId)
        {
            var roles = await _userManager.GetRolesAsync(userId);

            return roles.Count == 0 ? string.Empty : roles.First();
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserModel userModel)
        {
            IdentityResult result = null;

            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Name = userModel.Name,
                LastName = userModel.LastName,
                WorkSpaceId = userModel.WorkSpaceId
            };

            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    result = await _userManager.CreateAsync(user, userModel.Password);


                    if (result.Succeeded)
                    {
                        var resultRole = await _userManager.AddToRoleAsync(user.Id, userModel.Role);

                        if (!resultRole.Succeeded)
                        {
                            result = resultRole;
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _log.Error(ex.Message, ex.Source, ex.StackTrace);
                }

            }
            return result;
        }

        public async Task<IdentityResult> ResetPassword(string userId, string newPassword)
        {
            IdentityResult result = null;

            try
            {
                string token = _userManager.GeneratePasswordResetToken(userId);
                result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public bool IsValidCreateUser(int workSpaceId, short maxUsers)
        {
            bool result = false;

            try
            {
                int countUsers = _ctx.Users.Where(u => u.WorkSpaceId == workSpaceId).Count();

                result = maxUsers > countUsers;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }
            return result;
        }

        #endregion

        #region Token

        public SYS_ClientApp FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        #endregion

        #endregion

        #region Authorization

        public SYS_WorkSpace GetWorkSpace(int workSpaceId, string userName)
        {
            SYS_WorkSpace workSpace = null;

            try
            {
                workSpace = _ctx.WorkSpaces.Find(workSpaceId);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace, userName);
            }

            return workSpace;
        }

        public IEnumerable<SYS_DetailWorkSpace> GetDetailsWorkSpace(int workSpaceId, string userName)
        {
            IEnumerable<SYS_DetailWorkSpace> details = null;

            try
            {
                details = _ctx.DetailWorkSpaces.Where(d => d.WorkSpaceId == workSpaceId && d.Active).ToList();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace, userName);
            }

            return details;
        }

        public SYS_Module HasModuleAccess(int workSpaceId, byte moduleId)
        {
            SYS_Module item = null;

            try
            {
                var detail = _ctx.DetailWorkSpaces.Where(d => d.WorkSpaceId == workSpaceId && d.SYS_Application.ModuleId == moduleId && d.Active).FirstOrDefault();

                if (detail != null)
                {
                    item = new SYS_Module
                    {
                        ModuleId = detail.SYS_Application.ModuleId,
                        Name = detail.SYS_Application.SYS_Module.Name,
                        ShortName = detail.SYS_Application.SYS_Module.ShortName,
                        Icon = detail.SYS_Application.SYS_Module.Icon,
                        Active = detail.SYS_Application.SYS_Module.Active
                    };
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return item;
        }

        public bool HastOptionAccess(string userId, short optionId, byte companyId)
        {
            bool result = false;

            try
            {
                var authorizations = _ctx.Authorizations.Where(auth => auth.OptionId == optionId && auth.SYS_Option.Active &&
                        auth.CompanyId == companyId && auth.UserId == userId && auth.EndDate > DateTime.UtcNow);

                result = authorizations.Count() > 0;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public SYS_UserCompany CompanyData(byte companyId, string userId, string userName)
        {
            SYS_UserCompany company = null;

            try
            {
                company = _ctx.UserCompanies.Where(c => c.UserId == userId && c.SYS_Company.CompanyId == companyId &&
                    c.SYS_Company.Active && !c.SYS_Company.Canceled).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace, userName);
            }

            return company;
        }

        public SYS_Option OptionData(string viewType, byte moduleId, string userName)
        {
            SYS_Option option = null;

            try
            {
                option = _ctx.Options.Where(opts => opts.SYS_Application.ModuleId == moduleId && opts.ViewType.Equals(viewType)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace, userName);
            }

            return option;
        }

        public IEnumerable<SYS_Option> GetOptions(int workSpaceId, string role, string userId, byte companyId, byte moduleId, string userName)
        {
            IEnumerable<SYS_Option> list = null;

            try
            {
                var details = _ctx.DetailWorkSpaces.Where(dts => dts.WorkSpaceId == workSpaceId && dts.Active);

                if (role.Equals("Administrador"))
                {
                    var options = _ctx.Options.Where(opts => opts.SYS_Application.ModuleId == moduleId && opts.Active);

                    list = (from opts in options
                            join dts in details on opts.ApplicationId equals dts.ApplicationId
                            orderby opts.Sequence
                            select opts);
                }
                else
                {
                    var authorizations = _ctx.Authorizations.Where(auth => auth.SYS_Option.SYS_Application.ModuleId == moduleId && auth.SYS_Option.Active &&
                        auth.CompanyId == companyId && auth.UserId == userId && auth.EndDate > DateTime.UtcNow);

                    list = (from auth in authorizations
                            join dts in details on auth.SYS_Option.ApplicationId equals dts.ApplicationId
                               orderby auth.SYS_Option.Sequence
                               select new SYS_Option()
                               {
                                   OptionId = auth.OptionId,
                                   ApplicationId = auth.SYS_Option.ApplicationId,
                                   ParentId = auth.SYS_Option.ParentId,
                                   Name = auth.SYS_Option.Name,
                                   Tooltip = auth.SYS_Option.Tooltip,
                                   Sequence = auth.SYS_Option.Sequence,
                                   ViewClass = auth.SYS_Option.ViewClass,
                                   ViewType = auth.SYS_Option.ViewType,
                                   Icon = auth.SYS_Option.Icon,
                                   Leaf = auth.SYS_Option.Leaf,
                                   Active = auth.SYS_Option.Active
                               });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace, userName);
            }

            return list;
        }

        public bool IsAuthorize(string userId, byte companyId, string controller, string action)
        {
            bool result = false;

            try
            {
                var detail = _ctx.DetailOptions.Where(d => d.ControllerName.Equals(controller) && d.ActionName.Equals(action) && d.Active).FirstOrDefault();
                
                if ( detail != null )
                {
                    short optionId = detail.OptionId;
                    short detailOptionId = detail.DetailOptionId;

                    var authorize = _ctx.DetailAuthorizations.Where(auth => auth.UserId == userId && auth.CompanyId == companyId && auth.OptionId == optionId &&
                        auth.DetailOptionId == detailOptionId && auth.EndDate > DateTime.UtcNow ).FirstOrDefault();

                    result = authorize != null;
                }
                
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public IEnumerable<object> GetActionAccess(string role, short optionId, string userId, byte companyId)
        {
            IEnumerable<object> detail = null;

            try
            {
                if (role.Equals("Administrador"))
                {
                    var entities = _ctx.DetailOptions.Where(d => d.OptionId == optionId && d.Active);
                    detail = (from d in entities
                              select new
                              {
                                  Name = d.ActionName
                              }).Distinct();
                }
                else
                {
                    var access = _ctx.DetailAuthorizations.Where(d => d.OptionId == optionId && d.UserId == userId && d.CompanyId == companyId && d.SYS_DetailOption.Active);

                    detail = (from d in access
                              select new
                              {
                                  Name = d.SYS_DetailOption.ActionName
                              }).Distinct();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return detail;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ctx != null)
                {
                    _ctx.Dispose();
                    _ctx = null;
                }
                if (_log != null)
                {
                    _log.Dispose();
                    _log = null;
                }
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
        }

        #endregion


        public async Task<IdentityResult> UpdateUser(UpdateUserModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = userModel.Id,
                Email = userModel.Email,
                Name = userModel.Name,
                LastName = userModel.LastName
            };

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> ChangePassword(string userId, ChangePasswordModel passwordModel)
        {
            var result = await _userManager.ChangePasswordAsync(userId, passwordModel.OldPassword, passwordModel.NewPassword);

            return result;
        }

        public async Task<bool> AddRefreshToken(SYS_RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(SYS_RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<SYS_RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<SYS_RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        
    }
}