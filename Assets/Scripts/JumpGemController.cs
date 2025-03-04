using UnityEngine;

public class JumpGemController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                player.GetJumpGem();
                Destroy(gameObject);
            }
        }
    }
}
