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
        public static Random Rand;

        Level lvl;
        Background bg;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Rand = new Random();
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
            GameMessage.SetMessage("Press Enter\n    To Begin");
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites.SpriteSheet = Content.Load<Texture2D>("SpriteSheet");
            Sprites.Starfield = Content.Load<Texture2D>("Starfield");
            Sprites.ScoreFont = Content.Load<SpriteFont>("ScoreFont");
            Sprites.MessageFont = Content.Load<SpriteFont>("MessageFont");
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            bg.Update(gameTime);

            if (GameMessage.Active)
                GameMessage.Update();
            else
                lvl.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            bg.Draw(spriteBatch);
            lvl.Draw(spriteBatch);
            if (GameMessage.Active)
                GameMessage.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
