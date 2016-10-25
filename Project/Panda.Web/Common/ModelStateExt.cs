using System;
/*******************************************************************************
 * 作者：星星    
 * 描述：ModelState扩展
 * 修改记录： 
*********************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Panda.Web
{
    public static class ModelStateExt
    {
        public static Dictionary<string, string> ToError(this ModelStateDictionary modelState)
        {
            Dictionary<string, string> err = new Dictionary<string, string>();
            foreach (var skey in modelState.Keys)
            {
                var mstate = modelState[skey];
                if (mstate.Errors.Count > 0)
                {
                    err.Add(skey, mstate.Errors.FirstOrDefault().ErrorMessage);
                }
            }

            return err;
        }

    }
}