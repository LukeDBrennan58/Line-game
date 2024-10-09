using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class Util
    {
        public static bool IsBetween(float x, float a, float b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            return x > min && x < max;
        }
    }
}
