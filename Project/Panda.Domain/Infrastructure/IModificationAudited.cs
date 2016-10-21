/*******************************************************************************
 * 作者：星星    
 * 描述： 
 * 修改记录： 
*********************************************************************************/
using System;

namespace Panda.Domain
{
    public interface IModificationAudited
    {
        string F_Id { get; set; }
        string F_LastModifyUserId { get; set; }
        DateTime? F_LastModifyTime { get; set; }
    }
}