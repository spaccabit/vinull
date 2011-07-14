﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InvadersFromSpace.Objects;

namespace InvadersFromSpace {
    public class Level {

        Rectangle Screen;
        Rectangle Field;
        PlayerCannon Player;
        Armada Invaders;

        public Level(Rectangle screen) {
            Screen = screen;
            Field.X = screen.X + 30;
            Field.Width = screen.Width - 60;
            Field.Y = screen.Y + 20;
            Field.Height = screen.Height - 40;

            Player = new PlayerCannon(Field.X, Field.Height + Field.Y - SpriteSheet.PlayerCannon.Height, Field.X, Field.Width + Field.X - SpriteSheet.PlayerCannon.Width);
            Invaders = new Armada(6, 10, Field.X, Field.Y, Field);
        }

        public void Update(GameTime gameTime) {
            Player.Update(gameTime);
            Invaders.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(SpriteSheet.Texture, Screen, SpriteSheet.Solid, Color.Blue);
            spriteBatch.Draw(SpriteSheet.Texture, Field, SpriteSheet.Solid, Color.Black);
            Player.Draw(spriteBatch);
            Invaders.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}