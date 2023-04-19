using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using System.Diagnostics;

namespace MiniGameFramework.Models.GameObjects
{
    public abstract class GameObject
    {
        private static ILogger? _logger;
        public GameObject(string name, Position objectPosition, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Name = name;
            ObjectPosition = objectPosition;
        }
        public string Name { get; set; }
        public Position ObjectPosition { get; set; }

       
    }
}
