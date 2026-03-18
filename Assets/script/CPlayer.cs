using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameObject monst;

    public Rigidbody2D rb;
    public float speed;  // vitesse du joueur
    public float jumpingPower; // force du saut
    public LayerMask GroundLayer; // check s'il marche au sol
    public Transform GroundCheck; 
    float horizontal; // variable 

    SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);  // la vitesse deplacement, et y on ne touche pas
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x; // lire le clavier, x = que les touches de gauche a droite

        if (horizontal < 0) // si je click a gauche (-1) le perso flip
        {
            sr.flipX = true;
        }
        else // si je click a droite (1) le perso continue a droite
        {
            sr.flipX = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded()) // si space a ete touche && il est au sol
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower); // x ne change pas et y est saut
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(GroundCheck.position, new Vector2(1f, .1f), CapsuleDirection2D.Horizontal, 0, GroundLayer); // check si le player est au sol
    }
}
