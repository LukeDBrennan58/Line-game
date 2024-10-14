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

        private Color backGroundColor;
        private ColorCycle buttonColor;
        private ColorCycle borderColor;

        public MenuScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }
        public void Load()
        {
            screenHeight = Util.GetScreen().Y;
            screenWidth = Util.GetScreen().X;

            backGroundColor = JsonProps.GetColor("background");
            buttonColor = new(JsonProps.GetColor("button"), JsonProps.GetColor("lightButton"));
            borderColor = new(Color.Black, Color.DarkGray);

            button1 = new((int)screenWidth / 2, (int)screenHeight * 3 / 5,
                    180, 60,
                    buttonColor, borderColor);
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MainGameScene.mainGameStartTime = gameTime.TotalGameTime.Milliseconds;
                sceneManager.ChangeScene(new MainGameScene(contentManager, sceneManager));
            }

            if(button1.GetRectangle().Contains(Mouse.GetState().Position.ToVector2()))
            {
                button1.fill.SetColor(1);
                button1.border.SetColor(1);
            }
            else
            {
                button1.fill.SetColor(0);
                button1.border.SetColor(0);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.FillRectangle(button1.GetRectangle(), button1.fill);
            spriteBatch.DrawRectangle(button1.GetRectangle(), button1.border, 3, 0);
            spriteBatch.End();
        }


    }
}
