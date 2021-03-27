using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumController : EnemyController
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;


    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
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
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
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
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
