using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string firstLevelName;
    public string selectedLevelName;
    public string[] levelNames;

    private bool firstTime;

	// Use this for initialization
	void Start () {
        firstTime = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameOption()
    {
        SceneManager.LoadScene(firstLevelName);

        // Set Player Preferences for level unlocked
        // to be false for all levels
        for (int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i], 0);
        }

        // Set Player Preferences for other information
        // about the game player
        PlayerPrefs.DeleteKey("coinCount");
        PlayerPrefs.DeleteKey("currentLives");

        firstTime = false;
    }

    public void ContinueOption()
    {
        SceneManager.LoadScene(selectedLevelName);

        // Set Player Preferences for other information
        // about the game player
        if(firstTime)
        {
            PlayerPrefs.DeleteKey("coinCount");
            PlayerPrefs.DeleteKey("currentLives");
        }

        firstTime = false;
    }

    public void ExitOption()
    {
        Application.Quit();
    }
}
