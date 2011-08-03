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

namespace _6_Model_Camera {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        Matrix view;
        Matrix projection;
        Matrix world;
        Model robot;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            base.Initialize();
        }

        protected override void LoadContent() {
            robot = Content.Load<Model>("Robot");

            view = Matrix.CreateLookAt(
                Vector3.Transform(Vector3.Zero, robot.Bones["CameraPosition"].Transform),
                Vector3.Transform(Vector3.Zero, robot.Bones["CameraTarget"].Transform),
                robot.Bones["CameraPosition"].Transform.Up
            );

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 1f, 0.1f, 1000f);

            world = Matrix.Identity;
        }

        protected override void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (BasicEffect effect in robot.Meshes["Robot"].Effects) {
                effect.EnableDefaultLighting();
                effect.EmissiveColor = Color.White.ToVector3();
                effect.View = view;
                effect.Projection = projection;
                effect.World = robot.Bones["Robot"].Transform * world;
            }
            robot.Meshes["Robot"].Draw();

            base.Draw(gameTime);
        }
    }
}
