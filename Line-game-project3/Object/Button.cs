using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Object
{
    public class Button
    {
        public Vector2 pos;
        public Vector2 dim;
        public ColorCycle border;
        public ColorCycle fill;

        public Button(Vector2 pos, Vector2 dim, ColorCycle fill, ColorCycle border)
        {
            this.pos = pos;
            this.dim = dim;
            this.fill = fill;
            this.border = border;
        }

        public Button(int posX, int posY, int width, int height, ColorCycle fill, ColorCycle border)
        {
            this.pos = new Vector2(posX, posY);
            this.dim = new Vector2(width, height);
            this.fill = fill;
            this.border = border;
        }

        public RectangleF GetRectangle()
        {
            return new(pos.X - dim.X/2, pos.Y - dim.Y/2, dim.X, dim.Y);
        }

    }
}
