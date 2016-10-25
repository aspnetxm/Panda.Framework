using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panda.Code;
using Panda.Domain.Entity.SystemManage;
using Panda.Domain.IRepository.SystemManage;
using Panda.Repository.SystemManage;

namespace Panda.Application.Service
{
    public class UserService : IUserService
    {
        private IUserRepository userService = new UserRepository();
        private IUserLogOnRepository userLogService = new UserLogOnRepository();

        public async Task<UserEntity> Login(string username, string password)
        {
            return await Task.Run(() =>
            {
                UserEntity userEntity = userService.FindEntity(t => t.F_Account == username);
                if (userEntity == null)
                {
                    throw new Exception("账户不存在，请重新输入");
                }
                if (userEntity.F_EnabledMark != true)
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }

                UserLogOnEntity userLogOnEntity = userLogService.FindEntity(o => o.F_UserId == userEntity.F_Id);
                string dbPassword = Md5Encrypt.Md5(AES.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                if (dbPassword != userLogOnEntity.F_UserPassword)
                {
                    throw new Exception("密码不正确，请重新输入");
                }

                DateTime lastVisitTime = DateTime.Now;
                int LogOnCount = userLogOnEntity.F_LogOnCount ?? +1;
                if (userLogOnEntity.F_LastVisitTime != null)
                {
                    userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime;
                }
                userLogOnEntity.F_LastVisitTime = lastVisitTime;
                userLogOnEntity.F_LogOnCount = LogOnCount;
                userLogService.Update(userLogOnEntity);
                return userEntity;
            });
        }
    }
}
