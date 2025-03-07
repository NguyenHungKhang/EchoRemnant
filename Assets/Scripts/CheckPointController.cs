using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private bool isChecked = false;

    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isChecked)
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    SetCheckpoint();
                    GameManager.instance.SetLastCheckPoint(this);
                }
            }
    }

    public void SetCheckpoint()
    {
        if (!isChecked)
        {
            animator.SetTrigger("playerCheck");
            isChecked = true;
        }
    }
    
    public void DisableCheckPoint()
    {
        if (isChecked)
        {
            isChecked = false;
        }
    }
    
    void HandleAnimation()
    {
        animator.SetBool("isChecked", isChecked);
    }
}