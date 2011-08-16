using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _04_CollisionDetection.Objects {

    public struct Bullet {
        public Boolean Active;
        public Rectangle Location;
        public Color Color;

        public void Init() {
            Active = false;
            Location.X = 0;
            Location.Y = 0;
            Location.Width = Sprites.Bullet.Width;
            Location.Height = Sprites.Bullet.Height;
            Color = Color.LimeGreen;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Active)
                spriteBatch.Draw(Sprites.SpriteSheet, Location, Sprites.Bullet, Color);
        }
    }

    public struct Invader {
        public Boolean Active;
        public Rectangle Location;
        public Rectangle[] Frames;
        public Color Color;

        public void Init(Rectangle[] frames, Int32 x, Int32 y, Color color) {
            Frames = frames;
            Active = true;
            Color = color;
            Location.X = x;
            Location.Y = y;
            Location.Height = frames[0].Height;
            Location.Width = frames[0].Width;
        }

        public void Draw(SpriteBatch spriteBatch, Int32 frame) {
            if (Active)
                spriteBatch.Draw(Sprites.SpriteSheet, Location, Frames[frame], Color);
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

        public void Reset() {
            Points = 0;
            Display = String.Format(Score.DisplayFormat, Points);
            Location = Sprites.ScoreFont.MeasureString(Display);
            Location.Y = 5;
            Location.X = GameMain.Screen.Width - Location.X - 5;
        }
    }

    public struct Lives {
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

        public void Reset(int count) {
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
    }
}
