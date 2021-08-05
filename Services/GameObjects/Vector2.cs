using System;

namespace SoccerPong.Services.GameObjects
{
    public class Vector2
    {
        public double X;
        public double Y;

        static Random _rnd = new ();

        public Vector2(double value) => X = Y = value;
        public Vector2(double X, double Y) => (this.X, this.Y) = (X, Y);

        public static Vector2 RandomQuad()
            => new Vector2(_rnd.Next(2) == 0 ? -1 : 1, _rnd.Next(2) == 0 ? -1 : 1).Normalize();

        public Vector2 Clone() => new Vector2(this.X, this.Y);

        public static Vector2 operator +(Vector2 v, double d)
            => new Vector2(v.X+d, v.Y+d);
        public static Vector2 operator -(Vector2 v, double d)
            => new Vector2(v.X-d, v.Y-d);
        public static Vector2 operator *(Vector2 v, double d)
            => new Vector2(v.X*d, v.Y*d);
        public static Vector2 operator /(Vector2 v, double d)
            => new Vector2(v.X/d, v.Y/d);

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X+v2.X, v1.Y+v2.Y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X-v2.X, v1.Y-v2.Y);
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X*v2.X, v1.Y*v2.Y);
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X/v2.X, v1.Y/v2.Y);

        public double LengthSq() => X*X + Y*Y;
        public double Length() => Math.Sqrt(LengthSq());

        public Vector2 Normalize()
        {
            var len = Length();
            X /= len;
            Y /= len;
            return this;
        }
    }
}