using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public bool isBattleActive;
    public float startingTimeBetweenDropsSec;
    public float timeBetweenDropsSec;
    public float timeBeforePlatformsSec;
    public Transform mySpawnLeftPoint;
    public Transform mySpawnRightPoint;
    public Transform mySpawnMovingPoint;
    public GameObject fallingSpinsawPrefab;
    public GameObject myBoss;
    public bool isBossRight;
    public GameObject myRightPlatformGroup;
    public GameObject myLeftPlatformGroup;
    public bool isTakingDamage;
    public int startingHealth;
    public GameObject myLevelExitHolder;
    public bool isWaitingForRespawnCoroutineToEnd;

    private float counterBetweenDrops;
    private float counterBeforePlatforms;
    private int currentHealth;
    private CameraController theCameraController;
    private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
        // Set counters to maximum values
        // so they can count down to zero
        counterBetweenDrops = timeBetweenDropsSec;
        counterBeforePlatforms = timeBeforePlatformsSec;

        timeBetweenDropsSec = startingTimeBetweenDropsSec;

        // Set boss position to match spawn right point
        // so it has a good place to start at first
        myBoss.transform.position = mySpawnRightPoint.position;
        isBossRight = true;

        currentHealth = startingHealth;

        theCameraController = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(theLevelManager.isRespawnCoroutineActive)
        {
            isBattleActive = false;
            isWaitingForRespawnCoroutineToEnd = true;
        }

        if(isWaitingForRespawnCoroutineToEnd
            && !theLevelManager.isRespawnCoroutineActive)
        {
            isWaitingForRespawnCoroutineToEnd = false;

            myBoss.SetActive(false);
            myLeftPlatformGroup.SetActive(false);
            myRightPlatformGroup.SetActive(false);
            counterBeforePlatforms = timeBeforePlatformsSec;
            counterBetweenDrops = timeBetweenDropsSec;
            myBoss.transform.position = mySpawnRightPoint.position;
            isBossRight = true;
            currentHealth = startingHealth;
            theCameraController.isFollowingTarget = true;
            timeBetweenDropsSec = startingTimeBetweenDropsSec;
        }

		if(isBattleActive)
        {
            myBoss.SetActive(true);

            // Change the game camera to focus on battle area
            // instead of following the player
            theCameraController.isFollowingTarget = false;
            theCameraController.transform.position = Vector3.Lerp(
                theCameraController.transform.position,
                new Vector3(
                    transform.position.x,
                    theCameraController.transform.position.y,
                    theCameraController.transform.position.z),
                theCameraController.cameraShiftSpeed * Time.deltaTime);

            // Decrease counter by the amount of time that passed
            // but don't go below zero
            counterBetweenDrops = Mathf.Max(counterBetweenDrops - Time.deltaTime, 0);

            // When the counter has finished counting
            // create new spinsaw at the moving point location
            if(counterBetweenDrops == 0)
            {
                mySpawnMovingPoint.position = new Vector3(
                    Random.Range(mySpawnLeftPoint.position.x, mySpawnRightPoint.position.x),
                    mySpawnRightPoint.position.y,
                    0f);
                Instantiate(fallingSpinsawPrefab, mySpawnMovingPoint.position, mySpawnMovingPoint.rotation);
                counterBetweenDrops = timeBetweenDropsSec;
            }

            // Decrease counter by the amount of time that passed
            // but don't go below zero
            counterBeforePlatforms = Mathf.Max(counterBeforePlatforms - Time.deltaTime, 0);

            // When the counter has finished counting
            // activate the right platforms
            if (counterBeforePlatforms == 0)
            {
                if (isBossRight)
                {
                    myRightPlatformGroup.SetActive(true);
                }
                else
                {
                    myLeftPlatformGroup.SetActive(true);
                }
            }

            // When boss is taking damage from the player
            // then decrease boss health by one
            if(isTakingDamage)
            {
                currentHealth -= 1;
                isTakingDamage = false;

                if(currentHealth == 0)
                {
                    theCameraController.isFollowingTarget = true;
                    myLevelExitHolder.SetActive(true);
                    gameObject.SetActive(false);
                }

                if(isBossRight)
                {
                    myBoss.transform.position
                        = mySpawnLeftPoint.position;
                }
                else
                {
                    myBoss.transform.position
                        = mySpawnRightPoint.position;
                }

                // Toggle boolean
                isBossRight = !isBossRight;
                
                myLeftPlatformGroup.SetActive(false);
                myRightPlatformGroup.SetActive(false);
                counterBeforePlatforms = timeBeforePlatformsSec;

                // Speed up falling spinsaw drops
                timeBetweenDropsSec /= 2f;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isBattleActive = true;
        }
    }
}
