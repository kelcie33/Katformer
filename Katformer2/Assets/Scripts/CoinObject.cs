using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour {

    public int coinValue;

    private LevelManager myLevelManager;

	// Use this for initialization
	void Start () {
        myLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            myLevelManager.AddCoins(coinValue);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
