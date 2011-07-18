using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace {
    public class Background {

        Rectangle Screen;
        Rectangle Moon;
        Rectangle StarField1;
        Rectangle StarField2;

        static double starspeed = 0.1;
        
        public Background(Rectangle screen) {
            Screen = screen;
        
            StarField1 = screen;
            StarField2 = screen;
            StarField2.Y += 160;

            Moon = screen;
            Moon.Height = 30;
            Moon.Y = screen.Height - Moon.Height;

        }

        public void Update(GameTime gameTime) {
            StarField1.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);
            StarField1.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 4);
            StarField2.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed);
            StarField2.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprites.Starfield, Screen, StarField1, Color.DarkSlateGray);
            spriteBatch.Draw(Sprites.Starfield, Screen, StarField2, Color.SlateGray);
            spriteBatch.Draw(Sprites.SpriteSheet, Moon, Sprites.Moon, Color.Gray);
        }
    }
}
