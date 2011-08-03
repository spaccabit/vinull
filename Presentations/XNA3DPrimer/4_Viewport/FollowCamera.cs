﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _4_Viewport {
    class FollowCamera : ICamera {
        public Matrix Projection { get; set; }
        public Matrix View { get; set; }
        public Matrix World { get; set; }

        Vector3 startPos;
        Vector3 startTarget;
        Vector3 startUp;

        public FollowCamera() {

            World = Matrix.Identity;
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 1f, .1f, 10f);

            startPos = new Vector3(1.5f, 1.5f, 1f);
            startTarget = new Vector3(-1f, -1f, -1f);
            startUp = Vector3.Up;
        }

        public void Update(float scale, float distance, float rotation,
                           Vector3 axis) {

            Matrix transform = Matrix.Identity
                             * Matrix.CreateScale(scale)
                             * Matrix.CreateTranslation(0f, distance, 0f)
                             * Matrix.CreateFromAxisAngle(axis, rotation);

            Vector3 pos = Vector3.Transform(startPos, transform);
            Vector3 tar = Vector3.Transform(startTarget, transform);
            Vector3 up = Vector3.Transform(startUp, transform);

            View = Matrix.CreateLookAt(pos, tar, up);
        }
    }
}