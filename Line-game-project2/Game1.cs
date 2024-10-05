using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Line_game_project2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        Texture2D redSprite;


        public static float screenHeight;
        public static float screenWidth;

        public Character red;
        public Character blue;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            screenHeight = GraphicsDevice.Viewport.Bounds.Height;
            screenWidth = GraphicsDevice.Viewport.Bounds.Width;

            red = new Character("red");
            blue = new Character("blue");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            redSprite = Content.Load<Texture2D>("testCircle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                red.movement.Y = -1;
            } else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                red.movement.Y = 1;
            } else
            {
                red.movement.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                red.movement.X = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                red.movement.X = -1;
            }
            else
            {
                red.movement.X = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 15, 2));
            

            _spriteBatch.Begin();
            _spriteBatch.Draw(redSprite, red.pos, null, Color.White, 0, new Vector2(), 0.01f, new SpriteEffects(), 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public class Character
        {
            public string col;
            public float spd;
            public Vector2 pos;

            public bool movingHorz;
            public bool movingVert;

            public Vector2 movement;

            public Character(string col)
            {
                this.col = col;
                movingHorz = false;
                movingVert = false;
                movement = new Vector2(0, 0);

                if (col == "red")
                {
                    spd = 3;
                    pos = new Vector2(screenWidth / 4, screenHeight / 2);
                } else if (col == "blue")
                {
                    spd = 3;
                    pos = new Vector2(screenWidth * 3 / 4, screenHeight / 2);

                }
            }

            public void move()
            {
                float angleOffset = (float)(1 / Math.Sqrt(Math.Abs(movement.X) + Math.Abs(movement.Y)));
                pos.X += movement.X * angleOffset;
                pos.Y += movement.Y * angleOffset;
            }

        }
    }
}
