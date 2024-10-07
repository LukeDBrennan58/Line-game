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

        public RedLine(Character red)
        {
            pos = red.pos;
            length = 0;
        }
    }
}
