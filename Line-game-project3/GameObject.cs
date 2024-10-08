using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class GameObject
    {
        public Vector2 pos;
        public Texture2D sprite;

        public float spd;
        public float rot;
        public Vector2 movement;

        public bool action1 = false;
        public bool action2 = false;
    }
}
