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
using Object;
using Tools;

namespace Line_game_project3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SceneManager sceneManager;
        private Color backGroundColor;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            sceneManager = new();

            backGroundColor = JsonProps.GetColor("background");
        }

        protected override void Initialize()
        {
            Util.SetScreen(GraphicsDevice);
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
            GraphicsDevice.Clear(backGroundColor);

            sceneManager.GetCurrentScene().Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
