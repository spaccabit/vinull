using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace InvadersFromSpace {

    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screen;

        Level lvl;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            screen.Width = graphics.PreferredBackBufferWidth;
            screen.Height = graphics.PreferredBackBufferHeight;

            lvl = new Level(screen);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet.Texture = Content.Load<Texture2D>("SpriteSheet");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            lvl.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            lvl.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
