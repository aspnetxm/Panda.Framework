using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NLog;

namespace Panda.Code
{
    public class LogFactory
    {
        static LogFactory()
        {
           
        }
        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetLogger(type.Name));
        }
        public static Log GetLogger(string str)
        {
            return new Log(LogManager.GetLogger(str));
        }
    }
}
