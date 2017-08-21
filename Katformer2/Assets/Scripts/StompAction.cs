using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAction : MonoBehaviour {

    public GameObject deathEffect;
    public float bounceForce;

    private Rigidbody2D thePlayerRigidbody;

	// Use this for initialization
	void Start () {
        thePlayerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // Let's destroy the other object
            // when we bounce on another object
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);

            // Let's create a death effect
            // when we bounce on another object
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);

            // Bounce
            thePlayerRigidbody.velocity = new Vector3(
                thePlayerRigidbody.velocity.x,
                bounceForce,
                0f);
        }
        else if(collision.tag == "Boss")
        {
            // Bounce
            thePlayerRigidbody.velocity = new Vector3(
                thePlayerRigidbody.velocity.x,
                bounceForce,
                0f);

            // Now the boss will start taking damage
            collision.transform.parent.GetComponent<BossController>()
                .isTakingDamage = true;
        }
    }
}
