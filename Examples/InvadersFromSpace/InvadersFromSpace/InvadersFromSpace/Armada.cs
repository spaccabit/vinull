using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using InvadersFromSpace.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace InvadersFromSpace {
    public class Armada {

        public Invader[] Invaders;
        static Color[] InvaderColors = new Color[] { Color.Purple, Color.Blue, Color.Red, Color.Orange, Color.Yellow, Color.Silver };
        
        Int32 ArmadaFrame;
        double ArmadaMoveTimer;
        double ArmadaMoveRate = 50;
        Int32 ArmadaSpeed = 5;
        Int32 ArmadaDirection = 1;
        public Rectangle ArmadaLocation;
        Rectangle Field;

        Int32 startx;
        Int32 starty;
        public Boolean Landed;

        public Armada(Int32 rows, Int32 cols, Int32 x, Int32 y, Rectangle field) {

            Invaders = new Invader[rows * cols];
            Field = field;
            startx = x;

            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    Invaders[c + r * cols] = Invader.CreateInvader(SpriteSheet.Invader1,
                        field.X + c * SpriteSheet.Invader1[0].Height + c * 10,
                        field.Y + r * SpriteSheet.Invader1[0].Width + r * 10,
                        InvaderColors[r]);
                }
            }
            ArmadaLocation = new Rectangle(Invaders[0].Location.X, Invaders[0].Location.Y,
                Invaders[Invaders.Length - 1].Location.X + Invaders[Invaders.Length - 1].Location.Width - Invaders[0].Location.X,
                Invaders[Invaders.Length - 1].Location.Y + Invaders[Invaders.Length - 1].Location.Height - Invaders[0].Location.Y);
            ArmadaFrame = 0;
            ArmadaMoveTimer = 0;
            Landed = false;
        }

        public void UpdateArmadaLocation() {
            ArmadaLocation.X = 0;
            ArmadaLocation.Y = 0;
            ArmadaLocation.Width = 0;
            ArmadaLocation.Height = 0;

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
            }


        }

        public void Update(GameTime gameTime) {

            ArmadaMoveTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ArmadaMoveTimer >= ArmadaMoveRate) {
                ArmadaMoveTimer = 0;
                ArmadaFrame = ArmadaFrame == 0 ? 1 : 0;
                ArmadaLocation.X += ArmadaSpeed * ArmadaDirection;
                if (!Field.Contains(ArmadaLocation)) {
                    ArmadaLocation.X -= ArmadaSpeed * ArmadaDirection;
                    ArmadaDirection *= -1;

                    ArmadaLocation.Y += 20;
                    if (!Field.Contains(ArmadaLocation)) {
                        for (int i = 0; i < Invaders.Length; i++) {
                            Invaders[i].Active = false;
                            Landed = true;
                        }
                    }
                    else {
                        for (int i = 0; i < Invaders.Length; i++) {
                            Invaders[i].Location.Y += 20;
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
