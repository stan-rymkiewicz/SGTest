using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.MagicWords
{
    public class MagicWordsUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _leftDialogueEntryPrefab;
        [SerializeField] private GameObject _rightDialogueEntryPrefab;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _dialogueRoot;
        
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private CanvasGroup _startCanvasGroup;
        
        private  int _dialogueEntryIndex = 0;
        private  List<DialogUIEntry> dialogues = new List<DialogUIEntry>();

        private void Awake()
        {
            SwitchMainCanvasGroup(false);
        }

        public void StartDialogue()
        {
            SwitchMainCanvasGroup(true);
            dialogues[_dialogueEntryIndex].gameObject.SetActive(true);
            _scrollRect.verticalNormalizedPosition = 1.0f;
          
        }

        private void SwitchMainCanvasGroup(bool turnOn)
        {
            _mainCanvasGroup.alpha = turnOn ? 1 : 0;
            _mainCanvasGroup.interactable = turnOn;
            
            _startCanvasGroup.interactable = !turnOn;
            _startCanvasGroup.alpha = !turnOn ? 1 : 0;;
        }

        public void ActivateNextDialogue()
        {
            _dialogueEntryIndex = Mathf.Clamp(_dialogueEntryIndex + 1, 0, dialogues.Count - 1);
            dialogues[_dialogueEntryIndex].gameObject.SetActive(true);
            _scrollRect.verticalNormalizedPosition = 0f;
        }
        
        public void PopulateDialogue(DialogueContent content)
        {
            dialogues = new List<DialogUIEntry>(content.DialogueData.dialogue.Count);
            foreach (var entry in content.DialogueData.dialogue)
            {
                AvatarEntry avatar = content.DialogueData.avatars.FirstOrDefault( x => x.name == entry.name);

                if (avatar == null)
                {
                    continue;
                }

                GameObject dialogueEntry = Instantiate(content.IsLeftPositionAvatart(avatar) ? _leftDialogueEntryPrefab : _rightDialogueEntryPrefab, _dialogueRoot);
                
                DialogUIEntry dialogueUIEntry = dialogueEntry.GetComponent<DialogUIEntry>();
                
                dialogueUIEntry.SetEntry(entry);
                
                
                if (content.avatarCache.ContainsKey(avatar.url))
                {
                    dialogueUIEntry.SetAvatarImage(content.avatarCache[avatar.url]);
                }
                
                
                dialogueUIEntry.gameObject.SetActive(false);
                
                dialogues.Add(dialogueUIEntry);
            }
        }
    }
}