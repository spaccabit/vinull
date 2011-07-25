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
        }

        private void CollisionDetection() {
            if (armada.Landed && !player.Hit)
                GameOver();
            else if (armada.ArmadaLocation.Intersects(player.Location) ||
                    (player.Shot.Active &&
                    (armada.ArmadaLocation.Intersects(player.Shot.Location) || armada.ArmadaLocation.Contains(player.Shot.Location)))) {
                for (int i = armada.Invaders.Length - 1; i >= 0; i--) {
                    if (armada.Invaders[i].Active) {
                        if (armada.Invaders[i].Location.Intersects(player.Shot.Location) || armada.Invaders[i].Location.Contains(player.Shot.Location)) {
                            armada.Invaders[i].Active = false;
                            player.Shot.Active = false;
                            armada.UpdateArmadaLocation();
                            score.AddPoints(10);
                            break;
                        }
                        if (armada.Invaders[i].Location.Intersects(player.Location)) {
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
        }
    }
}
