using UnityEngine;

public class EndPositionCollider : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        animator.SetBool("isWin", false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                audioManager.StopMusic();
                audioManager.PlaySFX(audioManager.winClip);
                animator.SetTrigger("touchFlag");
                animator.SetBool("isWin", true);
                GameManager.instance.GameWin();
            }
        }
    }
}