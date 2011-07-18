using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace {
    public static class Sprites {

        public static Texture2D SpriteSheet;
        public static Texture2D Starfield;

        public static Rectangle PlayerCannon = new Rectangle(5, 0, 22, 32);
        public static Rectangle Bullet = new Rectangle(51, 0, 5, 16);
        public static Rectangle Solid = new Rectangle(73, 1, 34, 34);
        public static Rectangle Moon = new Rectangle(0, 237, 256, 19);
        
        public static Rectangle[] Invader1 = new Rectangle[] {
            new Rectangle(113, 10, 26, 22),
            new Rectangle(149, 10, 26, 22)
        };

    }
}
