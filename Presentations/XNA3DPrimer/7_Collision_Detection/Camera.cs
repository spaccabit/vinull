using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _7_Collision_Detection {
    public class Camera {

        public Matrix Projection;
        public Matrix View;
        public Matrix World;

        Matrix transPos;
        Matrix transTarget;

        public Camera(Matrix Position, Matrix Target) {

            World = Matrix.Identity;
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 1f, 0.1f, 1000f);

            transPos = Position;
            transTarget = Target;
        }

        public void Update(Vector3 position, float rotation) {

            Matrix transform = Matrix.Identity
                             * Matrix.CreateRotationY(rotation)
                             * Matrix.CreateTranslation(position);

            Vector3 pos = Vector3.Transform(Vector3.Zero, transPos * transform);
            Vector3 tar = Vector3.Transform(Vector3.Zero, transTarget * transform);

            View = Matrix.CreateLookAt(pos, tar, Vector3.Up);
        }
    }
}
