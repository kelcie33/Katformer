using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;

    private bool isVisible;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isVisible)
        {
            myRigidbody.velocity = new Vector3(
                -moveSpeed,
                myRigidbody.velocity.y,
                0f);
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "KillPlane")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        isVisible = false;
    }
}
