using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeBase.MagicWords
{
    public class EmojisResolver
    {
        //Shortcut - must be in configs
        private Dictionary<string, string> emojiMoodToUnicodeTable = new Dictionary<string, string>
        {
            {"satisfied", "\U0001F60A"},
            {"intrigued", "\U0001F60A"},
            {"neutral", "\U0001F60A"},
            {"affirmative", "\U0001F60A"},
        };
        
        string pattern = @"\{.*?\}";
        
        string defaultReplacement  = "\U0001F60A";
        
        public void InsertEmojiUnicode(DialogueContent dialogueContent)
        {
            foreach (var entry in dialogueContent.DialogueData.dialogue)
            {
                entry.text = Regex.Replace(entry.text, pattern, defaultReplacement);
            }
        }
    }
}