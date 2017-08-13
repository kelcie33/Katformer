using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour {

    public int coinValue;

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
            myLevelController.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
