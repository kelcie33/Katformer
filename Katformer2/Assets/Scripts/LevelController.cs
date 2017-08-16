using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Text, Image

public class LevelController : MonoBehaviour {

    public float respawnDelay;
    public PlayerController myPlayer;  // one of our scripts
    public GameObject myDeathEffect;
    public int coinCount;
    public Text coinText;
    public Image myHeart1;
    public Image myHeart2;
    public Image myHeart3;
    public Sprite mySpriteFull;
    public Sprite mySpriteHalf;
    public Sprite mySpriteEmpty;
    public int maxHealth;
    public int currentHealth;
    public bool isInvincible;
    public int currentLives;
    public int startingLives;
    public Text myLivesText;
    public GameObject myGameOverScreen;
    public int bonusLifeThreshold;

    private bool isRespawning;
    private RespawnAction[] myRespawnActions;
    private int coinBonusLifeCount;

    // Use this for initialization
    void Start () {
        // Search for objects in the scene
        myPlayer = FindObjectOfType<PlayerController>();

        // Set the player info at the start
        currentHealth = maxHealth;
        isRespawning = false;

        // Set the UI text at the start
        coinText.text = "Coins: " + coinCount;

        // Set the list of respawn actions
        // to all such actions in the game scene
        myRespawnActions = FindObjectsOfType<RespawnAction>();

        // Set the current lives to starting lives
        currentLives = startingLives;
        myLivesText.text = "Lives x" + currentLives;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
        {
            Respawn();
        }

        if(coinBonusLifeCount >= bonusLifeThreshold)
        {
            currentLives += 1;
            myLivesText.text = "Lives x" + currentLives;
            coinBonusLifeCount -= bonusLifeThreshold;
        }
	}

    // Called by the player controller when colliding with kill plane
    // allows player to be moved even when it is disabled for respawn
    public void Respawn()
    {
        // Check if we are already respawning and exit the function early
        // when we are already respawning or have no more lives
        if(isRespawning || currentLives == 0)
        {
            return;
        }

        currentLives -= 1;
        myLivesText.text = "Lives x" + currentLives;

        if(currentLives > 0)
        {
            isRespawning = true;
            StartCoroutine("RespawnCoroutine");
        }
        else if(currentLives == 0)
        {
            myPlayer.gameObject.SetActive(false);
            Instantiate(myDeathEffect, myPlayer.transform.position,
                myPlayer.transform.rotation);
            myGameOverScreen.SetActive(true);
        }
    }

    // Runs in a separate timeline as the normal flow of things
    // and runs longer than a single frame
    public IEnumerator RespawnCoroutine()
    {
        myPlayer.gameObject.SetActive(false);
        Instantiate(myDeathEffect, myPlayer.transform.position, 
            myPlayer.transform.rotation);
        yield return new WaitForSeconds(respawnDelay);

        currentHealth = maxHealth;
        UpdateHeartMeter();
        coinCount = 0;
        coinBonusLifeCount = 0;
        coinText.text = "Coins: " + coinCount;
        isRespawning = false;

        myPlayer.transform.position = myPlayer.respawnPosition;
        myPlayer.gameObject.SetActive(true);
        for(int i = 0; i < myRespawnActions.Length; i++)
        {
            myRespawnActions[i].gameObject.SetActive(true);
            myRespawnActions[i].ResetObject();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
    }

    // Called by the player controller when colliding with hazard
    // allows health to be updated which is managed only in this file
    public void TakeDamage(int damageToTake)
    {
        if(!isInvincible)
        {
            currentHealth = Mathf.Max(currentHealth - damageToTake, 0);
            UpdateHeartMeter();
            myPlayer.Knockback();
        }
    }

    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        currentHealth = Mathf.Min(currentHealth, 6);
        UpdateHeartMeter();
    }

    public void GiveLives(int livesToGive)
    {
        currentLives += livesToGive;
        myLivesText.text = "Lives x" + currentLives;
    }

    public void UpdateHeartMeter()
    {
        switch(currentHealth)
        {
            case 6:
                myHeart1.sprite = mySpriteFull;
                myHeart2.sprite = mySpriteFull;
                myHeart3.sprite = mySpriteFull;
                break;

            case 5:
                myHeart1.sprite = mySpriteFull;
                myHeart2.sprite = mySpriteFull;
                myHeart3.sprite = mySpriteHalf;
                break;

            case 4:
                myHeart1.sprite = mySpriteFull;
                myHeart2.sprite = mySpriteFull;
                myHeart3.sprite = mySpriteEmpty;
                break;

            case 3:
                myHeart1.sprite = mySpriteFull;
                myHeart2.sprite = mySpriteHalf;
                myHeart3.sprite = mySpriteEmpty;
                break;

            case 2:
                myHeart1.sprite = mySpriteFull;
                myHeart2.sprite = mySpriteEmpty;
                myHeart3.sprite = mySpriteEmpty;
                break;

            case 1:
                myHeart1.sprite = mySpriteHalf;
                myHeart2.sprite = mySpriteEmpty;
                myHeart3.sprite = mySpriteEmpty;
                break;

            case 0:
                myHeart1.sprite = mySpriteEmpty;
                myHeart2.sprite = mySpriteEmpty;
                myHeart3.sprite = mySpriteEmpty;
                break;

            default:
                myHeart1.sprite = mySpriteEmpty;
                myHeart2.sprite = mySpriteEmpty;
                myHeart3.sprite = mySpriteEmpty;
                break;
        }
    }
}
