/*******************************************************************************
 * 作者：星星    
 * 描述： 
 * 修改记录： 
*********************************************************************************/
using System;

namespace Panda.Domain
{
    public interface ICreationAudited
    {
        string F_Id { get; set; }
        string F_CreatorUserId { get; set; }
        DateTime? F_CreatorTime { get; set; }
    }
}