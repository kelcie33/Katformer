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

    private bool isRespawning;

    // Use this for initialization
    void Start () {
        // Search for objects in the scene
        myPlayer = FindObjectOfType<PlayerController>();

        // Set the player info at the start
        currentHealth = maxHealth;
        isRespawning = false;

        // Set the UI text at the start
        coinText.text = "Coins: " + coinCount;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth == 0 && !isRespawning)
        {
            Respawn();
        }
	}

    // Called by the player controller when colliding with kill plane
    // allows player to be moved even when it is disabled for respawn
    public void Respawn()
    {
        isRespawning = true;
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

        currentHealth = maxHealth;
        UpdateHeartMeter();
        isRespawning = false;

        myPlayer.transform.position = myPlayer.respawnPosition;
        myPlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
    }

    // Called by the player controller when colliding with hazard
    // allows health to be updated which is managed only in this file
    public void TakeDamage(int damageToTake)
    {
        currentHealth = Mathf.Max(currentHealth - damageToTake, 0);
        UpdateHeartMeter();
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
