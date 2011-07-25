using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InvadersFromSpace.Objects;

namespace InvadersFromSpace {
    public class Level {

        Rectangle field;

        PlayerCannon player;
        Armada armada;
        Shield[] shields;
        Score score;
        Lives lives;
        Boolean gameover;

        public Level() {

            field.X = 50;
            field.Width = GameMain.Screen.Width - 100;
            field.Y = 50;
            field.Height = GameMain.Screen.Height - 70;

            player = new PlayerCannon(field);
            armada = new Armada(6, 10, field);
            score = new Score();
            lives = new Lives(3);
            gameover = false;

            shields = new Shield[3];
            for (int i = 0; i < shields.Length; i++) {
                shields[i].Reset();
                shields[i].Location.Y = field.Height + field.Y - 90;
                shields[i].Location.X = field.Width + field.X - field.Width / shields.Length * i - field.Width / shields.Length / 2 - shields[i].Location.Width / 2;
            }
        }

        public void Update(GameTime gameTime) {

            if (gameover) {
                GameMessage.SetMessage(GameMessage.StartMessage);
                Reset();
            }
            else {
                player.Update(gameTime);
                armada.Update(gameTime);
                CollisionDetection();
            }
        }

        private void GameOver() {
            player.Hit = true;
            armada.Landed = true;
            GameMessage.SetMessage(GameMessage.GameOverMessage);
            gameover = true;
        }

        private void Reset() {
            gameover = false;
            score.Reset();
            lives.Reset(3);
            player.Reset();
            armada.Reset();
            for (int i = 0; i < shields.Length; i++) 
                shields[i].Reset();
        }

        private void CollisionDetection() {
            if (armada.Landed && !player.Hit)
                GameOver();
            else if (armada.ArmadaLocation.Intersects(player.Location) ||
                    (player.Shot.Active &&
                    (armada.ArmadaLocation.Intersects(player.Shot.Location) || armada.ArmadaLocation.Contains(player.Shot.Location)))) {
                for (int c = 0; c < armada.Invaders.Length; c++)
                    for (int r = 0; r < armada.Invaders[c].Length; r++) {
                        if (armada.Invaders[c][r].Active) {
                            if (armada.Invaders[c][r].Location.Intersects(player.Shot.Location) || armada.Invaders[c][r].Location.Contains(player.Shot.Location)) {
                                armada.Invaders[c][r].Active = false;
                                player.Shot.Active = false;
                                armada.UpdateArmadaLocation();
                                score.AddPoints(10);
                                break;
                            }
                            if (armada.Invaders[c][r].Location.Intersects(player.Location)) {
                                GameOver();
                                break;
                            }
                        }
                    }
            }

            for (int i = 0; i < armada.Missles.Length; i++) {
                if (armada.Missles[i].Active && armada.Missles[i].Location.Intersects(player.Location)) {
                    lives.Count--;
                    if (lives.Count >= 0) {
                        GameMessage.SetMessage(String.Format("Lives Left: {0}", lives.Count));
                    }
                    else {
                        GameOver();
                    }

                    player.Shot.Active = false;
                    for (int j = 0; j < armada.Missles.Length; j++)
                        armada.Missles[j].Active = false;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            player.Draw(spriteBatch);
            armada.Draw(spriteBatch);
            score.Draw(spriteBatch);
            lives.Draw(spriteBatch);
            for (int i = 0; i < shields.Length; i++)
                shields[i].Draw(spriteBatch);
        }
    }
}
