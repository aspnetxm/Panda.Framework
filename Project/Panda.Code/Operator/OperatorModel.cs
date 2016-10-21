/*******************************************************************************
 * 作者：星星    
 * 描述：操作人员信息   
 * 修改记录： 
*********************************************************************************/

using System;

namespace Panda.Code
{
    public class OperatorModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        public string LoginIPAddress { get; set; }

        public string LoginIPAddressName { get; set; }

        public string LoginToken { get; set; }

        public DateTime LoginTime { get; set; }

        public bool IsSystem { get; set; }
    }
}
