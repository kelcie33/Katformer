using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    public int healthToGive;

    private LevelController myLevelController;

	// Use this for initialization
	void Start () {
        myLevelController = FindObjectOfType<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            myLevelController.GiveHealth(healthToGive);
            gameObject.SetActive(false);
        }
    }

}
