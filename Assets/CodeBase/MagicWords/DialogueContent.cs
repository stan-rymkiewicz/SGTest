using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.MagicWords
{
    public class DialogueContent
    {
        private const string LeftPositionString = "left";
        public DialogueData DialogueData;
        public Dictionary<string, Sprite> avatarCache = new Dictionary<string, Sprite>();

        public bool IsLeftPositionAvatart(AvatarEntry avatar)
        {
            return avatar.position == LeftPositionString;
        }
    }
}