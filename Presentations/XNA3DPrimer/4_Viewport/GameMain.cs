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

namespace _4_Viewport {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        VertexPositionColor[] diamond;
        FollowCamera followCamera;
        Camera camera;
        Matrix shadow;
        Vector3 orbitAxis;

        Viewport viewleft;
        Viewport viewRight;
        Viewport viewFull;

        float orbitDistance;
        float orbitRotation;
        float orbitSpin;
        float orbitScale;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 900;
            graphics.ApplyChanges();

            followCamera = new FollowCamera();
            camera = new Camera();

            viewleft = new Viewport() {
                Height = 450,
                Width = 450,
                X = 0,
                Y = 0
            };

            viewRight = new Viewport() {
                Height = 450,
                Width = 450,
                X = 450,
                Y = 0
            };

            viewFull = graphics.GraphicsDevice.Viewport;

            Plane floor = new Plane(Vector3.Up, 2.2f);
            Vector3 light = new Vector3(1f, 1f, 0f);
            shadow = Matrix.CreateShadow(light, floor);

            orbitAxis = new Vector3(1f, 0, 1f);
            orbitAxis.Normalize();
            orbitDistance = 2f;
            orbitRotation = 0f;
            orbitSpin = 0f;
            orbitScale = .2f;
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

            orbitRotation += MathHelper.PiOver2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            orbitRotation = MathHelper.WrapAngle(orbitRotation);

            orbitSpin += MathHelper.TwoPi * (float)gameTime.ElapsedGameTime.TotalSeconds;
            orbitSpin = MathHelper.WrapAngle(orbitSpin);

            followCamera.Update(orbitScale, orbitDistance, orbitRotation, orbitAxis);
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Viewport = viewFull;
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            graphics.GraphicsDevice.VertexDeclaration = new VertexDeclaration(
                graphics.GraphicsDevice, VertexPositionColor.VertexElements);

            graphics.GraphicsDevice.Viewport = viewleft;
            DrawScene(camera);

            graphics.GraphicsDevice.Viewport = viewRight;
            DrawScene(followCamera);

            base.Draw(gameTime);
        }

        private void DrawScene(ICamera currentCamera) {
            BasicEffect effect = new BasicEffect(graphics.GraphicsDevice, null);
            effect.VertexColorEnabled = true;
            effect.Projection = currentCamera.Projection;
            effect.View = currentCamera.View;
            effect.World = currentCamera.World;

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Begin();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
                pass.End();
            }
            effect.End();

            Matrix satellite = Matrix.Identity
                             * Matrix.CreateScale(orbitScale)
                             * Matrix.CreateRotationY(orbitSpin)
                             * Matrix.CreateTranslation(0f, orbitDistance, 0f)
                             * Matrix.CreateFromAxisAngle(orbitAxis, orbitRotation);

            effect.View = satellite * currentCamera.View;

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Begin();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
                pass.End();
            }
            effect.End();

            // Render Shadow Effects
            effect.EnableDefaultLighting();
            effect.VertexColorEnabled = false;
            effect.EmissiveColor = Color.Black.ToVector3();

            effect.View = shadow * currentCamera.View;
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Begin();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
                pass.End();
            }
            effect.End();

            effect.View = satellite * shadow * currentCamera.View;
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Begin();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
                pass.End();
            }
            effect.End();
        }
    }
}
