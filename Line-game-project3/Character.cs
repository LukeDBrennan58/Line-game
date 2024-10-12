using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scene.GameScenes;
using System.Diagnostics;

namespace Line_game_project3
{
    public class Character : GameObject
    {
        public Dictionary<string, Keys> mKeys = new Dictionary<string, Keys>();
        public string col;
        public short score;
        public short life;

        public Character(string col)
        {
            this.score = 0;
            this.life = 10000;
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

        public void Move()
        {
            float angleOffset = Math.Abs(movement.X) + Math.Abs(movement.Y) == 2 ? 0.7071f : 1f;
            pos.X += movement.X * spd * angleOffset;
            pos.Y += movement.Y * spd * angleOffset;

            if (!(movement.X == 0 && movement.Y == 0))
            {
                rot = (float)(Math.Atan2(movement.Y, movement.X) + Math.PI / 2);
            }
        }

        public void MovementController()
        {
            KeyboardState keys = Keyboard.GetState();


            if (keys.IsKeyDown(mKeys["up"]) && keys.IsKeyDown(mKeys["down"]))
            {
                updateMovement(null, 0);
            }
            else if (keys.IsKeyDown(mKeys["up"]))
            {
                updateMovement(null, -1);
            }
            else if (keys.IsKeyDown(mKeys["down"]))
            {
                updateMovement(null, 1);
            }
            else
            {
                updateMovement(null, 0);
            }

            if (keys.IsKeyDown(mKeys["right"]) && keys.IsKeyDown(mKeys["left"]))
            {
                updateMovement(0, null);
            }
            else if (keys.IsKeyDown(mKeys["right"]))
            {
                updateMovement(1, null);
            }
            else if (keys.IsKeyDown(mKeys["left"]))
            {
                updateMovement(-1, null);
            }
            else
            {
                updateMovement(0, null);
            }

            Move();
            if(col == "blue")
            {
                if(pos.X > Game1.screenWidth + 15)
                {
                    pos.X = -15;
                }
                else if(pos.X < -15)
                {
                    pos.X = Game1.screenWidth + 15;
                }

                if(pos.Y > Game1.screenHeight + 15)
                {
                    pos.Y = -15;
                }
                else if(pos.Y < -15)
                {
                    pos.Y = Game1.screenHeight + 15;
                }
            }

            void updateMovement(int? x, int? y)
            {
                if (x != null)
                {
                    movement.X = (int)x;
                }
                else if (y != null)
                {
                    movement.Y = (int)y;
                }
            }
        }

        public void UpdateLife()
        {
            life -= 20;

            if(life <= 0)
            {
                MainGameScene.gameGoing = false;
            }
        }
    }
}
