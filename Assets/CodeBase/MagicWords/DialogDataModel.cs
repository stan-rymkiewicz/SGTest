using System;
using System.Collections.Generic;

namespace CodeBase.MagicWords
{
    [Serializable]
    public class DialogueEntry
    {
        public string name;
        public string text;
    }

    [Serializable]
    public class AvatarEntry
    {
        public string name;
        public string url;
        public string position;
    }

    [Serializable]
    public class DialogueData
    {
        public List<DialogueEntry> dialogue;
        public List<AvatarEntry> avatars;
    }

}