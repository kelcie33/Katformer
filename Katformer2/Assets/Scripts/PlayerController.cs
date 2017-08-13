using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float jumpSpeed;
    public Transform groundCheckTransform;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public Vector3 respawnPosition;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private LevelController myLevelController;
    private bool isAtGround;

	// Use this for initialization
	void Start ()
    {
        // Store reference to the LevelController object
        // created elsewhere in the current scene
        myLevelController = FindObjectOfType<LevelController>();

        // Store reference to the Rigidbody2D component
        // on the current GameObject (Player).
        myRigidbody = GetComponent<Rigidbody2D>();

        // Store reference to the Animator component
        // on the current GameObject (Player).
        myAnimator = GetComponent<Animator>();

        // Update the respawn position
        // so it is initially the same as first player position
        respawnPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        CheckAtGround();
        CheckHorizontalInput();
        CheckJumpInput();
        UpdateAnimator();
    }

    // OnTriggerEnter2D is called when entering any collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if colliding with the kill plane
        // and handle player respawn
        if(collision.tag == "KillPlane")
        {
            myLevelController.Respawn();
        }

        // Check if colliding with the checkpoint
        // and update next respawn position
        if(collision.tag == "Checkpoint")
        {
            respawnPosition = collision.transform.position;
        }
    }

    // OnCollisionEnter2D is called when entering any collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    // OnCollisionEnter2D is called when entering any collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    void CheckAtGround()
    {
        // Check whether the ground check transform
        // is overlapping any object on a certain layer
        isAtGround = Physics2D.OverlapCircle(
            groundCheckTransform.position,
            groundCheckRadius,
            groundCheckLayer);
    }

    void CheckHorizontalInput()
    {
        // Check whether the game input horizontal axis
        // is moving to the right.
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidbody.velocity = new Vector3(
                moveSpeed,
                myRigidbody.velocity.y,
                0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // Check whether the game input horizontal axis
        // is moving to the left.
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidbody.velocity = new Vector3(
                -moveSpeed,
                myRigidbody.velocity.y,
                0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // Check whether the game input horizontal axis
        // is not moving.
        else
        {
            myRigidbody.velocity = new Vector3(
                0f,
                myRigidbody.velocity.y,
                0f);
        }
    }
        
    void CheckJumpInput()
    {
        // Check whether the button is pressed down for jump
        if (Input.GetButtonDown("Jump") && isAtGround)
        {
            myRigidbody.velocity = new Vector3(
                myRigidbody.velocity.x,
                jumpSpeed,
                0f);
        }
    }

    void UpdateAnimator()
    {
        // Update animator values used for transitions
        // using values calculated in this file
        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnimator.SetBool("Grounded", isAtGround);
    }
}
