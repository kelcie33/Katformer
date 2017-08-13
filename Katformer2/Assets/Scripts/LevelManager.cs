using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float respawnDelay;
    public PlayerController myPlayer;  // one of our scripts
    public GameObject myDeathEffect;

	// Use this for initialization
	void Start () {
        myPlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called by the player controller when colliding with kill plane
    // allows player to be moved even when it is disabled for respawn
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    // Runs in a separate timeline as the normal flow of things
    // and runs longer than a single frame
    public IEnumerator RespawnCoroutine()
    {
        myPlayer.gameObject.SetActive(false);
        Instantiate(myDeathEffect, myPlayer.transform.position, 
            myPlayer.transform.rotation);
        yield return new WaitForSeconds(respawnDelay);

        myPlayer.transform.position = myPlayer.respawnPosition;
        myPlayer.gameObject.SetActive(true);
    }
}
