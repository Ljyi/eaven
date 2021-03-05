using Eaven.Ven.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Application
{
    public class AppUserService : IAppUserService
    {
        private IAppUserRepository _appUserRepository;
        public AppUserService(IAppUserRepository appUserRepository)
        {
            this._appUserRepository = appUserRepository;
        }
        public Task<bool> IsExistPhone(string phone)
        {
            return _appUserRepository.IsExistAsync(zw => zw.Phone == phone);
        }

        public Task<bool> Login(string phone, string password)
        {
            throw new NotImplementedException();
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
