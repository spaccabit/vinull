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

namespace _7_Collision_Detection {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        BasicEffect effect;

        Camera camera;

        VertexPositionColor[] diamond;
        Vector3 diamondPosition = new Vector3(-3, 0, 9);
        float diamondRotation = 0f;
        float diamondScale = 3f;
        Matrix diamondTransform;
        BoundingSphere diamondSphere;

        Model robot;
        Vector3 robotPosition = new Vector3(0f);
        float robotRotation = 0f;
        Matrix robotTransform;

        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            Content.RootDirectory = "XNA_3D_Primer_Content";

            effect = new BasicEffect(graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;

            base.Initialize();
        }

        protected override void LoadContent() {
            robot = Content.Load<Model>("Robot");

            camera = new Camera(robot.Bones["CameraPosition"].Transform,
                                robot.Bones["CameraTarget"].Transform);

            effect.Projection = camera.Projection;
            effect.World = camera.World;

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

            diamondSphere = BoundingSphere.CreateFromPoints(
                from v in diamond select v.Position);
            diamondSphere.Center = diamondPosition;
            diamondSphere.Radius *= diamondScale;
        }

        protected override void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();
            float eTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();
            else if (keys.IsKeyDown(Keys.Left))
                robotRotation = MathHelper.WrapAngle(robotRotation + eTime * MathHelper.Pi);
            else if (keys.IsKeyDown(Keys.Right))
                robotRotation = MathHelper.WrapAngle(robotRotation - eTime * MathHelper.Pi);

            Vector3 newPosition = robotPosition;

            if (keys.IsKeyDown(Keys.Up))
                newPosition += Vector3.Transform(new Vector3(0, 0, 4 * eTime),
                                    Matrix.CreateRotationY(robotRotation));
            else if (keys.IsKeyDown(Keys.Down))
                newPosition += Vector3.Transform(new Vector3(0, 0, -4 * eTime),
                                    Matrix.CreateRotationY(robotRotation));

            Matrix newTransform = Matrix.CreateRotationY(robotRotation)
                                * Matrix.CreateTranslation(newPosition);

            BoundingSphere robotSphere = robot.Meshes["Robot"].BoundingSphere;
            robotSphere.Center = Vector3.Transform(robotSphere.Center, newTransform);

            if (!robotSphere.Intersects(diamondSphere))
                robotPosition = newPosition;

            robotTransform = Matrix.CreateRotationY(robotRotation)
                           * Matrix.CreateTranslation(robotPosition);

            diamondRotation = MathHelper.WrapAngle(diamondRotation + eTime * MathHelper.Pi);
            diamondTransform = Matrix.CreateScale(diamondScale)
                             * Matrix.CreateRotationY(diamondRotation)
                             * Matrix.CreateTranslation(diamondPosition);

            camera.Update(robotPosition, robotRotation);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            effect.View = diamondTransform * camera.View;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                effect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip, diamond, 0, diamond.Length - 2);
            }

            foreach (BasicEffect rEffect in robot.Meshes["Robot"].Effects) {
                rEffect.EnableDefaultLighting();
                rEffect.DirectionalLight0.Direction = Vector3.Transform(rEffect.DirectionalLight0.Direction, robotTransform);
                rEffect.DirectionalLight1.Direction = Vector3.Transform(rEffect.DirectionalLight1.Direction, robotTransform);
                rEffect.DirectionalLight2.Direction = Vector3.Transform(rEffect.DirectionalLight2.Direction, robotTransform);
                rEffect.EmissiveColor = Color.White.ToVector3();
                rEffect.View = robotTransform * camera.View;
                rEffect.Projection = camera.Projection;
                rEffect.World = robot.Bones["Robot"].Transform * camera.World;
            }
            robot.Meshes["Robot"].Draw();

            base.Draw(gameTime);
        }
    }
}
