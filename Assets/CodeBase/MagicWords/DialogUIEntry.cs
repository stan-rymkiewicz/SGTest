using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeBase.MagicWords
{
    public class DialogUIEntry : MonoBehaviour
    {
        public Image avatarImage;           
        public TextMeshProUGUI nameText;    
        public TextMeshProUGUI dialogueText; 

        
        public void SetEntry(DialogueEntry dialogueEntry)
        {
            if (nameText)
            {
                nameText.text = dialogueEntry.name;
            }

            if (dialogueText)
            {
                dialogueText.text = dialogueEntry.text;
            }
           
        }
        
        public void SetAvatarImage(Sprite sprite)
        {
            avatarImage.sprite = sprite;
        }
    }
}