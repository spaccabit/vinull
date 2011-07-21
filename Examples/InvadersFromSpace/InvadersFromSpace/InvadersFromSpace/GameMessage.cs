using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace InvadersFromSpace {
    public static class GameMessage {

        public static Boolean Active;

        private static String message;
        private static Boolean pressed;
        private static Color background = new Color(0f, 0f, 0f, .60f);
        private static Vector2 location;

        public static void SetMessage(String msg) {
            Active = true;
            message = msg;
            location = Sprites.MessageFont.MeasureString(msg);
            location.X = GameMain.Screen.Width / 2 - location.X / 2;
            location.Y = GameMain.Screen.Height / 2 - location.Y / 2;
        }

        public static void Update() {
            if (Active) {
                KeyboardState keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Enter)) 
                    pressed = true;
                else if (pressed && keys.IsKeyUp(Keys.Enter)) {
                    pressed = false;
                    Active = false;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch) {
            if (Active) {
                spriteBatch.Draw(Sprites.SpriteSheet, GameMain.Screen, Sprites.Solid, background);
                spriteBatch.DrawString(Sprites.MessageFont, message, location, Color.White);
            }
        }
    }
}
