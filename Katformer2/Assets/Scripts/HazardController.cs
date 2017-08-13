using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    public int damageToGive;

    private LevelController myLevelController;

	// Use this for initialization
	void Start () {
        myLevelController = FindObjectOfType<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    // OnTriggerEnter2D is called when entering any collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if colliding with the player
        // and handle hazard damage
        if (collision.tag == "Player")
        {
            // Kill and respawn the player
            //myLevelController.Respawn();

            myLevelController.TakeDamage(damageToGive);
        }

    }
}
