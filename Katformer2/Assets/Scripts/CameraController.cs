using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject cameraTarget;
    public float cameraFollowDist;
    public float cameraShiftSpeed;

    private Vector3 cameraTargetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateToFollowTarget();
	}

    void UpdateToFollowTarget()
    {
        // Update x value used for new position vector
        // using position for the camera target
        cameraTargetPosition = new Vector3(
            cameraTarget.transform.position.x,
            transform.position.y,
            transform.position.z);

        // Update x value for new position vector
        // shifting the camera follow distance in the camera target's direction
        if (cameraTarget.transform.localScale.x > 0f)
        {
            cameraTargetPosition = new Vector3(
                cameraTargetPosition.x + cameraFollowDist,
                transform.position.y,
                transform.position.z);
        }
        else
        {
            cameraTargetPosition = new Vector3(
                cameraTargetPosition.x - cameraFollowDist,
                transform.position.y,
                transform.position.z);
        }

        // Update camera position vector to be the new position vector
        // using calculated position values above (NOT SMOOTH)
        //transform.position = cameraTargetPosition;

        // Update camera position vector to be the new position vector
        // using calculated position values with linear interpolation
        // so the visual transition looks more smooth
        transform.position = Vector3.Lerp(
            transform.position,
            cameraTargetPosition,
            cameraShiftSpeed * Time.deltaTime);
    }
}
