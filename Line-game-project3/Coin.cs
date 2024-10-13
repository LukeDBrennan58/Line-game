﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class Coin : GameObject
    {

        public Coin(Vector2 pos, Coin coinP)
        {
            this.sprite = coinP.sprite;
            this.radius = coinP.radius;
            this.oRadius = coinP.oRadius;
            this.dim = coinP.dim;
            this.pos = pos;
        }

        public Coin() { }

    }
}
