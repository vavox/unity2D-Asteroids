using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    #region Unity methods
    // Goes back to the main menu
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Play(AudioClipName.MenuButtonClick);
            MenuManager.GoToMenu(MenuName.Main);
        }
    }
    #endregion
}
