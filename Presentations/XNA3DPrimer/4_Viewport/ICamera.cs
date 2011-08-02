using System;
using Microsoft.Xna.Framework;
namespace _4_Viewport {
    interface ICamera {
        Matrix Projection { get; set; }
        Matrix View { get; set; }
        Matrix World { get; set; }
    }
}
