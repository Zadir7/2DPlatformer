using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SpriteAnimator", menuName = "Configs/SpriteAnimator", order = 0)]
    public sealed class SpriteAnimatorConfig : ScriptableObject
    {
        public List<SpriteSequence> spriteSequences = new List<SpriteSequence>();
    }

    [Serializable]
    public sealed class SpriteSequence
    {
        public AnimationType type;
        public List<Sprite> sprites = new List<Sprite>();
    }

    public enum AnimationType
    {
        Idle = 0,
        Run = 1,
        Jump = 2
    }
}