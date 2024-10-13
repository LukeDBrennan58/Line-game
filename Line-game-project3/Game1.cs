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
using Scene;
using Scene.GameScenes;

namespace Line_game_project3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Character red;
        public Character blue;
        public RedLine redLine;

        public SceneManager sceneManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            sceneManager = new();

        }

        protected override void Initialize()
        {
            Util.SetScreen(GraphicsDevice);
            JsonProps.Start();
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sceneManager.ChangeScene(new MenuScene(Content, sceneManager));
        }

        protected override void Update(GameTime gameTime)
        {

            sceneManager.GetCurrentScene().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 15, 2));

            sceneManager.GetCurrentScene().Draw(_spriteBatch);

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

            character.Move();

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
