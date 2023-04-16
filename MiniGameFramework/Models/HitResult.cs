
using MiniGameFramework.Models.GameObjects;
using MiniGameFramework.Models.Items;

namespace MiniGameFramework.Models
{
    public class HitResult
    {
        public WorldObject? HitReturnObject { get; set; }
        public List<Item>? HitReturnItems { get; set; }
    }
}
