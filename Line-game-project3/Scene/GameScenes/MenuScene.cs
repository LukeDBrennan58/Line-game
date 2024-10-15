using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private SpriteFont font1;

        private float screenWidth;
        private float screenHeight;

        private HashSet<Button> buttons;

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
            font1 = contentManager.Load<SpriteFont>("font1");

            screenHeight = Util.GetScreen().Y;
            screenWidth = Util.GetScreen().X;

            backGroundColor = JsonProps.GetColor("background");
            buttonColor = new(JsonProps.GetColor("button"), JsonProps.GetColor("lightButton"));
            borderColor = new(Color.Black, new Color(60,60,60));

            Button button1 = new((int)screenWidth / 3, (int)screenHeight * 3 / 5,
                    180, 60,
                    new(buttonColor), new(borderColor),
                    "Start Game");

            Button button2 = new((int)screenWidth * 2 / 3, (int)screenHeight * 3 / 5,
                    180, 60,
                    new(buttonColor), new(borderColor),
                    "Extra Button");

            buttons = new HashSet<Button>();
            buttons.Add(button1);
            buttons.Add(button2);
        }
        public void Update(GameTime gameTime)
        {
            foreach(Button button in buttons)
            {
                if (button.GetRectangle().Contains(Mouse.GetState().Position.ToVector2()))
                {
                    button.fill.SetColor(1);
                    button.border.SetColor(1);

                    if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        sceneManager.ChangeScene(button.GetNewScene(contentManager, sceneManager, gameTime.TotalGameTime.Milliseconds));
                    }
                }
                else
                {
                    button.fill.SetColor(0);
                    button.border.SetColor(0);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font1, "Line Game", new(screenWidth / 2, screenHeight / 3), Color.Red, 0, Util.Origin(font1, "Line Game"), 3, new SpriteEffects(), 0);
            foreach(Button button in buttons)
            {
                spriteBatch.FillRectangle(button.GetRectangle(), button.fill);
                spriteBatch.DrawRectangle(button.GetRectangle(), button.border, 3, 0);
                spriteBatch.DrawString(font1, button.text, button.pos, button.border, 0, Util.Origin(font1, button.text), 2, new SpriteEffects(), 0);
            }
            spriteBatch.End();
        }


    }
}
