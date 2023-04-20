using MiniGameFramework.Logging;

namespace MiniGameFramework.Configuration
{
    public interface IConfig
    {
        public ILogger ConfigureFromFile(string filePath);
    }
}