using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin();
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 1f);
            Destroy(gameObject);
        }   
    }
}
