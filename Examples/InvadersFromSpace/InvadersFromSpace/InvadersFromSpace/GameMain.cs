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
        
        public static Rectangle Screen;

        Level lvl;
        Background bg;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            Screen.Width = graphics.PreferredBackBufferWidth;
            Screen.Height = graphics.PreferredBackBufferHeight;

            lvl = new Level();
            bg = new Background();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites.SpriteSheet = Content.Load<Texture2D>("SpriteSheet");
            Sprites.Starfield = Content.Load<Texture2D>("Starfield");
            Sprites.ScoreFont = Content.Load<SpriteFont>("ScoreFont");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            bg.Update(gameTime);
            lvl.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            bg.Draw(spriteBatch);
            lvl.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
