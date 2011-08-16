using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _03_Armada.Objects {

    public struct Bullet {
        public Boolean Active;
        public Rectangle Location;
        public Color Color;

        public void Init() {
            Active = false;
            Location.X = 0;
            Location.Y = 0;
            Location.Width = Sprites.Bullet.Width;
            Location.Height = Sprites.Bullet.Height;
            Color = Color.LimeGreen;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Active)
                spriteBatch.Draw(Sprites.SpriteSheet, Location, Sprites.Bullet, Color);
        }
    }

    public struct Invader {
        public Boolean Active;
        public Rectangle Location;
        public Rectangle[] Frames;
        public Color Color;

        public void Init(Rectangle[] frames, Int32 x, Int32 y, Color color) {
            Frames = frames;
            Active = true;
            Color = color;
            Location.X = x;
            Location.Y = y;
            Location.Height = frames[0].Height;
            Location.Width = frames[0].Width;
        }

        public void Draw(SpriteBatch spriteBatch, Int32 frame) {
            if (Active)
                spriteBatch.Draw(Sprites.SpriteSheet, Location, Frames[frame], Color);
        }
    }
}
