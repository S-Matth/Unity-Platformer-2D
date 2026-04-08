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
    private float dashCooldown = 3f;

    // Variable FX pour gérer les particules
    public ParticleSystem smokeFX;

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
        // Met à jour l’orientation du sprite
        OrientationCharactere();

        // Effet de poussière lors de la course
        HandleRunDust();

        // Détection du sol et des murs
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
        // Si le joueur dash, on ignore la physique normale
        if (isDashing) return;

        // Si le joueur n'est pas en wall jump, on applique le mouvement horizontal
        if (!isWallJumping) 
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);

    }
    
    private void HandleRunDust()
    {
        // Active la poussière si le joueur court au sol
        if (isGrounded && Mathf.Abs(moveInput.x) > 0.1f)
        {
            if (!smokeFX.isPlaying)
                smokeFX.Play();
        }
        else
        {
            if (smokeFX.isPlaying)
                smokeFX.Stop();
        }
    }

    private void IsGrounded()
    {
        // Vérifie si le joueur touche le sol via un petit cercle
        bool wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, lmGround);

        // Si on vient d’atterrir, on réinitialise le compteur de sauts
        if (isGrounded && !wasGrounded) 
            cptJump = 0;
    }

    private bool IsWalled() => 
        Physics2D.OverlapCircle(wallCheck.position, 0.05f, lmWall); // Détection du mur

    private void WallSlide()
    {
        // Si on est en wall jump, on ne glisse pas
        if (isWallJumping)
        {
            isWallSliding = false;
            return;
        }

        // Conditions du wall slide : contre un mur, en l’air, et en mouvement horizontal
        if (IsWalled() && !isGrounded && moveInput.x != 0)
        {
            isWallSliding = true;

            // On limite la vitesse verticale pour créer l’effet de glissade
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -wallSlidingSpeed, float.MaxValue)); 
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void HandleWallJumpTimer()
    {
        // Réduit le timer du wall jump
        if (isWallJumping)
        {
            wallJumpTimer -= Time.deltaTime;

            // Quand le timer expire, on rend le contrôle au joueur
            if (wallJumpTimer <= 0f)
            {
                isWallJumping = false;
            }
        }
    }

    private void GestionAnimation()
    {
        // Animation du déplacement
        animator.SetFloat("val_x", Mathf.Abs(moveInput.x));

        // Animation du saut
        animator.SetFloat("val_y", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);
    }

    public void OrientationCharactere()
    {
        // Met à jour la direction du personnage selon l’input
        if (moveInput.x != 0)
            lastXDirection = Mathf.Sign(moveInput.x);

        // Flip du sprite
        sp.flipX = (lastXDirection < 0);

        // Déplace le point de détection du mur selon la direction
        wallCheck.localPosition = new Vector3(0.05f * lastXDirection, wallCheck.localPosition.y, 0);
    }

    private void WallJump()
    {
        // On désactive le wall slide
        isWallSliding = false;

        // On active le wall jump
        isWallJumping = true;
        wallJumpTimer = wallJumpDuration;

        // direction opposée au mur
        float jumpDirection = -lastXDirection;

        // On applique la force du wall jump
        rb.linearVelocity = new Vector2(
            wallJumpDirection.x * jumpDirection * wallJumpForce,
            wallJumpDirection.y * wallJumpForce);
    }

    private void OnMove(InputValue value) => 
        moveInput = value.Get<Vector2>(); // Récupère l’input du joueur

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            // Saut normal au sol
            if (isGrounded)
            {
                rb.linearVelocityY = jumpForce;
                cptJump++;
            }

            // Double saut
            else if (!isGrounded && !IsWalled() && maxJump > cptJump)
            {
                rb.linearVelocityY = jumpForce;
                cptJump++;
            }

            // Wall jump
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
            // Empêche le spam du dash
            canDash = false;
            isDashing = true;

            // On désactive la gravité pendant le dash
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
            // Fin du dash
            isDashing = false;

            // Cooldown avant de pouvoir dash à nouveau
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
}
