using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class Coin : Consumable
    {

        public Coin(Vector2 pos, Coin coinP)
        {
            sprite = coinP.sprite;
            radius = coinP.radius;
            oRadius = coinP.oRadius;
            dim = coinP.dim;
            this.pos = pos;
        }

        public Coin() { }

    }
}
