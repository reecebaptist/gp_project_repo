using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
        //Transistion from Jump to Fall
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }

        //Transition from Fall to Idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
        
    }

    private void Move()
    {
        if (facingLeft)
        {
            //Make sure the sprite is facing the correct direction if not then face the correct direction.
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //Test to see if we are beyond the left cap.
            if (transform.position.x > leftCap)
            {
                //Test to see if he is on the ground.
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            //Make sure the sprite is facing the correct direction if not then face the correct direction.
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            //Test to see if we are beyond the left cap.
            if (transform.position.x < rightCap)
            {
                //Test to see if he is on the ground.
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

}
