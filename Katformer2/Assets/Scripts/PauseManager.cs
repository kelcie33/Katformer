using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public string levelSelectName;
    public string mainMenuName;
    public GameObject pauseScreenImage;

    private LevelManager myLevelManager;
    private PlayerController myPlayerController;

	// Use this for initialization
	void Start () {
        myLevelManager = FindObjectOfType<LevelManager>();
        myPlayerController = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            // Check whether the flow of time in the game
            // is currently stopped or not
            if (Time.timeScale == 0)
            {
                ResumeOption();
            }
            else
            {
                PauseGame();
            }
        }
	}

    public void PauseGame()
    {
        // Let's stop the flow of time in the game
        // while the pause screen is active
        Time.timeScale = 0;
        pauseScreenImage.SetActive(true);
        myPlayerController.isMovable = false;
        myLevelManager.myLevelMusic.Pause();
    }

    public void ResumeOption()
    {
        // Let's resume the flow of time in the game
        // now that the game is resumed
        Time.timeScale = 1f;
        pauseScreenImage.SetActive(false);
        myPlayerController.isMovable = true;
        myLevelManager.myLevelMusic.Play();
    }

    public void LevelSelectOption()
    {
        // Store the Level Manager data
        // to the player preferences so it saved
        PlayerPrefs.SetInt("coinCount", myLevelManager.currentLives);
        PlayerPrefs.SetInt("currentLives", myLevelManager.coinCount);

        // Navigate to the next scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelectName);
    }

    public void ReturnToMainMenuOption()
    {
        // Navigate to the next scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuName);
    }
}
