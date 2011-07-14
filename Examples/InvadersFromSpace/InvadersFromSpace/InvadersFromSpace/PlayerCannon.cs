using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InvadersFromSpace.Objects;

namespace InvadersFromSpace {
    public class PlayerCannon {

        public Rectangle Location;
        public Int32 LeftBounds;
        public Int32 RightBounds;
        public Bullet Shot = Bullet.CreateBullet();

        const Double CannonSpeed = 0.2;
        const Double BulletSpeed = 0.4;

        public PlayerCannon(Int32 x, Int32 y, Int32 left, Int32 right) {
            Location.X = x;
            Location.Y = y;
            Location.Width = SpriteSheet.PlayerCannon.Width;
            Location.Height = SpriteSheet.PlayerCannon.Height;
            LeftBounds = left;
            RightBounds = right;
        }

        public void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();
            MovePlayer(gameTime, keys);
            UpdateShot(gameTime, keys);
        }

        private void UpdateShot(GameTime gameTime, KeyboardState keys) {
            if (Shot.Active) {
                Shot.Location.Y -= (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * BulletSpeed);

                if (Shot.Location.Y < 0)
                    Shot.Active = false;
            }
            else if (keys.IsKeyDown(Keys.Space)) {
                Shot.Active = true;
                Shot.Location.Y = Location.Y - SpriteSheet.Bullet.Height;
                Shot.Location.X = Location.X + Location.Width / 2 - SpriteSheet.Bullet.Width / 2;
            }
        }

        private void MovePlayer(GameTime gameTime, KeyboardState keys) {
            if (keys.IsKeyDown(Keys.Right))
                Location.X += (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * CannonSpeed);

            else if (keys.IsKeyDown(Keys.Left))
                Location.X -= (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * CannonSpeed);

            if (Location.X < LeftBounds || Location.X > RightBounds)
                Location.X = (Int32)MathHelper.Clamp(Location.X, LeftBounds, RightBounds);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(SpriteSheet.Texture, Location, SpriteSheet.PlayerCannon, Color.ForestGreen);
            Bullet.Draw(spriteBatch, Shot, Color.Green);
        }
    }
}
