using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _03_Armada.Objects;

namespace _03_Armada {
    public class Level {

        Rectangle field;

        PlayerCannon player;
        Armada armada;
        Boolean gameover;

        public Level() {

            field.X = 50;
            field.Width = GameMain.Screen.Width - 100;
            field.Y = 50;
            field.Height = GameMain.Screen.Height - 70;

            player = new PlayerCannon(field);
            armada = new Armada(6, 10, field);
            gameover = false;

        }

        public void Update(GameTime gameTime) {
            player.Update(gameTime);
            armada.Update(gameTime);
        }

        private void GameOver() {
            player.Hit = true;
            armada.Landed = true;
            gameover = true;
        }

        private void Reset() {
            gameover = false;
            player.Reset();
            armada.Reset();
        }

        public void Draw(SpriteBatch spriteBatch) {
            player.Draw(spriteBatch);
            armada.Draw(spriteBatch);
        }
    }
}
