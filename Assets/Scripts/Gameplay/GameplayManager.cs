using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Fields
    bool endGame = false;
    #endregion

    #region Unity methods
    // Update is called once per frame
    void Update()
    {
        // pause game on escape key if game isn't currently paused
		if (Input.GetKeyDown(KeyCode.Escape) &&
			Time.timeScale != 0)
		{
            AudioManager.Play(AudioClipName.MenuButtonClick);
			MenuManager.GoToMenu(MenuName.Pause);
		}
        
        if ((GameObject.FindGameObjectsWithTag("Ship").Length == 0)
            && !endGame)
		{
			EndGame();
		}
    }
    #endregion

    #region Private methods
	// Ends the game
	void EndGame()
	{
        endGame = !endGame;

		// instantiate prefab and set score
		GameObject gameOverMessage = Instantiate(Resources.Load("MenuPrefabs/GameOverMessage")) as GameObject;
		GameOverMessage gameOverMessageScript = gameOverMessage.GetComponent<GameOverMessage>();
		GameObject hud = GameObject.FindGameObjectWithTag("HUD");
		HUD hudScript = hud.GetComponent<HUD>();
		gameOverMessageScript.SetScore(hudScript.Score);
        //hud.SetActive(false);
	}
    #endregion
}
