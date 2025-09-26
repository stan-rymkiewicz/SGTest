
using System.Linq;
using CodeBase.Core;
using Unity.VisualScripting;
using UnityEngine;


namespace CodeBase.MagicWords
{
    public class MagicWordsModuleController : GameModuleController
    {
       
        
        public string jsonUrl;

        public override void Initialize()
        {
            base.Initialize();
            
            _dialogueContent = new DialogueContent();
            
            var downloader = new JsonDataDownloader<DialogueData>(this);
           
            downloader.Download(jsonUrl, (success, dialogueData) =>
            {
                if (success)
                {
                    _dialogueContent.DialogueData = dialogueData;
                    
                    OnDataLoaded();
                }
                else
                {
                    Debug.LogError("Failed to  parse Dialogue data.");
                }
            });
        }


        private DialogueContent _dialogueContent;
        
        [SerializeField] private MagicWordsUIController magicWordsUIController;

       
        private void LoadAvatars()
        {
            var downloader = new SpritesDownloader(this);
           
            var urls = _dialogueContent.DialogueData.avatars.Select(a => a.url).ToArray();
                
            downloader.Download(urls, (success, sprites) =>
            {
                if (success)
                {
                    _dialogueContent.avatarCache.AddRange(sprites);
                }
                else
                {
                    Debug.LogError("Failed to  load avatar sprites.");
                }
                
                OnAvatarsLoaded();
            });
        }

        
        private void OnDataLoaded()
        {
            InsertEmojis();
            
            LoadAvatars();
        }

        private void InsertEmojis()
        {
            EmojisResolver resolver = new EmojisResolver();
            
            resolver.InsertEmojiUnicode(_dialogueContent);
        }

        private void OnAvatarsLoaded()
        {
            magicWordsUIController.PopulateDialogue(_dialogueContent);

            StartGameLoop();
        }

        public override void StartGameLoop()
        {
            magicWordsUIController.StartDialogue();
        }

        public void ActivateNextDialogue()
        {
            magicWordsUIController.ActivateNextDialogue();
        }
        
    }
    
}