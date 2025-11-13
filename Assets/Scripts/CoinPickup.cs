using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinScore = 50;
    bool isCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.Instance.AddScore(coinScore);
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 0.5f);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }   
    }
}
