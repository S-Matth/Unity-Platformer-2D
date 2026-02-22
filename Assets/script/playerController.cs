using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float wallSlidingSpeed = 0.5f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float wallJumpForce = 3f;
    [SerializeField] private LayerMask lmGround;
    [SerializeField] private LayerMask lmWall;



    // Variable Move
    private Rigidbody2D rb;
    Vector2 moveInput;
    private float lastXDirection = 1f;

    // Variable Jump
    private SpriteRenderer sp;
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

    private Animator animator;
    

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
        if(!isWallJumping) rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);

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

    private void OrientationCharactere()
    {
        if (moveInput.x != 0) lastXDirection = Mathf.Sign(moveInput.x);
        sp.flipX = (lastXDirection < 0);

        wallCheck.localPosition = new Vector3(0.05f * lastXDirection, wallCheck.localPosition.y, 0);
    }

    private void WallJump()
    {
        isWallSliding = false ;
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

    
}
