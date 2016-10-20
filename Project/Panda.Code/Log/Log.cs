/*******************************************************************************
 * 作者：星星    
 * 描述：NLog 
 * 修改记录： 
*********************************************************************************/

using System;
using NLog;

namespace Panda.Code
{
    public class Log
    {
        private ILogger logger;
        public Log(ILogger log)
        {
            this.logger = log;
        }
        public void Debug(object message)
        {
            this.logger.Debug(message);
        }
        public void Error(object message)
        {
            this.logger.Error(message);
        }
        public void Info(object message)
        {
            this.logger.Info(message);
        }
        public void Warn(object message)
        {
            this.logger.Warn(message);
        }
    }
}
