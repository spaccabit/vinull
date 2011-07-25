using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using InvadersFromSpace.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace {
    public class Armada {

        public Rectangle ArmadaLocation;
        public Invader[][] Invaders;
        public Boolean Landed;
        public Bullet[] Missles;

        readonly Color[] InvaderColors = new Color[] { Color.Purple, Color.Blue, Color.Red, Color.Orange, Color.Yellow, Color.Silver };
        const Int32 InvaderSpacing = 10;
        const double ArmadaStartingMoveRate = 200;
        const double ArmadaEndingMoveRate = 15;
        const Int32 ArmadaSpeed = 5;
        const Int32 ArmadaDecent = 20;

        Int32 ArmadaFrame;
        double ArmadaMoveTimer;
        double ArmadaMoveRate;
        Int32 ArmadaDirection = 1;
        Rectangle Field;

        double MissleTimer;
        const Double MissleSpeed = 0.3;
        const Double MissleRate = 750;

        public Armada(Int32 rows, Int32 cols, Rectangle field) {

            Missles = new Bullet[3];
            Invaders = new Invader[cols][];
            Field = field;

            ArmadaFrame = 0;
            ArmadaMoveTimer = 0;
            ArmadaMoveRate = ArmadaStartingMoveRate;
            Landed = false;

            ArmadaLocation.Width = cols * (InvaderSpacing + Sprites.Invader1[0].Width) - InvaderSpacing;
            ArmadaLocation.Height = rows * (InvaderSpacing + Sprites.Invader1[0].Height) - InvaderSpacing;
            ArmadaLocation.X = Field.X + Field.Width / 2 - ArmadaLocation.Width / 2;
            ArmadaLocation.Y = Field.Y;

            for (int c = 0; c < cols; c++) {
                Invaders[c] = new Invader[rows];
                for (int r = 0; r < rows; r++) {
                    Invaders[c][r].Init(Sprites.Invader1,
                        ArmadaLocation.X + c * (Sprites.Invader1[0].Width + InvaderSpacing),
                        ArmadaLocation.Y + r * (Sprites.Invader1[0].Height + InvaderSpacing),
                        InvaderColors[r]);
                }
            }

            for (int i = 0; i < Missles.Length; i++) {
                Missles[i].Init();
            }
        }

        public void UpdateArmadaLocation() {
            ArmadaLocation.X = 0;
            ArmadaLocation.Y = 0;
            ArmadaLocation.Width = 0;
            ArmadaLocation.Height = 0;
            double killed = 0;

            for (int c = 0; c < Invaders.Length; c++)
                for (int r = 0; r < Invaders[c].Length; r++) {
                    if (Invaders[c][r].Active) {
                        if (ArmadaLocation.X == 0 || ArmadaLocation.X > Invaders[c][r].Location.X) {
                            ArmadaLocation.Width += ArmadaLocation.X - Invaders[c][r].Location.X;
                            ArmadaLocation.X = Invaders[c][r].Location.X;
                    }
                    if (ArmadaLocation.Y == 0)
                        ArmadaLocation.Y = Invaders[c][r].Location.Y;
                    if (ArmadaLocation.Width < Invaders[c][r].Location.X + Invaders[c][r].Location.Width - ArmadaLocation.X)
                        ArmadaLocation.Width = Invaders[c][r].Location.X + Invaders[c][r].Location.Width - ArmadaLocation.X;
                    if (ArmadaLocation.Height < Invaders[c][r].Location.Y + Invaders[c][r].Location.Height - ArmadaLocation.Y)
                        ArmadaLocation.Height = Invaders[c][r].Location.Y + Invaders[c][r].Location.Height - ArmadaLocation.Y;
                }
                else
                    killed++;
            }

            double adj = 1 - MathHelper.Clamp((float)Math.Cos(MathHelper.PiOver2 * killed / (Invaders.Length * Invaders[0].Length - 1) + 0.20), 0, 1);
            ArmadaMoveRate = ArmadaStartingMoveRate - adj * (ArmadaStartingMoveRate - ArmadaEndingMoveRate);

            if (killed == Invaders.Length * Invaders[0].Length) {
                GameMessage.SetMessage("Next Wave");
                Reset();
            }
        }

        public void Update(GameTime gameTime) {

            ArmadaMoveTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            MissleTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!Landed && ArmadaMoveTimer >= ArmadaMoveRate) {
                ArmadaMoveTimer = 0;
                ArmadaFrame = ArmadaFrame == 0 ? 1 : 0;
                ArmadaLocation.X += ArmadaSpeed * ArmadaDirection;
                if (!Field.Contains(ArmadaLocation)) {
                    ArmadaLocation.X -= ArmadaSpeed * ArmadaDirection;
                    ArmadaDirection *= -1;

                    ArmadaLocation.Y += ArmadaDecent;
                    if (!Field.Contains(ArmadaLocation)) {
                        Landed = true;
                        for (int c = 0; c < Invaders.Length; c++)
                            for (int r = 0; r < Invaders[c].Length; r++) {
                                Invaders[c][r].Location.Y += ArmadaDecent - (ArmadaLocation.Y + ArmadaLocation.Height - (Field.Y + Field.Height));
                        }
                    }
                    else {
                        for (int c = 0; c < Invaders.Length; c++)
                            for (int r = 0; r < Invaders[c].Length; r++) {
                               Invaders[c][r].Location.Y += ArmadaDecent;
                        }
                    }
                }
                else {
                    for (int c = 0; c < Invaders.Length; c++)
                        for (int r = 0; r < Invaders[c].Length; r++) {
                        Invaders[c][r].Location.X += ArmadaSpeed * ArmadaDirection;
                    }
                }
            }

            if (!Landed && MissleTimer < 0) {
                MissleTimer = MissleRate;
                for (int i = 0; i < Missles.Length; i++) {
                    if (!Missles[i].Active) {
                        do {
                            Int32 c = GameMain.Rand.Next(Invaders.Length);
                            for (int r = Invaders[c].Length - 1; r >= 0; r--)
                                if (Invaders[c][r].Active) {
                                    Missles[i].Active = true;
                                    Missles[i].Color = Invaders[c][r].Color;
                                    Missles[i].Location.Y = Invaders[c][r].Location.Y + Invaders[c][r].Location.Height;
                                    Missles[i].Location.X = Invaders[c][r].Location.X + Invaders[c][r].Location.Width / 2;
                                    break;
                                }
                        }
                        while (!Missles[i].Active);
                        break;
                    }
                }
            }

            for (int i = 0; i < Missles.Length; i++) {
                if (Missles[i].Active) {
                    Missles[i].Location.Y += (Int32)(gameTime.ElapsedGameTime.TotalMilliseconds * MissleSpeed);

                    if (Missles[i].Location.Y > Field.Y + Field.Height)
                        Missles[i].Active = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < Missles.Length; i++)
                Missles[i].Draw(spriteBatch);
            for (int c = 0; c < Invaders.Length; c++)
                for (int r = 0; r < Invaders[c].Length; r++)
                    Invader.Draw(spriteBatch, Invaders[c][r], ArmadaFrame);
        }

        public void Reset() {
            Landed = false;
            ArmadaDirection = 1;
            ArmadaFrame = 0;
            for (int i = 0; i < Missles.Length; i++)
                Missles[i].Active = false;

            for (int c = 0; c < Invaders.Length; c++)
                for (int r = 0; r < Invaders[c].Length; r++)
                    Invaders[c][r].Active = true;

            UpdateArmadaLocation();

            Int32 xDelta = ArmadaLocation.X - (Field.X + Field.Width / 2 - ArmadaLocation.Width / 2);
            Int32 yDelta = ArmadaLocation.Y - Field.Y;
            ArmadaLocation.X -= xDelta;
            ArmadaLocation.Y -= yDelta;

            for (int c = 0; c < Invaders.Length; c++)
                for (int r = 0; r < Invaders[c].Length; r++) {
                    Invaders[c][r].Location.X -= xDelta;
                    Invaders[c][r].Location.Y -= yDelta;
                }

        }
    }
}
