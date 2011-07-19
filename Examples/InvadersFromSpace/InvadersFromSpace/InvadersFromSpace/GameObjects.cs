using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace.Objects {

    public struct Bullet {
        public Boolean Active;
        public Rectangle Location;

        public static Bullet CreateBullet() {
            Bullet result = new Bullet();
            result.Active = false;
            result.Location.X = 0;
            result.Location.Y = 0;
            result.Location.Width = Sprites.Bullet.Width;
            result.Location.Height = Sprites.Bullet.Height;
            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Bullet bullet, Color color) {
            if (bullet.Active)
                spriteBatch.Draw(Sprites.SpriteSheet, bullet.Location, Sprites.Bullet, color);
        }
    }

    public struct Invader {
        public Boolean Active;
        public Rectangle Location;
        public Rectangle[] Frames;
        public Color Color;

        public static Invader CreateInvader(Rectangle[] frames, Int32 x, Int32 y, Color color) {
            Invader result = new Invader();
            result.Frames = frames;
            result.Active = true;
            result.Color = color;
            result.Location.X = x;
            result.Location.Y = y;
            result.Location.Height = frames[0].Height;
            result.Location.Width = frames[0].Width;

            return result;
        }

        public static void Draw(SpriteBatch spriteBatch, Invader invader, Int32 frame) {
            if (invader.Active)
                spriteBatch.Draw(Sprites.SpriteSheet, invader.Location, invader.Frames[frame], invader.Color);
        }
    }

    public class Score {
        public Int32 Points;
        public String Display;
        
        Vector2 Location;
        const String DisplayFormat = "Score: {0:0000}";

        public Score() {
            AddPoints(0);
        }

        public void AddPoints(Int32 p) {
            Points += p;
            Display = String.Format(Score.DisplayFormat, Points);
            Location = Sprites.ScoreFont.MeasureString(Display);
            Location.Y = 5;
            Location.X = GameMain.Screen.Width - Location.X - 5;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Sprites.ScoreFont, Display, Location, Color.LimeGreen);
        }
    }

    public class Lives {
        public Int32 Count;

        Rectangle[] markers;
        const String Label = "Lives: ";
        static Vector2 LabelLocation = new Vector2(5, 5);

        public Lives(Int32 count) {
            Count = count;

            Vector2 startMarkers = Sprites.ScoreFont.MeasureString(Label);

            markers = new Rectangle[Count];
            for (int i = 0; i < Count; i++) {
                markers[i].Height = (Int32)(Sprites.PlayerCannon.Height * .75);
                markers[i].Width = (Int32)(Sprites.PlayerCannon.Width * .75);
                markers[i].X = (Int32)(startMarkers.X + LabelLocation.X) + i * 8 + i * markers[i].Width;
                markers[i].Y = (Int32)(LabelLocation.Y + startMarkers.Y * .85 - markers[i].Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Sprites.ScoreFont, Label, LabelLocation, Color.LimeGreen);
            for (int i = 0; i < Count; i++)
                spriteBatch.Draw(Sprites.SpriteSheet, markers[i], Sprites.PlayerCannon, Color.LimeGreen);
        }
    }

}
