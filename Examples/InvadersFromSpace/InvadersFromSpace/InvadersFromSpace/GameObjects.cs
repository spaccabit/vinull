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
            result.Location.Width = SpriteSheet.Bullet.Width;
            result.Location.Height = SpriteSheet.Bullet.Height;
            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Bullet bullet, Color color) {
            if (bullet.Active)
                spriteBatch.Draw(SpriteSheet.Texture, bullet.Location, SpriteSheet.Bullet, color);
        }
    }

    public struct Invader {
        public Boolean Active;
        public Rectangle Location;
        public Rectangle[] Sprites;
        public Color Color;

        public static Invader CreateInvader(Rectangle[] sprites, Int32 x, Int32 y, Color color) {
            Invader result = new Invader();
            result.Sprites = sprites;
            result.Active = true;
            result.Color = color;
            result.Location.X = x;
            result.Location.Y = y;
            result.Location.Height = sprites[0].Height;
            result.Location.Width = sprites[0].Width;

            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Invader invader, Int32 frame) {
            if (invader.Active)
                spriteBatch.Draw(SpriteSheet.Texture, invader.Location, invader.Sprites[frame], invader.Color);
        }
    }
}
