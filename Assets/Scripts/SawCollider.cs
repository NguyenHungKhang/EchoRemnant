using UnityEngine;

public class SawCollider : MonoBehaviour
{
    private int damage = 1;
    private Vector2 worldPointA, worldPointB;
    public bool isMovabvle = false;
    public Transform pointA, pointB;
    public float speed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isMovabvle)
        {
            worldPointA = pointA.position;
            worldPointB = pointB.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovabvle)
        {
            float t = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector2.Lerp(worldPointA, worldPointB, t);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
