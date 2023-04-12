using NLog;

namespace LoggerSpace
{
    public class Log
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public bool isLogEnable { get; } = false;
        public Log(bool isLogEnable)
        {
            this.isLogEnable = isLogEnable;
        }
        public void Info(string message)
        {
            if(this.isLogEnable)
                logger.Info(message);
        }

        public void Trace(string message)
        {
            if (this.isLogEnable)
                logger.Trace(message);
        }

        public void TraceInfo(string message)
        {
            if (this.isLogEnable)
            {
                logger.Trace(message);
                logger.Info(message);
            }            
        }

        public void Error(string message)
        {
            if (this.isLogEnable)
                logger.Error(message);
        }

        public void Error(Exception exp,string message)
        {
            if (this.isLogEnable)
                logger.Error(exp,message);
        }        

        public void Warn(string message)
        {
            if (this.isLogEnable)
                logger.Warn(message);
        }

        public void Debug(string message)
        {
            if (this.isLogEnable)
                logger.Debug(message);
        }      
    }
}