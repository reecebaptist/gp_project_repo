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
    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthAmount;
    

    private enum State { idle, running, jumping, falling, hurt };
    private State state = State.idle;
    private Collider2D coll;
    private float HDirection = 0f;
    private int lastLooked = 1;
    

    // Start is called only at the beginning
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
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
            cherry.Play();
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
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

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x-2f, jumpForce);
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

    private void Footstep()
    {
        footstep.Play();
    }

    private void HandleHealth()
    {
        health -= 1;
        healthAmount.text = health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
