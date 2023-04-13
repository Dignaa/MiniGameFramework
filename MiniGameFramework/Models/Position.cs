using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Models
{
    public class Position
    {
        public Position(float x, float? y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float? Y { get; set; }

        public float GetDistance(Position end)
        {
            float dx = end.X - X;
            float dy = end.Y.GetValueOrDefault() - Y.GetValueOrDefault();
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
