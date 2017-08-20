using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndpointObject : MonoBehaviour {

    public string nextLevelName;
    public float endpointMoveDelay;
    public float endpointLoadDelay;
    public Sprite flagClosedSprite;
    public Sprite flagOpenSprite;

    private PlayerController myPlayer;
    private CameraController myCamera;
    private LevelManager myLevelManager;
    private bool isMovingPlayer;
    private SpriteRenderer mySpriteRenderer;

    // Use this for initialization
    void Start () {
        myPlayer = FindObjectOfType<PlayerController>();
        myCamera = FindObjectOfType<CameraController>();
        myLevelManager = FindObjectOfType<LevelManager>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = flagClosedSprite;
    }
	
	// Update is called once per frame
	void Update () {
		if(isMovingPlayer)
        {
            myPlayer.myRigidbody.velocity = new Vector3(
                myPlayer.moveSpeed,
                myPlayer.myRigidbody.velocity.y,
                0f);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mySpriteRenderer.sprite = flagOpenSprite;
            StartCoroutine("EndpointCoroutine");
        }
    }

    public IEnumerator EndpointCoroutine()
    {
        myPlayer.isMovable = false;
        myCamera.isFollowingTarget = false;
        myLevelManager.isInvincible = true;
        myPlayer.myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        myLevelManager.myLevelMusic.Stop();
        myLevelManager.myGameOverMusic.Play();
        yield return new WaitForSeconds(endpointMoveDelay);

        isMovingPlayer = true;
        yield return new WaitForSeconds(endpointLoadDelay);

        SceneManager.LoadScene(nextLevelName);
    }
}
