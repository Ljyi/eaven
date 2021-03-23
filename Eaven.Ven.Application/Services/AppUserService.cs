using Eaven.Ven.Domain;
using Eaven.Ven.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Application
{
    public class AppUserService : ApplicationService, IAppUserService
    {
        private IAppUserRepository _appUserRepository;
        public AppUserService(IAppUserRepository appUserRepository):base()
        {
            this._appUserRepository = appUserRepository;
        }
        public Task<bool> IsExistPhone(string phone)
        {
            return _appUserRepository.IsExistAsync(zw => zw.Phone == phone);
        }

        public async Task<AppUser> Login(string phone, string password)
        {
            try
            {
                Expression<Func<AppUser, bool>> appuerex = t => true;
                appuerex = appuerex.And(t => t.Phone == phone);
                appuerex = appuerex.And(t => t.Password.ToLower() == password.ToLower());
                var appuser = await _appUserRepository.GetAsync(appuerex);
                return appuser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> ModifyPassword(int appUserId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(string phone, string password)
        {
            throw new NotImplementedException();
        }
    }
}
