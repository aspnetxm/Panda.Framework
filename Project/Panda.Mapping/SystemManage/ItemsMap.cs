﻿/*******************************************************************************
 * 作者：星星    
 * 描述：  
 * 修改记录： 
*********************************************************************************/
using Panda.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Panda.Mapping.SystemManage
{
    public class ItemsMap : EntityTypeConfiguration<ItemsEntity>
    {
        public ItemsMap()
        {
            this.ToTable("Sys_Items");
            this.HasKey(t => t.F_Id);
        }
    }
}
