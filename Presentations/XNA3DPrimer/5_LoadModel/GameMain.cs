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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace _5_LoadModel {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        Camera camera;
        Model robot;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            camera = new Camera();
            Content.RootDirectory = "Content";

            base.Initialize();
        }

        protected override void LoadContent() {
            robot = Content.Load<Model>("Robot");
        }

        protected override void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();

            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (BasicEffect effect in robot.Meshes["Robot"].Effects) {
                effect.EnableDefaultLighting();
                effect.EmissiveColor = Color.White.ToVector3();
                effect.View = camera.View;
                effect.Projection = camera.Projection;
                effect.World = robot.Bones["Robot"].Transform * camera.World;
            }
            robot.Meshes["Robot"].Draw();

            base.Draw(gameTime);
        }
    }
}
