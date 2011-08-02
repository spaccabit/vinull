using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace XNA_3D_Primer_Pipeline.Content {

    public class MeshAnimationInfo {

        [ContentSerializer]
        public Dictionary<String, Int32> BoneNames;

        [ContentSerializer]
        public List<Matrix> BoneTransforms;

        [ContentSerializer]
        public List<Matrix> InverseBoneTransforms;

        [ContentSerializer]
        public List<Int32> BoneParent;

        [ContentSerializer]
        public Dictionary<String, List<AnimationFrame>> Timelines;

        public MeshAnimationInfo() {
            BoneNames = new Dictionary<String, Int32>();
            BoneTransforms = new List<Matrix>();
            InverseBoneTransforms = new List<Matrix>();
            BoneParent = new List<Int32>();
            Timelines = new Dictionary<String, List<AnimationFrame>>();
        }
    }

    public class AnimationFrame {

        [ContentSerializer]
        public TimeSpan Time;

        [ContentSerializer]
        public Dictionary<Int32, Matrix> BoneTransforms;

        public AnimationFrame() {
            BoneTransforms = new Dictionary<Int32, Matrix>();
        }
    }
}
