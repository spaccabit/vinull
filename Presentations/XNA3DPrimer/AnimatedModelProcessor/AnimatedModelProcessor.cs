using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using XNA_3D_Primer_Pipeline.Content;
using System.IO;

namespace XNA_3D_Primer_Pipeline.Processors {
    
    [ContentProcessor(DisplayName = "AnimatedModel - XNA 3D Primer")]
    public class AnimatedModelProcessor : ModelProcessor {
        
        public override ModelContent Process(NodeContent input, ContentProcessorContext context) {
            BoneContent skel = MeshHelper.FindSkeleton(input);
            IList<BoneContent> bones = MeshHelper.FlattenSkeleton(skel);

            MeshAnimationInfo ani = new MeshAnimationInfo();
            foreach (BoneContent bone in bones) {
                ani.BoneNames.Add(bone.Name, bones.IndexOf(bone));
                ani.BoneParent.Add(bones.IndexOf(bone.Parent as BoneContent));
                ani.BoneTransforms.Add(bone.Transform);
                ani.InverseBoneTransforms.Add(Matrix.Invert(bone.AbsoluteTransform));
            }

            foreach (var timeline in skel.Animations) {
                foreach (var bone in timeline.Value.Channels) {
                    Int32 boneIdx = ani.BoneNames[bone.Key];
                    foreach (var frame in bone.Value) {
                        if (!ani.Timelines.ContainsKey(timeline.Key))
                            ani.Timelines[timeline.Key] = new List<AnimationFrame>();
                        if (ani.Timelines[timeline.Key].Count - 1 < bone.Value.IndexOf(frame))
                            ani.Timelines[timeline.Key].Add(new AnimationFrame() { Time = frame.Time });
                        ani.Timelines[timeline.Key][bone.Value.IndexOf(frame)].BoneTransforms[boneIdx] = frame.Transform;
                    }
                }
            }

            ModelContent model = base.Process(input, context);
            model.Tag = ani;

            return model;
        }
    }
}