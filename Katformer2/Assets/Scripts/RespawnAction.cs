using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAction : MonoBehaviour {

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;

        if(GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetObject()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        if (myRigidbody != null)
        {
            myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        }

    }
}
