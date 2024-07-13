using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float dirX = 0f;
   [SerializeField] private float moveSpeed = 7f;     //[SerializeField] - Making it accessible and editable in the Unity Inspector.
   [SerializeField] private float jumpForce = 14f;
   [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    private enum MovementState { idle, running, jumping, falling }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //Raw - No sliding on stopping.
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);  //Vector2 --> X and Y dims.

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;    //Mirroring player to left side.
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -1f)   //downward force.
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);  //to convert the enum value to its corresponding integer value.
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);  //Create shape around player similar to the box collider. 0f -->rotation nil. Vector2.down, .1f -->Moves box down a tiny bit.
    }

}