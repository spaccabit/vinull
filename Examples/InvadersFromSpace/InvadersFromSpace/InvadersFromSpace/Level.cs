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
        Rectangle Moon;

        Rectangle StarField1;
        Rectangle StarField2;
        PlayerCannon Player;
        Armada Invaders;

        static double starspeed = 0.1;

        public Level(Rectangle screen) {
            Screen = screen;

            StarField1 = screen;
            StarField2 = screen;
            StarField2.Y += 160;
            
            Field.X = 50;
            Field.Width = screen.Width - 100;
            Field.Y = 50;
            Field.Height = screen.Height - 70;

            Moon = screen;
            Moon.Height = 30;
            Moon.Y = screen.Height - Moon.Height;

            Player = new PlayerCannon(Field);
            Invaders = new Armada(6, 10, Field);
        }

        public void Update(GameTime gameTime) {
            Player.Update(gameTime);
            Invaders.Update(gameTime);

            StarField1.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);
            StarField1.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 4);
            StarField2.X += Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed);
            StarField2.Y -= Convert.ToInt32(gameTime.ElapsedGameTime.TotalMilliseconds * starspeed / 2);

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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(Sprites.Starfield, Screen, StarField1, Color.DarkSlateGray);
            spriteBatch.Draw(Sprites.Starfield, Screen, StarField2, Color.SlateGray);
            spriteBatch.Draw(Sprites.SpriteSheet, Moon, Sprites.Moon, Color.Gray);
            Player.Draw(spriteBatch);
            Invaders.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
