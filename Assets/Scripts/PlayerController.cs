using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private int jumpCounter = 0;
    private int maxJumps = 1;
    private bool isRolledJump = false;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(6f, 16f);

    private int totalHP = 1;
    private int currentHP;

    private bool isAlive = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator animator;

    private void Start()
    {
        currentHP = totalHP;
        isAlive = true;
    }

    private void Update()
    {
        if (GameManager.instance.IsGameOver())
        {
            return;
        }
        if (GameManager.instance.IsGameWin())
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
      
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();
        WallSlide();
        WallJump();
        HandleAnimation();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    public void GetJumpGem()
    {
        jumpCounter++;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, groundLayer);
    }

    private bool IsDoubleJumping()
    {
        return isRolledJump;
    }

    private bool IsJumpAble()
    {
        return jumpCounter > 0;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") & IsJumpAble())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            if (!IsGrounded() && !IsWalled())
            {
                jumpCounter--;
                animator.SetTrigger("isRolledJumping");
            }
        }

        if (IsGrounded() || IsWalled())
        {
            jumpCounter = maxJumps;
            isRolledJump = false;
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,
                Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else if(isWallSliding)
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        else
        {
            wallJumpingCounter = 0;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        this.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        animator.SetTrigger("isDead");
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }

    void HandleAnimation()
    {
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.1f);
        animator.SetBool("isJumping", !IsGrounded() && !IsWalled());
        animator.SetFloat("verticalSpeed", rb.linearVelocityY > 0 ? 1 : -1);
        animator.SetBool("isWallSliding", isWallSliding);
    }
}