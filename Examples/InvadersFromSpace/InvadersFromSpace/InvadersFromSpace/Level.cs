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
        
        Int32 Score;
        String ScoreDisplay;
        Vector2 ScoreLocation;
        const String ScoreFormat = "Score: {0:0000}";

        public Level(Rectangle screen) {
            Screen = screen;

            Field.X = 50;
            Field.Width = screen.Width - 100;
            Field.Y = 50;
            Field.Height = screen.Height - 70;

            Player = new PlayerCannon(Field);
            Invaders = new Armada(6, 10, Field);
            Score = 0;
            UpdateScore();
        }

        public void Update(GameTime gameTime) {
            Player.Update(gameTime);
            Invaders.Update(gameTime);

            if (Invaders.ArmadaLocation.Intersects(Player.Location) ||
                (Player.Shot.Active &&
                (Invaders.ArmadaLocation.Intersects(Player.Shot.Location) || Invaders.ArmadaLocation.Contains(Player.Shot.Location)))) {
                for (int i = Invaders.Invaders.Length - 1; i >= 0; i--) {
                    if (Invaders.Invaders[i].Active) {
                        if (Invaders.Invaders[i].Location.Intersects(Player.Shot.Location) || Invaders.Invaders[i].Location.Contains(Player.Shot.Location)) {
                            Invaders.Invaders[i].Active = false;
                            Player.Shot.Active = false;
                            Invaders.UpdateArmadaLocation();
                            Score += 10;
                            UpdateScore();
                            break;
                        }
                        if (Invaders.Invaders[i].Location.Intersects(Player.Location)) {
                            Invaders.Landed = true;
                            Player.Hit = true;
                            Player.Shot.Active = false;
                            break;
                        }
                    }
                }
            }
        }

        private void UpdateScore() {
            ScoreDisplay = String.Format(ScoreFormat, Score);
            ScoreLocation = Sprites.ScoreFont.MeasureString(ScoreDisplay);
            ScoreLocation.Y = 5;
            ScoreLocation.X = Screen.Width - ScoreLocation.X - 5;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Player.Draw(spriteBatch);
            Invaders.Draw(spriteBatch);
            spriteBatch.DrawString(Sprites.ScoreFont, ScoreDisplay, ScoreLocation, Color.LimeGreen);
        }
    }
}
