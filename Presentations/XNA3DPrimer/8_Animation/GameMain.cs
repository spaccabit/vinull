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
using XNA_3D_Primer_Pipeline.Content;

namespace _8_Animation {
    public class GameMain : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        Camera camera;
        Model robot;
        MeshAnimationInfo robotAni;
        Matrix[] robotTransforms;
        float armAngle = 0f;
        
        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            camera = new Camera();
            Content.RootDirectory = "XNA_3D_Primer_Content";

            base.Initialize();
        }

        protected override void LoadContent() {
            robot = Content.Load<Model>("Robot");
            robotAni = robot.Tag as MeshAnimationInfo;
            robotTransforms = new Matrix[robotAni.BoneTransforms.Count];
        }

        protected override void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();

            float eTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();
            else if (keys.IsKeyDown(Keys.S))
                armAngle += 1f * eTime;
            else if (keys.IsKeyDown(Keys.W))
                armAngle -= 1f * eTime;
            
            armAngle = MathHelper.Clamp(armAngle, -1 * MathHelper.PiOver2, 0f);

            robotAni.BoneTransforms.CopyTo(robotTransforms);

            robotTransforms[robotAni.BoneNames["RightArm"]] = 
                Matrix.CreateRotationY(armAngle) 
                * robotTransforms[robotAni.BoneNames["RightArm"]];

            robotTransforms[robotAni.BoneNames["LeftArm"]] = 
                Matrix.CreateRotationY(armAngle) 
                * robotTransforms[robotAni.BoneNames["LeftArm"]];

            robotTransforms[robotAni.BoneNames["LeftShoulder"]] = 
                Matrix.CreateRotationY(armAngle) * 
                robotTransforms[robotAni.BoneNames["LeftShoulder"]];

            for (int i = 1; i < robotTransforms.Length; i++)
                robotTransforms[i] = robotTransforms[i] 
                    * robotTransforms[robotAni.BoneParent[i]];

            for (int i = 0; i < robotTransforms.Length; i++)
                robotTransforms[i] = robotAni.InverseBoneTransforms[i]
                    * robotTransforms[i];

            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (SkinnedEffect effect in robot.Meshes["Robot"].Effects) {
                effect.EnableDefaultLighting();
                effect.EmissiveColor = Color.White.ToVector3();
                effect.SetBoneTransforms(robotTransforms);
                effect.View = camera.View;
                effect.Projection = camera.Projection;
                effect.World = robot.Bones["Robot"].Transform * camera.World;
            }
            robot.Meshes["Robot"].Draw();

            base.Draw(gameTime);
        }
    }
}
