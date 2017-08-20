using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour {

    public Sprite flagClosedSprite;
    public Sprite flagOpenSprite;
    public bool isCheckpointActive;

    private SpriteRenderer mySpriteRenderer;

    // Use this for initialization
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // OnTriggerEnter2D is called when entering any collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            mySpriteRenderer.sprite= flagOpenSprite;
            isCheckpointActive = true;
        }
    }
}
