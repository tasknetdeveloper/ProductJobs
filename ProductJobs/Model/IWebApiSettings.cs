
namespace Model
{
    public interface IWebApiSettings
    {
        string ConnectionDB { get; }
        string RedisConnection { get; }
        int ConsoleLoopDelay { get; }
        int MaxRetryDelay { get; }
        int LimitSelectRowsFromDb { get; }
        bool isLogEnable { get; set; }
        int InstanceNumber { get; set; }
    }
    
}
