using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class RedLine : GameObject
    {
        public int length;
        public Character red;

        public RedLine(Character red)
        {
            this.red = red;
            spd = red.spd/6;
            pos = red.pos;
            length = 0;
        }
        public void UpdateLine()
        {
            length = (int)Vector2.Distance(red.pos, pos);
            var stretch = (float)Math.Pow(1.01, length / 2);
            if (length != 0)
            {
                pos.X += spd * (red.pos.X - pos.X) * stretch / length;
                pos.Y += spd * (red.pos.Y - pos.Y) * stretch / length;
            }
        }
    }
}
