using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float wallSlidingSpeed = 0.5f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);
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

    // Variable wallSliding
    private bool isWallSliding;
    private Transform wallCheck;

    // Variable wallJumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingDuration = 0.4f;
    private bool isJumping = false;

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
        if (!isWallJumping) OrientationCharactere();

        // Est ce que le personnage est au sol ?
        IsGrounded();
        IsWalled();

        // Gestion des animations du personnage
        GestionAnimation(); 

        // Gestion du wall slide
        WallSlide();
        //WallJump();
    }

    private void FixedUpdate()
    {
        if (!isWallJumping) rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);

    }


    private void IsGrounded() => isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, lmGround);

    private bool IsWalled() => Physics2D.OverlapCircle(wallCheck.position, 0.1f, lmWall);

    private void WallSlide()
    {
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

        wallCheck.localPosition = new Vector3(0.1f * lastXDirection, wallCheck.localPosition.y, 0);
    }

        
        


    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -moveInput.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (isJumping && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (moveInput.x != wallJumpingDirection) OrientationCharactere();

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping() => isWallJumping = false;
    private void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            isJumping = true;
            if(isGrounded) rb.linearVelocityY = jumpForce;
        }
        else isJumping = false;
            
    }

    
}
