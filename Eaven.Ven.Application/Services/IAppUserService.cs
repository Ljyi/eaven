
using Eaven.Ven.Domain;
using System;
using System.Threading.Tasks;

namespace Eaven.Ven.Application
{
    public interface IAppUserService: IApplicationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AppUser> Login(string phone, string password);
        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="appUserDto"></param>
        ///// <returns></returns>
        //Task<AppUser> Register(AddAppUserDto appUserDto);
        ///// <summary>
        ///// 完善个人信息
        ///// </summary>
        ///// <param name="appUserDto"></param>
        ///// <returns></returns>
        //Task<bool> ImproveAppUser(int appuserId, EditAppUserDto appUserDto);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ModifyPassword(int appUserId, string password);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(string phone, string password);

        /// <summary>
        /// 验证手机号是否已经注册过
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> IsExistPhone(string phone);
        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //Task<AppUserInfo> GetAPPUserInfo(int userId);
        /////根据UserUniqueId获取用户信息
        ///// </summary>
        ///// <returns></returns>
        //Task<AppUserInfo> GetAppUserByUserUniqueId(string userUniqueId);
    }
}
