using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorObject : MonoBehaviour {

    public string levelName;
    public bool isUnlocked;
    public Sprite upperSpriteOpen;
    public Sprite upperSpriteClosed;
    public SpriteRenderer upperSpriteRenderer;
    public Sprite lowerSpriteOpen;
    public Sprite lowerSpriteClosed;
    public SpriteRenderer lowerSpriteRenderer;

    // Use this for initialization
    void Start () {
        if(levelName == "Level1")
        {
            PlayerPrefs.SetInt(levelName, 1);
        }

        if(PlayerPrefs.HasKey(levelName))
        {
            if (PlayerPrefs.GetInt(levelName) == 1)
            {
                isUnlocked = true;
            }
        }

        if (isUnlocked)
        {
            upperSpriteRenderer.sprite = upperSpriteOpen;
            lowerSpriteRenderer.sprite = lowerSpriteOpen;
        }
        else
        {
            upperSpriteRenderer.sprite = upperSpriteClosed;
            lowerSpriteRenderer.sprite = lowerSpriteClosed;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(Input.GetButtonDown("Jump") && isUnlocked)
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
