using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class Character : GameObject
    {
        public Dictionary<string, Keys> mKeys = new Dictionary<string, Keys>();
        public string col;

        public Character(string col)
        {
            this.col = col;
            movement = new Vector2(0, 0);

            if (col == "red")
            {
                mKeys.Add("up", Keys.W);
                mKeys.Add("down", Keys.S);
                mKeys.Add("right", Keys.D);
                mKeys.Add("left", Keys.A);

                spd = 4;
                pos = new Vector2(Game1.screenWidth / 4, Game1.screenHeight / 2);

                rot = (float)Math.PI / 2;
            }
            else if (col == "blue")
            {
                mKeys.Add("up", Keys.I);
                mKeys.Add("down", Keys.K);
                mKeys.Add("right", Keys.L);
                mKeys.Add("left", Keys.J);

                spd = 4;
                pos = new Vector2(Game1.screenWidth * 3 / 4, Game1.screenHeight / 2);

                rot = (float)Math.PI * 3 / 2;
            }
        }

        public void move()
        {
            float angleOffset = Math.Abs(movement.X) + Math.Abs(movement.Y) == 2 ? 0.7071f : 1f;
            pos.X += movement.X * spd * angleOffset;
            pos.Y += movement.Y * spd * angleOffset;

            if (!(movement.X == 0 && movement.Y == 0))
            {
                rot = (float)(Math.Atan2(movement.Y, movement.X) + Math.PI / 2);
            }
        }

    }
}
