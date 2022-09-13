using UnityEngine;
using TMPro;

public class GameOverMessage : MonoBehaviour
{
    #region Fields
    [SerializeField]
    TextMeshProUGUI messageText;
    #endregion

    #region Unity methods
    void Start()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
    }
    #endregion

    #region Public methods
    // Sets score
    public void SetScore(int score)
    {
        messageText.text = "Game Over!\n\nYour score: " +
            score.ToString();
    }

    public void RestartGame()
    {
        // unpause game and destroy menu
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Play);
    }

    // Moves to main menu when quit button clicked
    public void QuitGame()
    {
        // unpause game, destroy menu, and go to main menu
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
    #endregion
}
