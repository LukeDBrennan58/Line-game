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

        private static float screenHeight;
        private static float screenWidth;

        public Character red;
        public Character blue;
        public Coin coinP;
        public HashSet<Coin> coins;
        public RedLine redLine;

        public Texture2D coinSprite;
        public SpriteFont font1;

        public static double mainGameStartTime;
        public static bool gameGoing;

        private Color testColor;

        public MainGameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            coins = new HashSet<Coin>();
        }

        public void Load()
        {
            testColor = Color.White;

            gameGoing = true;

            screenHeight = Game1.screenHeight;
            screenWidth = Game1.screenWidth;

            red = new Character("red");
            blue = new Character("blue");

            coinP = new Coin();
            redLine = new RedLine(red);

            red.SetSprite(contentManager.Load<Texture2D>("red-player"));
            blue.SetSprite(contentManager.Load<Texture2D>("blue-player"));
            coinP.SetSprite(contentManager.Load<Texture2D>("coin"));
            font1 = contentManager.Load<SpriteFont>("font1");

            CoinController.lastCoinTime = mainGameStartTime;
            
        }
        public void Update(GameTime gameTime)
        {
            if(gameGoing)
            {
                red.MovementController();
                blue.MovementController();
                redLine.UpdateLine();
                blue.UpdateLife();
                CoinController.CoinLogic(gameTime.TotalGameTime.TotalMilliseconds, blue);
            }

            calculateBlueOnLine();

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
            spriteBatch.Draw(red.sprite, red.pos, null, Color.White, red.rot, red.GetCenterOffset(), 1, new SpriteEffects(), 0);
            spriteBatch.Draw(blue.sprite, blue.pos, null, testColor, blue.rot, blue.GetCenterOffset(), 1, new SpriteEffects(), 0);
            spriteBatch.DrawLine(redLine.pos, red.pos, Color.Red, 1, 0);
            foreach(Coin coin in CoinController.coins)
            {
                spriteBatch.Draw(coinP.sprite, coin.pos, null, Color.White, 0, coinP.GetCenterOffset(), 1, new SpriteEffects(), 0);
            }

            if(gameGoing)
            {
                spriteBatch.DrawLine(new Vector2(screenWidth * 2 / 3 - 50, screenHeight - 25),
                    new Vector2((screenWidth * 2 / 3) - 50 + (screenWidth / 3) * blue.life / 10000, screenHeight - 25),
                    Color.Blue, 3, 0);
                spriteBatch.DrawString(font1, blue.score.ToString(), new Vector2(screenWidth - 25, screenHeight - 25), Color.Blue);
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
                Clean();
                sceneManager.ChangeScene(new MenuScene(contentManager, sceneManager));
            }
            

        }
        private void Clean()
        {
            CoinController.Clean();
        }
    }
}
