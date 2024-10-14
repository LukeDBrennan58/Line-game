using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using Scene;
using Scene.GameScenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Object
{
    public class Button
    {
        public Vector2 pos;
        public Vector2 dim;
        public ColorCycle border;
        public ColorCycle fill;

        public string name;

        public Button(Vector2 pos, Vector2 dim, ColorCycle fill, ColorCycle border, string name)
        {
            this.pos = pos;
            this.dim = dim;
            this.fill = fill;
            this.border = border;
            this.name = name;
        }

        public Button(int posX, int posY, int width, int height, ColorCycle fill, ColorCycle border, string name)
        {
            this.pos = new Vector2(posX, posY);
            this.dim = new Vector2(width, height);
            this.fill = fill;
            this.border = border;
            this.name = name;
        }

        public RectangleF GetRectangle()
        {
            return new(pos.X - dim.X/2, pos.Y - dim.Y/2, dim.X, dim.Y);
        }

        public IScene GetNewScene(ContentManager contentManager, SceneManager sceneManager, double gameTime)
        {
            if(name == "startGame")
            {
                return new MainGameScene(contentManager, sceneManager, gameTime);
            }
            else
            {
                return null;
            }
               
        }

        public static implicit operator string(Button button)
        {
            return button.name;
        }

    }
}
