using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private int scoreValue = 1;
    [SerializeField] private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Đảm bảo Player có tag "Player"
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                gameManager.AddScore(scoreValue);
                Destroy(gameObject);
            }
        }
    }
}
