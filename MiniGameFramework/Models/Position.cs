using MiniGameFramework.Logging;
using System.Diagnostics;

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

        /// <summary>
        /// Get distance between a creature and a given position
        /// </summary>
        /// <param name="end"></param>
        /// <returns>float Distance</returns>
        public float GetDistance(Position start, Position end)
        {
            float dx = end.X - start.X;
            float dy = end.Y.GetValueOrDefault() - start.Y.GetValueOrDefault();
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static bool operator ==(Position? p1, Position? p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (p1 is null || p2 is null)
            {
                return false;
            }

            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Position? p1, Position? p2)
        {
            return !(p1== p2);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Position))
                return false;
            else
                return this == (Position)obj;
        }
    }
}
