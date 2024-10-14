using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class ColorCycle
    {
        private Color reference;
        private List<Color> colors;

        public ColorCycle(List<Color> colors)
        {
            this.colors = colors;
            this.reference = colors[0];
        }

        public ColorCycle(Color color1, Color color2)
        {
            colors = new List<Color> { color1, color2 };
            this.reference = colors[0];
        }

        public void SetColor(int index)
        {
            reference = colors[index];
        }

        public static implicit operator Color(ColorCycle colorCycle)
        {
            return colorCycle.reference;
        }
    }
}
