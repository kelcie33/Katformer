using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour {

    public GameObject movingObject;
    public Transform startLocation;
    public Transform endLocation;
    public float speed;

    private Vector3 target;

	// Use this for initialization
	void Start () {
        target = endLocation.position;
	}
	
	// Update is called once per frame
	void Update () {

        // Update moving object to move towards target
        // gradually with "MoveTowards" function
        movingObject.transform.position = Vector3.MoveTowards(
            movingObject.transform.position,
            target,
            speed * Time.deltaTime);

        // Update target position to start position
        // when we reach the end position
        if (movingObject.transform.position == endLocation.position)
        {
            target = startLocation.position;
        }
        // Update target position to end position
        // when we reach the start position
        else if (movingObject.transform.position == startLocation.position)
        {
            target = endLocation.position;
        }
    }
}
