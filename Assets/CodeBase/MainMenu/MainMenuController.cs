using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [System.Serializable]
        public class MenuButton
        {
            public Button button; 
            public string sceneName;
        }

        [Header("Menu Buttons")]
        public MenuButton[] menuButtons;

        private void Start()
        {
            foreach (var menuButton in menuButtons)
            {
                if (menuButton.button != null && !string.IsNullOrEmpty(menuButton.sceneName))
                {
                    menuButton.button.onClick.AddListener(() => LoadScene(menuButton.sceneName));
                }
            }
        }

        private void LoadScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}