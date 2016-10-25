using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panda.Code;
using Panda.Domain.Entity.SystemManage;
using Panda.Domain.IRepository.SystemManage;
using Panda.Repository.SystemManage;
using Panda.Data;

namespace Panda.Application.Service
{
    public interface IUserService
    {
        Task<UserEntity> Login(string username, string password);
    }
}
