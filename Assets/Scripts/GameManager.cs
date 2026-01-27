using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuracion de Tiempo")]
    public float gameTime = 300f;
    private bool gameEnded = false;

    [Header("Puntuacion")]
    public int playerPoints = 0;
    public int robotPoints = 0;

    [Header("UI Referencias")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playerPointsText;
    public TextMeshProUGUI robotPointsText;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI resultScoreText;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        if (gameEnded) return;

        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);

        if (timerText)
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (playerPointsText) playerPointsText.text = "Player: " + playerPoints;
        if (robotPointsText) robotPointsText.text = "Robot: " + robotPoints;
    }

    public void AddPoints(bool isPlayer, int amount)
    {
        if (gameEnded) return;

        if (isPlayer) playerPoints += amount;
        else robotPoints += amount;
    }

    void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerPoints >= robotPoints)
        {
            winPanel.SetActive(true);
            if (resultScoreText) resultScoreText.text = "Puntos finales: " + playerPoints;
        }
        else
        {
            losePanel.SetActive(true);
            if (resultScoreText) resultScoreText.text = "Puntos finales: " + playerPoints;
        }
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
