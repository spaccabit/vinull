using System;
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

            Field.X = 50;
            Field.Width = screen.Width - 100;
            Field.Y = 50;
            Field.Height = screen.Height - 70;

            Player = new PlayerCannon(Field);
            Invaders = new Armada(6, 10, Field);
        }

        public void Update(GameTime gameTime) {
            Player.Update(gameTime);
            Invaders.Update(gameTime);

            if (Player.Shot.Active &&
                (Invaders.ArmadaLocation.Intersects(Player.Shot.Location) || Invaders.ArmadaLocation.Contains(Player.Shot.Location))) {
                for (int i = Invaders.Invaders.Length - 1; i >= 0; i--) {
                    if (Invaders.Invaders[i].Active &&
                        (Invaders.Invaders[i].Location.Intersects(Player.Shot.Location) || Invaders.Invaders[i].Location.Contains(Player.Shot.Location))) {
                        Invaders.Invaders[i].Active = false;
                        Player.Shot.Active = false;
                        Invaders.UpdateArmadaLocation();
                        break;
                    }
                }
            }

            if (Invaders.ArmadaLocation.Intersects(Player.Location)) {
                Invaders.Landed = true;
                Player.Hit = true;
                Player.Shot.Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            Player.Draw(spriteBatch);
            Invaders.Draw(spriteBatch);
        }
    }
}
