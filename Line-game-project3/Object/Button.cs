using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class Button
    {
        public Vector2 pos;
        public Vector2 dim;
        public Color color;

        public Button(Vector2 pos, Vector2 dim)
        {
            this.pos = pos;
            this.dim = dim;
            this.color = Color.Black;
        }

        public Button(int posX, int posY, int width, int height)
        {
            this.pos = new Vector2(posX, posY);
            this.dim = new Vector2(width, height);
            this.color = Color.Black;
        }

        public RectangleF GetRectangle()
        {
            return new(pos.X - dim.X/2, pos.Y - dim.Y/2, dim.X, dim.Y);
        }
    }
}
