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
using System.Diagnostics;
using Object;
using Tools;
using Controller;

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

        private CoinController coinController;

        public Texture2D coinSprite;
        public SpriteFont font1;

        public double mainGameStartTime;
        public static bool gameGoing;

        private Color testColor;

        public MainGameScene(ContentManager contentManager, SceneManager sceneManager, double gameTime)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
            this.mainGameStartTime = gameTime;

            Vector2 screen = Util.GetScreen();
            screenWidth = screen.X;
            screenHeight = screen.Y;

            coins = new HashSet<Coin>();
            testColor = Color.White;
            gameGoing = true;

        }

        public void Load()
        {
            red = new Character("red");
            blue = new Character("blue");

            redLine = new RedLine(red);
            coinP = new Coin();
            coinController = new CoinController();

            red.SetSprite(contentManager.Load<Texture2D>("red-player"));
            blue.SetSprite(contentManager.Load<Texture2D>("blue-player"));
            coinP.SetSprite(contentManager.Load<Texture2D>("coin"));
            font1 = contentManager.Load<SpriteFont>("font1");

            coinController.lastCoinTime = mainGameStartTime;
            coinController.coinP = coinP;
            
        }
        public void Update(GameTime gameTime)
        {
            if(gameGoing)
            {
                red.MovementController();
                blue.MovementController();
                redLine.UpdateLine();
                blue.UpdateLife();
                coinController.CoinLogic(gameTime.TotalGameTime.TotalMilliseconds, blue);
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
            foreach(Coin coin in coinController.coins)
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
            coinController.Clean();
        }
    }
}
