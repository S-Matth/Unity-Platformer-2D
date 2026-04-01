using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    // Variable SerializeField (actualisable dans l'inspector)
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float wallSlidingSpeed = 0.5f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float wallJumpForce = 3f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask lmGround;
    [SerializeField] private LayerMask lmWall;

    // Variable Move
    private Rigidbody2D rb;
    Vector2 moveInput;
    private float lastXDirection = 1f;

    // Variable Jump
    public SpriteRenderer sp;
    private bool isGrounded;
    private Transform groundCheck;
    private float maxJump = 2;
    private float cptJump = 0;

    // Variable wallSliding
    private bool isWallSliding;
    private Transform wallCheck;

    // Variable wallJump
    private float wallJumpTimer;
    
    private Vector2 wallJumpDirection = new Vector2(1f, 1.5f);
    private float wallJumpDuration = 0.2f;
    private bool isWallJumping;

    // Variable Animator
    private Animator animator;

    // Variable Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 5f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 1f;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck");
    }

    private void Update()
    {
        // Sens du personnage
        OrientationCharactere();

        // Est ce que le personnage est au sol ?
        IsGrounded();
        IsWalled();

        // Gestion des animations du personnage
        GestionAnimation(); 

        // Gestion du wall slide
        WallSlide();
        HandleWallJumpTimer();
    }

    private void FixedUpdate()
    {
        if(isDashing) return;
        if (!isWallJumping) rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);

    }

    private void IsGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, lmGround);
        if (isGrounded && !wasGrounded) cptJump = 0;
    }

    private bool IsWalled() => Physics2D.OverlapCircle(wallCheck.position, 0.05f, lmWall);

    private void WallSlide()
    {
        if (isWallJumping)
        {
            isWallSliding = false;
            return;
        }

        if (IsWalled() && !isGrounded && moveInput.x != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -wallSlidingSpeed, float.MaxValue)); 
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void HandleWallJumpTimer()
    {
        if (isWallJumping)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0f)
            {
                isWallJumping = false;
            }
        }
    }

    private void GestionAnimation()
    {
        // Animation Move
        animator.SetFloat("val_x", Mathf.Abs(moveInput.x));

        // Animation Jump
        animator.SetFloat("val_y", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);
    }

    public void OrientationCharactere()
    {
        if (moveInput.x != 0) lastXDirection = Mathf.Sign(moveInput.x);
        sp.flipX = (lastXDirection < 0);

        wallCheck.localPosition = new Vector3(0.05f * lastXDirection, wallCheck.localPosition.y, 0);
    }

    private void WallJump()
    {
        isWallSliding = false;
        isWallJumping = true;
        wallJumpTimer = wallJumpDuration;

        // direction opposée au mur
        float jumpDirection = -lastXDirection;

        rb.linearVelocity = new Vector2(
            wallJumpDirection.x * jumpDirection * wallJumpForce,
            wallJumpDirection.y * wallJumpForce);
    }

    private void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (isGrounded)
            {
                rb.linearVelocityY = jumpForce;
                cptJump++;
            }

            else if (!isGrounded && !IsWalled() && maxJump > cptJump)
            {
                rb.linearVelocityY = jumpForce;
                cptJump++;
            }
            else if (IsWalled())
            {
                cptJump = 2;
                WallJump();
            }
        }   
    }

    private IEnumerator OnDash(InputValue value)
    {
        if (value.isPressed)
        {
            // Si le personnage ne peut pas dash ou s'il est en train de dash, on sort de la fonction
            canDash = false;
            isDashing = true;

            // On stocke la gravité originale pour la rétablir après le dash
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;

            // On applique la vitesse de dash dans la direction du personnage
            rb.linearVelocity = new Vector2(lastXDirection * dashPower, 0f);

            // On active l'effet de traînée du dash
            tr.emitting = true;

            // On attend la durée du dash
            yield return new WaitForSeconds(dashDuration);

            // On désactive l'effet de traînée et on rétablit la gravité initiale
            tr.emitting = false;
            rb.gravityScale = originalGravity;

            // On réinitialise les variables de dash
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
}
