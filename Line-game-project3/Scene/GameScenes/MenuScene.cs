using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line_game_project3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Scene.GameScenes
{
    class MenuScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        private float screenWidth;
        private float screenHeight;

        private int button1Width;
        private int button1Height;

        public MenuScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }
        public void Load()
        {
            screenHeight = Game1.screenHeight;
            screenWidth = Game1.screenWidth;

            button1Width = 100;
            button1Height = 50;
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MainGameScene.mainGameStartTime = gameTime.TotalGameTime.Milliseconds;
                sceneManager.ChangeScene(new MainGameScene(contentManager, sceneManager));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle(new RectangleF());
        }


    }
}
