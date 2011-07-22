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
        public Invader[] Invaders;
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
            Invaders = new Invader[rows * cols];
            Field = field;

            ArmadaFrame = 0;
            ArmadaMoveTimer = 0;
            ArmadaMoveRate = ArmadaStartingMoveRate;
            Landed = false;

            ArmadaLocation.Width = cols * (InvaderSpacing + Sprites.Invader1[0].Width) - InvaderSpacing;
            ArmadaLocation.Height = rows * (InvaderSpacing + Sprites.Invader1[0].Height) - InvaderSpacing;
            ArmadaLocation.X = Field.X + Field.Width / 2 - ArmadaLocation.Width / 2;
            ArmadaLocation.Y = Field.Y;

            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    Invaders[c + r * cols].Init(Sprites.Invader1,
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

            for (int i = 0; i < Invaders.Length; i++) {
                if (Invaders[i].Active) {
                    if (ArmadaLocation.X == 0 || ArmadaLocation.X > Invaders[i].Location.X) {
                        ArmadaLocation.Width += ArmadaLocation.X - Invaders[i].Location.X;
                        ArmadaLocation.X = Invaders[i].Location.X;
                    }
                    if (ArmadaLocation.Y == 0)
                        ArmadaLocation.Y = Invaders[i].Location.Y;
                    if (ArmadaLocation.Width < Invaders[i].Location.X + Invaders[i].Location.Width - ArmadaLocation.X)
                        ArmadaLocation.Width = Invaders[i].Location.X + Invaders[i].Location.Width - ArmadaLocation.X;
                    if (ArmadaLocation.Height < Invaders[i].Location.Y + Invaders[i].Location.Height - ArmadaLocation.Y)
                        ArmadaLocation.Height = Invaders[i].Location.Y + Invaders[i].Location.Height - ArmadaLocation.Y;
                }
                else
                    killed++;
            }

            double adj = 1 - MathHelper.Clamp((float)Math.Cos(MathHelper.PiOver2 * killed / (Invaders.Length - 1) + 0.20), 0, 1);
            ArmadaMoveRate = ArmadaStartingMoveRate - adj * (ArmadaStartingMoveRate - ArmadaEndingMoveRate);

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
                        for (int i = 0; i < Invaders.Length; i++) {
                            Invaders[i].Location.Y += ArmadaDecent - (ArmadaLocation.Y + ArmadaLocation.Height - (Field.Y + Field.Height));
                        }
                    }
                    else {
                        for (int i = 0; i < Invaders.Length; i++) {
                            Invaders[i].Location.Y += ArmadaDecent;
                        }
                    }
                }
                else {
                    for (int i = 0; i < Invaders.Length; i++) {
                        Invaders[i].Location.X += ArmadaSpeed * ArmadaDirection;
                    }
                }
            }

            if (!Landed && MissleTimer < 0) {
                MissleTimer = MissleRate;
                for (int i = 0; i < Missles.Length; i++) {
                    if (!Missles[i].Active) {
                        do {
                            Int32 s = GameMain.Rand.Next(Invaders.Length);
                            if (Invaders[s].Active) {
                                Missles[i].Active = true;
                                Missles[i].Color = Invaders[s].Color;
                                Missles[i].Location.Y = Invaders[s].Location.Y + Invaders[s].Location.Height;
                                Missles[i].Location.X = Invaders[s].Location.X + Invaders[s].Location.Width / 2;
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
            for (int i = 0; i < Invaders.Length; i++)
                Invader.Draw(spriteBatch, Invaders[i], ArmadaFrame);
        }


        public void Reset() {
            Landed = false;
            ArmadaDirection = 1;
            ArmadaFrame = 0;
            for (int i = 0; i < Missles.Length; i++)
                Missles[i].Active = false;
            
            for (int i = 0; i < Invaders.Length; i++)
                Invaders[i].Active = true;
            
            UpdateArmadaLocation();

            Int32 xDelta = ArmadaLocation.X - (Field.X + Field.Width / 2 - ArmadaLocation.Width / 2);
            Int32 yDelta = ArmadaLocation.Y - Field.Y;
            ArmadaLocation.X -= xDelta;
            ArmadaLocation.Y -= yDelta;

            for (int i = 0; i < Invaders.Length; i++) {
                Invaders[i].Location.X -= xDelta;
                Invaders[i].Location.Y -= yDelta;
            }
           
        }
    }
}
