using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _02_PLayerCannon.Objects;

namespace _02_PLayerCannon {
    public class Level {

        Rectangle field;

        PlayerCannon player;
        Boolean gameover;

        public Level() {

            field.X = 50;
            field.Width = GameMain.Screen.Width - 100;
            field.Y = 50;
            field.Height = GameMain.Screen.Height - 70;

            player = new PlayerCannon(field);
            gameover = false;

        }

        public void Update(GameTime gameTime) {
            player.Update(gameTime);
        }

        private void GameOver() {
            player.Hit = true;
            gameover = true;
        }

        private void Reset() {
            gameover = false;
            player.Reset();
        }

        public void Draw(SpriteBatch spriteBatch) {
            player.Draw(spriteBatch);
        }
    }
}
