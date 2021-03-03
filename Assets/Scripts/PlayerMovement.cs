using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField]
    private int speed=4, jumpForce=300;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private bool flipped = false, flip = false, isGrounded = true;

    void Awake() 
    {      
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;         

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isGrounded)
        {            
            Jump(flipped);
        }
        //temp grav changer for testing. Will inverse gravity on player
       /* if(Input.GetKeyDown(KeyCode.Space))
        {
            flip = true;
        }

        if(flip)
        {
            GravityChange(flipped);
        }    */    
    }

    void FixedUpdate()
    {
        //Moves the player forward at a constant speed
        playerRb.velocity = new Vector2(speed,playerRb.velocity.y);
    }

    //grounded check
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        //Death state code needs to be added
        if(collision.gameObject.tag == "obs")
        {            
            DeathManager.deathManager.Died();
        }        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {        
        if(collider.gameObject.tag == "pickup")
        {
            PickupManager.pickupManager.PickupCollision(collider.gameObject);
        }

        if(collider.gameObject.tag == "portal")
        {
            flipped =!flipped;
            GravityChange(flipped);
        }
    }

    //Applies a jump force depending on if the player is flipped or not
    void Jump(bool isFlipped)
    {
        if(!PortalManager.portalManager.flipped)
            playerRb.AddForce(new Vector2(0,(jumpForce)), ForceMode2D.Force);
        else   
            playerRb.AddForce(new Vector2(0,(-jumpForce)), ForceMode2D.Force);
        isGrounded = false;
    }

    //Changes the players gravity when they are flipped
    void GravityChange(bool isFlipped)
    {
        if(isFlipped)
        {
            playerRb.gravityScale = -1;
        }
        else
        {
            playerRb.gravityScale = 1;
        }     
    }
}
