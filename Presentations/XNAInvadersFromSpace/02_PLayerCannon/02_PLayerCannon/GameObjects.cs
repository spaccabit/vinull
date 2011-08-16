using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _02_PLayerCannon.Objects {

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
}
