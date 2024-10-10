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
using System.Diagnostics;

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

        public bool gameGoing;

        private Color testColor;

        public MainGameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Load()
        {
            testColor = Color.White;

            gameGoing = true;

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
            if(gameGoing)
            {
                red.MovementController();
                blue.MovementController();
                redLine.UpdateLine();
            }

            calculateBlueOnLine();

            Debug.Write(gameGoing + " " + red.spd);

            if (Keyboard.GetState().IsKeyDown(Keys.C) && !red.action1)
            {
                red.action1 = true;
                (red.pos, redLine.pos) = (redLine.pos, red.pos);
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

            if(!gameGoing)
            {
                //Died text
            }
            spriteBatch.End();

        }

        protected void calculateBlueOnLine()
        {

            var area = Math.Abs(0.5 *
                (red.pos.X * (redLine.pos.Y - blue.pos.Y)
                + redLine.pos.X * (blue.pos.Y - red.pos.Y)
                + blue.pos.X * (red.pos.Y - redLine.pos.Y)));
            if (gameGoing
                    && redLine.length != 0
                    && area / redLine.length < 5.5f
                    && Util.IsBetween(blue.pos.X, red.pos.X, redLine.pos.X)
                    && Util.IsBetween(blue.pos.Y, red.pos.Y, redLine.pos.Y))
            {
                //testColor = Color.Black;
                gameGoing = false;
            }
            else
            {
                //testColor = Color.White;
            }

            if(!gameGoing
                    && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.ChangeScene(new MenuScene(contentManager, sceneManager));
            }
            

        }
    }
}
