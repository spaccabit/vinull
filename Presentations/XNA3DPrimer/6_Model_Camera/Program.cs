using System;

namespace _6_Model_Camera {
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

