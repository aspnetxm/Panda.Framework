/*******************************************************************************
 * 作者：星星    
 * 描述：  
 * 修改记录： 
*********************************************************************************/
using System;
using System.Collections.Generic;
using Panda.Code;
using Panda.Domain.Entity.SystemManage;
using Panda.Domain.IRepository.SystemManage;
using Panda.Repository.SystemManage;
using Panda.Data;

namespace Panda.Application.SystemManage
{
    public class UserApp
    {
        private IUserRepository service = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }
        public UserEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }

        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }

        public void UpdateForm(UserEntity userEntity)
        {
            service.Update(userEntity);
        }

        /// <summary>
        /// 登录密码登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserEntity Login(string username, string password)
        {
            UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
            if (userEntity == null)
            {
                throw new Exception("账户不存在，请重新输入");
            }

            if (userEntity.F_EnabledMark != true)
            {
                throw new Exception("账户被系统锁定,请联系管理员");
            }

            UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
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
            userLogOnApp.UpdateForm(userLogOnEntity);
            return userEntity;
        }
    }
}
