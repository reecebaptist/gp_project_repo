# gp_project_repo

Following this video series:
https://www.youtube.com/watch?v=JQvicrPb3ok&list=PLpj8TZGNIBNy51EtRuyix-NYGmcfkNAuH&index=6

Import Assets
1.	Add the sunny land asset from the asset store
2.	Download and import

Converting everything to 16-bit
1.	Select all tilesets and background images and make the ‘Pixels per unit’ 16.
2.	Apply the changes
Creating Tilesets
1.	Open an unsliced image.
2.	Open the Sprite Editor.
3.	Side option:
4.	Grid by Cell size
5.	16 x 16
6.	Slice
7.	Apply
8.	Aplly
9.	Create new Palette
10.	Drag and drop tileset to palette
11.	Save it and use it

Character Sprite
1.	Go to the character sprite
2.	Do converting to 16-bit
3.	Create a new GameObject called ‘Player’
4.	Add ‘Sprite Renderer’ component to it
5.	Add the idle to Sprite option
6.	Go to sorting layer
7.	Click on Add Sorting Layer
8.	Make ‘Base’, ‘Fore’, ‘Entity’
9.	Make changes to background by choosing ‘Back’
10.	Make changes to ground by choosing ‘fore’.
11.	Make changes to player by choosing ‘Entity’.

Adding colliders
1.	Added RigidBody 2D to ‘Player’
2.	Added BoxCollider 2D to ‘Player’
3.	Added TileMapCollider 2D to ‘Foreground’
4.	Added RigidBody 2D to ‘Foreground’
5.	Added CompositeCollider 2D to ‘Foreground’
6.	Checked the ‘Used By Composite’ box in Foreground’s RigidBody 2D

Finishing Movement
1.	Add ‘PlayerController’ script to Player.
2.	Add the Script:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        //Move Left
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
        
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
    }
}

3.	Check the Freeze Z in Constraints in RigidBody of the Player Object
4.	Adjust the mass to 50 and linear drag to 1. (Or accordingly)



Jumping
1. Modify the code of Player Controller.
2. Add the following:
	//Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        }
3. Problem: Infinite jumps work here.


Flipping Animations
1. Modify the code and the following:

	//Move Left
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

2. Here this code affects the camera.
3. Remove Camera from under the Player to a Separate Game Object
4. Add 'Camera Controller' Script to it.
5. Type the following code over there.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}



Animations
1. Go to idle folder
2. Right Click > Create > Animation > name it 'idle'
3. Drag it on to Player
4. Open Animator Window from Window Drop Down List
5. Open Animation Window.
6. Click on Player.
7. Drag and drop all the animations 1,2,3,4 from the folder to the timeline and adjust
8. After adjusting play and see the result.
9. Do 2-8 steps for run animation.
10. No separate player thingie comes.
11. Go to Animator window
12. Click on Make Transisition with 'run' from idle
13. Remove the Has Exit time. Open settings uncheck and set everything to 0.
14. Make a transiotion from run to idle.
15. Do the same thing.
16. GO to parameters, create a bool called 'running' (Spelling and CAse importnant.
17. go to conditions for each of the transisitons and make running false and true accordingly for both the transisiotns.
18. Go to code make changes as follows:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    // Start is called only at the beginning
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float HDirection = Input.GetAxis("Horizontal");
        //Move Left
        if(HDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        }
        
        //Move Right
        else if (HDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        }

        else
        {
            anim.SetBool("running", false);
        }

        //Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        }
    }
}
19. Code is also cleaned in the previous step.

Finite State Machine
1. Create enumarators state {idle, running, jumping}
2. Type the following:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private enum State { idle, running, jumping};
    private State state = State.idle;

    // Start is called only at the beginning
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {


        float HDirection = Input.GetAxis("Horizontal");
        //Move Left
        if(HDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            
        }
        
        //Move Right
        else if (HDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            
        }

        else
        {
            
        }

        //Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5f);
            state = State.jumping;
        }

        velocityState();
        anim.SetInteger("state", (int)state);
    }

    private void velocityState()
    { 
        
        if(state == State.jumping)
        {

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

3. Go to the parameters and delete running and add an Integer type 'state'.
4. State = 1 for idle->run.
5. State = 0 for run->idle.

