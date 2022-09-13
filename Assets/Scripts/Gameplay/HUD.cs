// HUD component 
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    #region Fields
    [SerializeField]
    GameObject scoreTextGameObject;
    [SerializeField]
    GameObject healthPointsTextGameObject;

    [SerializeField]
    GameObject waveNumberTextGameObject;

    static TextMeshProUGUI scoreText;
    static TextMeshProUGUI healthPointsText;
    static TextMeshProUGUI waveNumberText;

    const string ScoreTextPrefix = "SCORE: ";
    const string HealthPoints = "<3\n";

    const string WaveTextPrefix = "WAVE: ";

    static int score = 0;
    static int hudWaveNumber = 1;
    #endregion

    #region Public properties
    public int Score
    {
        get{ return score; }
    }
    #endregion

    #region  Unity methods
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = scoreTextGameObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = ScoreTextPrefix + score.ToString();

        healthPointsText = healthPointsTextGameObject.GetComponent<TextMeshProUGUI>();
        healthPointsText.text = "";

        waveNumberText = waveNumberTextGameObject.GetComponent<TextMeshProUGUI>();
        waveNumberText.text = WaveTextPrefix + hudWaveNumber.ToString();
        
        EventManager.AddAddPointsListener(AddPoints);
        EventManager.AddDrawHealthPointsListener(DrawHealthPoints);
        EventManager.AddSetHudWaveNumberListener(SetHudWaveNumber);
    }
    #endregion

    #region Private methods
    static void AddPoints(int points)
    {
        score += points;
        scoreText.text = ScoreTextPrefix + score.ToString(); 
    }

    static void DrawHealthPoints(int hearthAmount)
    {
        healthPointsText.text = "";
        for(int i = 0; i < hearthAmount; i++)
        {
            healthPointsText.text += HealthPoints;
        }
    }

    static void SetHudWaveNumber(int wave)
    {
        hudWaveNumber = wave;
        waveNumberText.text = WaveTextPrefix + hudWaveNumber.ToString();
    }
    #endregion
}
