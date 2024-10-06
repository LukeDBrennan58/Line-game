using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Line_game_project3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static float screenHeight;
        public static float screenWidth;

        public Character red;
        public Character blue;
        public GameObject redLine;

        bool testvar = true;

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
            redLine = new GameObject();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            red.sprite = Content.Load<Texture2D>("red-player");
            blue.sprite = Content.Load<Texture2D>("blue-player");
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            movementController(red);
            redController();
            movementController(blue);


            if(Keyboard.GetState().IsKeyDown(Keys.Space) && testvar)
            {
                //red.rot += (float)Math.PI/2;
                testvar = false;
            }
            if(!Keyboard.GetState().IsKeyDown(Keys.Space) && !testvar)
            {
                testvar = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 15, 2));


            _spriteBatch.Begin();
            _spriteBatch.Draw(red.sprite, red.pos, null, Color.White, red.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            _spriteBatch.Draw(blue.sprite, blue.pos, null, Color.White, blue.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void movementController(Character character)
        {
            KeyboardState keys = Keyboard.GetState();


            if (keys.IsKeyDown(character.mKeys["up"]) && keys.IsKeyDown(character.mKeys["down"]))
            {
                updateMovement(null, 0);
            }
            else if (keys.IsKeyDown(character.mKeys["up"]))
            {
                updateMovement(null, -1);
            }
            else if (keys.IsKeyDown(character.mKeys["down"]))
            {
                updateMovement(null, 1);
            }
            else
            {
                updateMovement(null, 0);
            }

            if (keys.IsKeyDown(character.mKeys["right"]) && keys.IsKeyDown(character.mKeys["left"]))
            {
                updateMovement(0, null);
            }
            else if (keys.IsKeyDown(character.mKeys["right"]))
            {
                updateMovement(1, null);
            }
            else if (keys.IsKeyDown(character.mKeys["left"]))
            {
                updateMovement(-1, null);
            }
            else
            {
                updateMovement(0, null);
            }

            character.move();

            void updateMovement(Nullable<int> x, Nullable<int> y)
            {
                if (x != null) 
                {
                    character.movement.X = (int)x;
                }
                else if (y != null)
                {
                    character.movement.Y = (int)y;
                }
            }
        }

        protected void redController()
        {
             
        }
    }
}
