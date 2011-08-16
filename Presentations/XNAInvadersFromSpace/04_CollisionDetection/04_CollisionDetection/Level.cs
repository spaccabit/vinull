using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _04_CollisionDetection.Objects;

namespace _04_CollisionDetection {
    public class Level {

        Rectangle field;

        PlayerCannon player;
        Armada armada;
        Score score;
        Lives lives;

        public Level() {

            field.X = 50;
            field.Width = GameMain.Screen.Width - 100;
            field.Y = 50;
            field.Height = GameMain.Screen.Height - 70;

            player = new PlayerCannon(field);
            armada = new Armada(6, 10, field);
            score = new Score();
            lives = new Lives(3);
        }

        public void Update(GameTime gameTime) {
            player.Update(gameTime);
            armada.Update(gameTime);
            CollisionDetection();
        }

        private void GameOver() {
            player.Hit = true;
            armada.Landed = true;
        }

        private void Reset() {
            player.Reset();
            armada.Reset();
            score.Reset();
            lives.Reset(3);
        }

        private void CollisionDetection() {

            /* test player and aramda */
            if (armada.Landed && !player.Hit)
                GameOver();
            else if (armada.ArmadaLocation.Intersects(player.Location) ||
                    (player.Shot.Active && armada.ArmadaLocation.Intersects(player.Shot.Location))) {
                for (int c = 0; c < armada.Invaders.Length; c++)
                    for (int r = 0; r < armada.Invaders[c].Length; r++) {
                        if (armada.Invaders[c][r].Active) {
                            if (armada.Invaders[c][r].Location.Intersects(player.Shot.Location)) {
                                armada.Invaders[c][r].Active = false;
                                player.Shot.Active = false;
                                score.AddPoints(10);
                                if (armada.UpdateArmada() == 0) {
                                    armada.Reset();
                                }
                                break;
                            }
                            if (armada.Invaders[c][r].Location.Intersects(player.Location)) {
                                GameOver();
                                break;
                            }
                        }
                    }
            }

            /* test invader missles */
            for (int i = 0; i < armada.Missles.Length; i++) {
                if (armada.Missles[i].Active) {
                    if (armada.Missles[i].Location.Intersects(player.Location)) {
                        lives.Count--;
                        if (lives.Count < 0) {
                            GameOver();
                        }

                        player.Shot.Active = false;
                        for (int j = 0; j < armada.Missles.Length; j++)
                            armada.Missles[j].Active = false;
                        break;
                    }

                }
            }

        }

        public void Draw(SpriteBatch spriteBatch) {
            player.Draw(spriteBatch);
            armada.Draw(spriteBatch);
            score.Draw(spriteBatch);
            lives.Draw(spriteBatch);
        }
    }
}
