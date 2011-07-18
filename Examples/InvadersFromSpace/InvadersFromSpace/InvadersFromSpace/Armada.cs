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

        public Armada(Int32 rows, Int32 cols, Rectangle field) {

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
                    Invaders[c + r * cols] = Invader.CreateInvader(Sprites.Invader1,
                        ArmadaLocation.X + c * (Sprites.Invader1[0].Width + InvaderSpacing),
                        ArmadaLocation.Y + r * (Sprites.Invader1[0].Height + InvaderSpacing),
                        InvaderColors[r]);
                }
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
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < Invaders.Length; i++)
                Invader.Draw(spriteBatch, Invaders[i], ArmadaFrame);
        }

    }
}
