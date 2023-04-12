using DBWork;
using Redis.OM;
using Model;
using Microsoft.Extensions.Configuration;
using System;

namespace ReadEmulator
{   
    internal class ReadJob
    {
        private static ReposiotoryWork? repo = null;
        private static readonly CancellationTokenSource cancelToken = new();
        private IConfigurationRoot? config;        

        private static int delay = 100;
        internal ReadJob()
        {            
            IEmulatorSettings? iemusettings = GetSettins();
            if (iemusettings == null) return;
            RedisConnectionProvider provider = new RedisConnectionProvider(iemusettings.RedisConnection);

            Console.CancelKeyPress += (_,eventArgs) => {
                Cancel();
                eventArgs.Cancel = true;
            };            

            delay = (iemusettings.ConsoleLoopDelay > 0) ? iemusettings.ConsoleLoopDelay : delay;
            repo = new(provider, new LoggerSpace.Log(iemusettings.isLogEnable), iemusettings);
        }

        private void Cancel(string message = "") {
            Console.WriteLine($"Job stopped. {message}");
            cancelToken.Cancel();            
        }

        private IEmulatorSettings? GetSettins() {

            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (config != null)
            {
                var settings = config.GetRequiredSection("SystemSettings").Get<SystemSettings>();

                if (settings != null)
                    return settings;
            }
            return null;
        }

        internal async Task Start()
        {
            if (repo == null || !repo.HealthCheck())
            {
                Cancel("Ошибка подключения к базе данных или Redis"); 
                return;
            }
            
            Console.WriteLine("Job started. (for Cancel please press Ctrl+C)");
            int i = 0;
            Random rnd = new();
            while (!cancelToken.IsCancellationRequested)
            {
                i = rnd.Next();
                var r = repo.GetProductList();
                await Task.Delay(delay);
            }
        }
    }
}
