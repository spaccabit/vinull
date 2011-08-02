using System;

namespace _5_LoadModel {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            using (GameMain game = new GameMain()) {
                game.Run();
            }
        }
    }
}

