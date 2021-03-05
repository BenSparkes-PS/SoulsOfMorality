using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField]
    private bool flipped = false, isGrounded = true;
    private GameObject LastCollider;
    void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Camera.main.GetComponent<CameraFollow>().target = transform;
    }

    void Update()
    {
        if (GameManager.Instance.bPlaying)
        {
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Jump(flipped);
            }
        }
    }

    void FixedUpdate() //Physics update
    {

        if (flipped)
        {
            playerRb.AddForce(Vector2.up * LevelManager.Instance.PlayerGravity);
        }
        else
        {
            playerRb.AddForce(Vector2.down * LevelManager.Instance.PlayerGravity);
        }
        if (GameManager.Instance.bPlaying)
        {
            transform.Translate(Vector2.right * Time.deltaTime * LevelManager.Instance.PlayerSpeed);
        }
    }

    //grounded check
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        //Death state code needs to be added
        if (GameManager.Instance.bPlaying)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                DeathManager.deathManager.Died();
            }
        }

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Collectable")
        {
            PickupManager.pickupManager.PickupCollision(collider.gameObject);
        }
        if (collider.gameObject.tag == "Finish")
        {
            LevelManager.Instance.LevelComplete();
        }
        if (collider.gameObject.tag == "Portal")
        {
            if (LastCollider == null || (collider.gameObject.transform.position != LastCollider.transform.position))
            {
                isGrounded = false;
                GravityChange(flipped);
                LastCollider = collider.gameObject;
            }
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
        AudioManager.Instance.PlayPortal();
        LevelManager.Instance.isFlipped = !LevelManager.Instance.isFlipped;
        ColourFlipper.Instance.FlipColour(LevelManager.Instance.isFlipped);
        flipped = !flipped;

    }
}
