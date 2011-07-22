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
        public Rectangle Field;
        public Bullet Shot;
        public Boolean Hit;

        const Double CannonSpeed = 0.2;
        const Double BulletSpeed = 0.4;

        public PlayerCannon(Rectangle field) {
            Field = field;
            Hit = false;

            Location.X = Field.X;
            Location.Y = Field.Y + Field.Height - Sprites.PlayerCannon.Height;
            Location.Width = Sprites.PlayerCannon.Width;
            Location.Height = Sprites.PlayerCannon.Height;

            Shot.Init();
        }

        public void Update(GameTime gameTime) {
            KeyboardState keys = Keyboard.GetState();
            if (!Hit) {
                MovePlayer(gameTime, keys);
                UpdateShot(gameTime, keys);
            }
        }

        private void UpdateShot(GameTime gameTime, KeyboardState keys) {
            if (Shot.Active) {
                Shot.Location.Y -= (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * BulletSpeed);

                if (Shot.Location.Y < Field.Y)
                    Shot.Active = false;
            }
            else if (keys.IsKeyDown(Keys.Space)) {
                Shot.Active = true;
                Shot.Location.Y = Location.Y - Sprites.Bullet.Height;
                Shot.Location.X = Location.X + Location.Width / 2 - Sprites.Bullet.Width / 2;
            }
        }

        private void MovePlayer(GameTime gameTime, KeyboardState keys) {
            if (keys.IsKeyDown(Keys.Right))
                Location.X += (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * CannonSpeed);

            else if (keys.IsKeyDown(Keys.Left))
                Location.X -= (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * CannonSpeed);

            if (!Field.Contains(Location))
                Location.X = (Int32)MathHelper.Clamp(Location.X, Field.X, Field.X + Field.Width - Location.Width);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprites.SpriteSheet, Location, Sprites.PlayerCannon, Color.LimeGreen);
            Shot.Draw(spriteBatch);
        }

        internal void Reset() {
            Hit = false;

            Location.X = Field.X;
            Location.Y = Field.Y + Field.Height - Sprites.PlayerCannon.Height;
            Location.Width = Sprites.PlayerCannon.Width;
            Location.Height = Sprites.PlayerCannon.Height;

            Shot.Init();
        }
    }
}
