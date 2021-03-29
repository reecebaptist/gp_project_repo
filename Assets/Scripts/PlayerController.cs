using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource cherry;
    [SerializeField] float climbSpeed = 3f;
    

    private enum State { idle, running, jumping, falling, hurt, climb };
    private State state = State.idle;
    private Collider2D coll;
    private float HDirection = 0f;
    private int lastLooked = 1;
    private float naturalGravity;

    //Ladder variables
    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool bottomLadder = false;
    [HideInInspector] public bool topLadder = false;
    public LadderController ladder;


    // Start is called only at the beginning
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        naturalGravity = rb.gravityScale;
        PermanentUIController.perm.healthAmount.text = PermanentUIController.perm.health.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if(state == State.climb)
        {
            Climb();
        }
        
        else if(state != State.hurt)
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
            cherry.Play();
            Destroy(collision.gameObject);
            PermanentUIController.perm.cherries += 1;
            PermanentUIController.perm.cherryText.text = PermanentUIController.perm.cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyController frog = other.gameObject.GetComponent<EnemyController>();
            if(state == State.falling)
            {
                frog.JumpedOn();
                Jump();
            } 

            else
            {
                state = State.hurt;
                HandleHealth(); //Deals with Health
                if (other.gameObject.transform.position.x > transform.position.x)
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
        HDirection = Input.GetAxis("Horizontal");

        if(canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            state = State.climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;
            
        }

        //Move Left
        if (HDirection < 0)
        {
            rb.velocity = new Vector2(HDirection*speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            lastLooked = -1;
        }

        //Move Right
        else if (HDirection > 0)
        {
            rb.velocity = new Vector2(HDirection*speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            lastLooked = 1;
        } 
        
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.localScale = new Vector2(lastLooked, 1);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
            if (hit.collider != null)
                Jump();
        }
    }

    private void AnimationState()
    { 
        if(state == State.climb)
        {
            
        }
        //Jumping animation change
        else if(state == State.jumping)
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
        
        else if(coll.IsTouchingLayers(ground) && Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving;
            state = State.running;
        }

        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
            else
            {
                state = State.falling;
            }
        }

    }

    private void Footstep()
    {
        footstep.Play();
    }

    private void HandleHealth()
    {
        PermanentUIController.perm.health -= 1;
        PermanentUIController.perm.healthAmount.text = PermanentUIController.perm.health.ToString();
        

        if (PermanentUIController.perm.health <= 0)
        {
            PermanentUIController.perm.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = naturalGravity;
            anim.speed = 1f;
            Jump();
            return;
        }

        float vDirection = Input.GetAxis("Vertical");

        //Climbing up
        if(vDirection > .1f && !topLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climbSpeed);
            anim.speed = 1f;
        }

        //Climbing down
        else if(vDirection < -.1f && !bottomLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climbSpeed);
            anim.speed = 1f;
        }

        //Still
        else
        {
            rb.velocity = Vector2.zero;
            anim.speed = 0f;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
}
