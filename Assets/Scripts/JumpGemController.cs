using UnityEngine;

public class JumpGemController : MonoBehaviour
{
    private float respawnTime = 4f;

    private float respawnCounter = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RespawnJumpGem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Đảm bảo Player có tag "Player"
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetJumpGem();
                gameObject.SetActive(false);
                respawnCounter = respawnTime;
            }
        }
    }

    private void RespawnJumpGem()
    {
        if (respawnCounter > 0)
            respawnCounter -= Time.deltaTime;
        else
        {
            gameObject.SetActive(true);
            respawnCounter = 0;
        }
    }
}