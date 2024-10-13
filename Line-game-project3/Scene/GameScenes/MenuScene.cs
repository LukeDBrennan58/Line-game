using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Object;
using Tools;

namespace Scene.GameScenes
{
    class MenuScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        private float screenWidth;
        private float screenHeight;

        private Button button1;

        public MenuScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }
        public void Load()
        {
            screenHeight = Util.GetScreen().Y;
            screenWidth = Util.GetScreen().X;

            button1 = new((int)screenWidth / 2, (int)screenHeight * 3 / 5, 180, 60);
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
            spriteBatch.Begin();
            spriteBatch.DrawRectangle(button1.GetRectangle(), button1.color, 3);
            spriteBatch.End();
        }


    }
}
