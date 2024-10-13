using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Util
    {
        private static int screenWidth;
        private static int screenHeight;

        public static void SetScreen(GraphicsDevice graphicsDevice)
        {
            screenWidth = graphicsDevice.Viewport.Width;
            screenHeight = graphicsDevice.Viewport.Height;
        }
        public static Vector2 GetScreen()
        {
            return new(screenWidth, screenHeight);
        }

        public static bool IsBetween(float x, float a, float b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            return x > min - 10 && x < max + 10;
        }

        public static Vector2 PointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
