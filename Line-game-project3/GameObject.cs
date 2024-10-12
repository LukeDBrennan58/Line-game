using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class GameObject
    {
        public Vector2 pos;
        public Texture2D sprite;
        public Vector2 dim;
        public float radius;
        public float oRadius;

        public float spd;
        public float rot;
        public Vector2 movement;

        public bool action1 = false;
        public bool action2 = false;

        public GameObject()
        {
            //
        }

        public void SetSprite(Texture2D sprite)
        {
            this.sprite = sprite;
            this.dim = Util.PointToVector(sprite.Bounds.Size);
            this.radius = dim.X / 2;
            this.oRadius = radius * 1.25f;
        }
        public Vector2 GetCenterOffset()
        {
            return new Vector2(radius, dim.Y - radius);
        }

    }
}
