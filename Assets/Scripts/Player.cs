using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D PlayerRigid;

    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();          //Sets playerrigid to the rigidbody2d component on the player.
    }

    void FixedUpdate() //Physics update
    {
        if (GameManager.Instance.bPlaying)            //if round is currently playing
        {
            PlayerRigid.AddForce(Vector2.down * LevelManager.Instance.PlayerGravity);           //Adds the gravity down (Will need to be changed dependant on gravity flip)
            transform.Translate(Vector2.right * Time.deltaTime * LevelManager.Instance.PlayerSpeed); //Quick simple work, maybe should be changed to rigidbody velocity/force but dont see any current issues.
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.bPlaying)            //Jump condition (Set to touch input > 0) && check to make sure ISGROUNDED. Also checks if game is in playing state (round playing)
        {
            print("Jump");
            PlayerRigid.AddForce(Vector2.up * LevelManager.Instance.PlayerJumpHeight);
        }
    }


}
