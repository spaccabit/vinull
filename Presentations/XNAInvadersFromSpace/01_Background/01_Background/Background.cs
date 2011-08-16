using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _01_Background {
    public class Background {

        Rectangle Moon;
        Rectangle StarField1;
        Rectangle StarField2;

        static double starspeed = 0.1;
        
        public Background() {
            StarField1 = GameMain.Screen;
            StarField2 = GameMain.Screen;
            StarField2.Y += 160;

            Moon = GameMain.Screen;
            Moon.Height = 30;
            Moon.Y = GameMain.Screen.Height - Moon.Height;
        }

        public void Update(GameTime gameTime) {
            StarField1.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);
            StarField1.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 3);
            StarField2.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed);
            StarField2.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprites.Starfield, GameMain.Screen, StarField1, Color.DarkSlateGray);
            spriteBatch.Draw(Sprites.Starfield, GameMain.Screen, StarField2, Color.SlateGray);
            spriteBatch.Draw(Sprites.SpriteSheet, Moon, Sprites.Moon, Color.Gray);
        }
    }
}
