using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace.Objects {

    public struct Bullet {
        public Boolean Active;
        public Rectangle Location;

        public static Bullet CreateBullet() {
            Bullet result = new Bullet();
            result.Active = false;
            result.Location.X = 0;
            result.Location.Y = 0;
            result.Location.Width = Sprites.Bullet.Width;
            result.Location.Height = Sprites.Bullet.Height;
            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Bullet bullet, Color color) {
            if (bullet.Active)
                spriteBatch.Draw(Sprites.SpriteSheet, bullet.Location, Sprites.Bullet, color);
        }
    }

    public struct Invader {
        public Boolean Active;
        public Rectangle Location;
        public Rectangle[] Frames;
        public Color Color;

        public static Invader CreateInvader(Rectangle[] frames, Int32 x, Int32 y, Color color) {
            Invader result = new Invader();
            result.Frames = frames;
            result.Active = true;
            result.Color = color;
            result.Location.X = x;
            result.Location.Y = y;
            result.Location.Height = frames[0].Height;
            result.Location.Width = frames[0].Width;

            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Invader invader, Int32 frame) {
            if (invader.Active)
                spriteBatch.Draw(Sprites.SpriteSheet, invader.Location, invader.Frames[frame], invader.Color);
        }
    }
}
