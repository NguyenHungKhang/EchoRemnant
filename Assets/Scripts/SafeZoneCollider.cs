using UnityEngine;

public class SafeZoneCollider : MonoBehaviour
{
    private int damage = 999;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && !GameManager.instance.IsGameWin())
            {
                player.TakeDamage(damage);
            }
        }
    }
}