using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        livesText.text = "lives: " + playerLives;
        scoreText.text = "score: "+score;
    }

    public void PlayerDamage()
    {
        if (playerLives > 1)
        {
            TakeDamage();
        }
        else
        {
            RestartLevel();
        }
    }

    void TakeDamage()
    {
        playerLives--;
        livesText.text = "lives: " + playerLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RestartLevel()
    {
        ScenePersist.Instance.ResetPersistData();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "score: "+score;
    }
}
