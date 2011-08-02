using System;

namespace _1_3D_Overview
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameMain game = new GameMain())
            {
                game.Run();
            }
        }
    }
}

