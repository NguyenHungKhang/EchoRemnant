using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public float speedX = -2f; // Tốc độ di chuyển ngang
    public float resetX = -10f; // Khi nào reset Tilemap
    public float startX = 10f; // Vị trí bắt đầu lại

    void Update()
    {
        transform.position += Vector3.right * speedX * Time.deltaTime;

        // Khi Tilemap đi quá điểm reset, đưa nó về đầu để lặp lại
        if (transform.position.x <= resetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }
}