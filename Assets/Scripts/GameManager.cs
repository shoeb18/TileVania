using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddCoin()
    {
        coins++;
    }
}
