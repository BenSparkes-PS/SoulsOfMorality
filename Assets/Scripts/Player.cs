using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField]
    private bool flipped = false, flip = false, isGrounded = true;

    void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    }

    void Update()
    {
        if (GameManager.Instance.bPlaying)
        {
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Jump(flipped);
            }
            //temp grav changer for testing. Will inverse gravity on player
            if (Input.GetKeyDown(KeyCode.Space))
            {
                flip = true;
            }

            if (flip)
            {
                GravityChange(flipped);
            }
        }
    }

    void FixedUpdate() //Physics update
    {
        if (GameManager.Instance.bPlaying)            //if round is currently playing
        {
            if (flipped)
            {
                playerRb.AddForce(Vector2.up * LevelManager.Instance.PlayerGravity);           //Adds the gravity down (Will need to be changed dependant on gravity flip)
            }
            else
            {
                playerRb.AddForce(Vector2.down * LevelManager.Instance.PlayerGravity);           //Adds the gravity down (Will need to be changed dependant on gravity flip)
            }
            //playerRb.velocity = new Vector2(LevelManager.Instance.PlayerSpeed, playerRb.velocity.y);
            transform.Translate(Vector2.right * Time.deltaTime * LevelManager.Instance.PlayerSpeed); //Quick simple work, maybe should be changed to rigidbody velocity/force but dont see any current issues.

        }
    }

    //grounded check
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        //Death state code needs to be added
        if (collision.gameObject.tag == "Obstacle")
        {
            DeathManager.deathManager.Died();
        }

    }

    //Applies a jump force depending on if the player is flipped or not
    void Jump(bool isFlipped)
    {
        if (!isFlipped)
            playerRb.AddForce(Vector2.up * LevelManager.Instance.PlayerJumpHeight);
        else
            playerRb.AddForce(Vector2.down * LevelManager.Instance.PlayerJumpHeight);
        isGrounded = false;
    }

    //Changes the players gravity when they are flipped
    void GravityChange(bool isFlipped)
    {
        if (!flipped)
        {
            flipped = true;
        }
        else
        {
            flipped = false;
        }
        flip = false;
    }
}
