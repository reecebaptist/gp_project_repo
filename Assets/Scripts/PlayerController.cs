using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 10f;

    private enum State { idle, running, jumping, falling, hurt};
    private State state = State.idle;
    private Collider2D coll;

    // Start is called only at the beginning
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        

        AnimationState();

        //Set Animation as per the enumarators.
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            } 

            else
            {
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right so damage and then move to my left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                } 

                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
            
        }
    } 

    private void Movement()
    {
        float HDirection = Input.GetAxis("Horizontal");
        //Move Left
        if (HDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }

        //Move Right
        else if (HDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    { 
        //Jumping animation change
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        //Falling animation change
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        //Hurt Animation change
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
        
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving;
            state = State.running;
        } 

        else
        {
            state = State.idle;
        }
        
    }
}
