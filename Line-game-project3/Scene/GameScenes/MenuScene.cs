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

namespace Scene.GameScenes
{
    class MenuScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        public MenuScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }
        public void Load()
        {
            //nothing
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.ChangeScene(new MainGameScene(contentManager, sceneManager));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //nothing
        }


    }
}
