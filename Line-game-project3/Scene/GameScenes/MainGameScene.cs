using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.VectorDraw;
using MonoGame.Extended.Timers;
using Microsoft.Xna.Framework.Content;
using Scene;
using Line_game_project3;

namespace Scene.GameScenes
{
    public class MainGameScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static float screenHeight;
        public static float screenWidth;

        public Character red;
        public Character blue;
        public RedLine redLine;

        private Color testColor;

        public MainGameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Load()
        {
            testColor = Color.White;

            screenHeight = Game1.screenHeight;
            screenWidth = Game1.screenWidth;

            red = new Character("red");
            blue = new Character("blue");
            redLine = new RedLine(red);

            red.sprite = contentManager.Load<Texture2D>("red-player");
            blue.sprite = contentManager.Load<Texture2D>("blue-player");
        }
        public void Update(GameTime gameTime)
        {

            movementController(red);
            movementController(blue);

            updateLine();
            calculateBlueOnLine();

            if (Keyboard.GetState().IsKeyDown(Keys.C) && !red.action1)
            {
                red.action1 = true;
                var temp = redLine.pos;
                redLine.pos = red.pos;
                red.pos = temp;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.C) && red.action1)
            {
                red.action1 = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(red.sprite, red.pos, null, Color.White, red.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            spriteBatch.Draw(blue.sprite, blue.pos, null, testColor, blue.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            spriteBatch.DrawLine(redLine.pos, red.pos, Color.Red, 1, 0);
            spriteBatch.End();

        }

        protected void movementController(Character character)
        {
            KeyboardState keys = Keyboard.GetState();


            if (keys.IsKeyDown(character.mKeys["up"]) && keys.IsKeyDown(character.mKeys["down"]))
            {
                updateMovement(null, 0);
            }
            else if (keys.IsKeyDown(character.mKeys["up"]))
            {
                updateMovement(null, -1);
            }
            else if (keys.IsKeyDown(character.mKeys["down"]))
            {
                updateMovement(null, 1);
            }
            else
            {
                updateMovement(null, 0);
            }

            if (keys.IsKeyDown(character.mKeys["right"]) && keys.IsKeyDown(character.mKeys["left"]))
            {
                updateMovement(0, null);
            }
            else if (keys.IsKeyDown(character.mKeys["right"]))
            {
                updateMovement(1, null);
            }
            else if (keys.IsKeyDown(character.mKeys["left"]))
            {
                updateMovement(-1, null);
            }
            else
            {
                updateMovement(0, null);
            }

            character.move();

            void updateMovement(int? x, int? y)
            {
                if (x != null)
                {
                    character.movement.X = (int)x;
                }
                else if (y != null)
                {
                    character.movement.Y = (int)y;
                }
            }
        }

        protected void calculateBlueOnLine()
        {

            var area = Math.Abs(0.5 *
                (red.pos.X * (redLine.pos.Y - blue.pos.Y)
                + redLine.pos.X * (blue.pos.Y - red.pos.Y)
                + blue.pos.X * (red.pos.Y - redLine.pos.Y)));
            if (redLine.length != 0
                    && area / redLine.length < 5.5f
                    && Util.IsBetween(blue.pos.X, red.pos.X, redLine.pos.X)
                    && Util.IsBetween(blue.pos.Y, red.pos.Y, redLine.pos.Y))
            {
                //testColor = Color.Black;
                sceneManager.ChangeScene(new MenuScene(contentManager, sceneManager));
            }
            else
            {
                //testColor = Color.White;
            }
            

        }

        protected void updateLine()
        {
            redLine.length = (int)Vector2.Distance(red.pos, redLine.pos);
            var stretch = (float)Math.Pow(1.01, redLine.length / 2);
            if (redLine.length != 0)
            {
                redLine.pos.X += redLine.spd * (red.pos.X - redLine.pos.X) * stretch / redLine.length;
                redLine.pos.Y += redLine.spd * (red.pos.Y - redLine.pos.Y) * stretch / redLine.length;
            }
        }




    }
}
