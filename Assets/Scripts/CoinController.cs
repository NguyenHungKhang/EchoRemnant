using TMPro;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    private int scoreValue = 1;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                audioManager.PlaySFX(audioManager.coinClip);
                gameManager.AddScore(scoreValue);
                gameObject.SetActive(false);
            }
        }
    }
}