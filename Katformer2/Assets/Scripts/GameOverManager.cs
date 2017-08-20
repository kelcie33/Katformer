using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public string myLevelSelectName;
    public string myMainMenuName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartOption()
    {
        PlayerPrefs.DeleteKey("coinCount");
        PlayerPrefs.DeleteKey("currentLives");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectOption()
    {
        PlayerPrefs.DeleteKey("coinCount");
        PlayerPrefs.DeleteKey("currentLives");
        SceneManager.LoadScene(myLevelSelectName);
    }

    public void ReturnToMainMenuOption()
    {
        SceneManager.LoadScene(myMainMenuName);
    }
}
