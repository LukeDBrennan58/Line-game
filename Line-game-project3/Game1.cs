using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MonoGame.Extended;
using MonoGame.Extended.VectorDraw;

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
        public RedLine redLine;

        public 

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
            redLine = new RedLine(red);

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
            movementController(blue);

            updateLine();
            calculateBlueOnLine();

            if(Keyboard.GetState().IsKeyDown(Keys.C) && !red.action1)
            {
                red.action1 = true;
                var temp = redLine.pos;
                redLine.pos = red.pos;
                red.pos = temp;
            } else if(!Keyboard.GetState().IsKeyDown(Keys.C) && red.action1)
            {
                red.action1 = false;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 15, 2));


            _spriteBatch.Begin();
            _spriteBatch.Draw(red.sprite, red.pos, null, Color.White, red.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            _spriteBatch.Draw(blue.sprite, blue.pos, null, Color.White, blue.rot, new Vector2(13, 23), 1, new SpriteEffects(), 0);
            _spriteBatch.DrawLine(redLine.pos, red.pos, Color.Red, 1, 0);
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

        protected void calculateBlueOnLine()
        {
            
            var area = Math.Abs((0.5) *
                (red.pos.X * (redLine.pos.Y - blue.pos.Y)
                + redLine.pos.X * (blue.pos.Y - red.pos.Y)
                + blue.pos.X * (red.pos.Y - redLine.pos.Y)));
            if(area / redLine.length < 5.5f
                    && (Util.IsBetween(blue.pos.X, red.pos.X, redLine.pos.X)
                    || Util.IsBetween(blue.pos.Y, red.pos.Y, redLine.pos.Y)))
            {
                //TODO: Blue dies
            } 

        }

        protected void updateLine()
        {
            redLine.length = (int)Vector2.Distance(red.pos, redLine.pos);
            var stretch = (float)Math.Pow(1.01, redLine.length / 2);
            if (redLine.length != 0)
            {
                redLine.pos.X += redLine.spd * (red.pos.X - redLine.pos.X) * stretch / redLine.length;
                redLine.pos.Y += redLine.spd * (red.pos.Y - redLine.pos.Y) * stretch / redLine.length;
            }
        }
    }
}
