
namespace Model
{
    public class SystemSettings: IEmulatorSettings, IWebApiSettings
    {
        public string ConnectionDB { get;  } = "";
        public string RedisConnection { get; set; } = "";
        public int DelayInService { get; set; } = 60000;       
        public bool isLogEnable { get; set; } = false;
        public int ConsoleLoopDelay { get; } = 1000;
        public int LimitSelectRowsFromDb { get; } = 1000;
        public int MaxRetryDelay { get;} = 3;
        public int InstanceNumber { get; set; } = -1;
    }
}
