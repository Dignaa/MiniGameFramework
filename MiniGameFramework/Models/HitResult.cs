
using MiniGameFramework.Models.GameObjects;
using MiniGameFramework.Models.Items;

namespace MiniGameFramework.Models
{
    public class HitResult
    {
        public WorldObject? HitObject { get; set; }
        public List<Item>? HitItems { get; set; }
    }
}
