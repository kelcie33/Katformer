using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigglerController : MonoBehaviour {

    public Transform startLocation;
    public Transform endLocation;
    public float speed;

    private Rigidbody2D myRigidbody;
    public bool isMovingRight;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMovingRight && transform.position.x >= endLocation.position.x)
        {
            isMovingRight = false;
        }
        if(!isMovingRight && transform.position.x <= startLocation.position.x)
        {
            isMovingRight = true;
        }

        // Movement is due to rigidbody force
        if (isMovingRight)
        {
            myRigidbody.velocity = new Vector3(
                speed,
                myRigidbody.velocity.y,
                0f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(
                -speed,
                myRigidbody.velocity.y,
                0f);
        }
    }
}
