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

namespace _1_3D_Overview {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        VertexPositionColor[] diamond;
        Matrix cameraProjection, cameraView;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            cameraProjection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 1f, 1f, 10000f);

            cameraView = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), Vector3.Zero, Vector3.Up);

            base.Initialize();
        }

        protected override void LoadContent() {
            diamond = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(0, 0, 1), Color.Red),
                new VertexPositionColor(new Vector3(-1, 0, 0), Color.Green),
                new VertexPositionColor(new Vector3(0, 1, 0), Color.Blue),
                new VertexPositionColor(new Vector3(0, 0, -1), Color.Red),
                new VertexPositionColor(new Vector3(0, 1, 0), Color.Green),
                new VertexPositionColor(new Vector3(1, 0, 0), Color.Blue),
                new VertexPositionColor(new Vector3(0, 0, 1), Color.Red),
                new VertexPositionColor(new Vector3(0, -1, 0), Color.Green),
                new VertexPositionColor(new Vector3(-1, 0, 0), Color.Blue),
                new VertexPositionColor(new Vector3(0, -1, 0), Color.Red),
                new VertexPositionColor(new Vector3(0, 0, -1), Color.Green),
                new VertexPositionColor(new Vector3(1, 0, 0), Color.Blue),
            };
        }

        protected override void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            graphics.GraphicsDevice.VertexDeclaration = new VertexDeclaration(
                graphics.GraphicsDevice, VertexPositionColor.VertexElements);

            BasicEffect effect = new BasicEffect(graphics.GraphicsDevice, null);
            effect.VertexColorEnabled = true;
            effect.Projection = cameraProjection;
            effect.View = cameraView;
            effect.World = Matrix.Identity;

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Begin();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
