using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aberrantGeek.DiscordBot
{
    public interface ICustomService
    {
        void Run();
    }

    public abstract class BaseService : ICustomService
    {
        protected readonly ILogger<BaseService> _logger;
        protected readonly IConfiguration _config;

        public BaseService(ILogger<BaseService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        abstract public void Run();
    }
}