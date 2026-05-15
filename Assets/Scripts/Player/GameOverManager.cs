using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
public void ShowGameOver()
{
    if (gameOverPanel != null)
    {
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("✅ Game Over Screen Shown");
    }
}

public void RestartGame()
{
    Time.timeScale = 1f;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }
    
}