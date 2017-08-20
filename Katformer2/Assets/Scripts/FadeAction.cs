using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAction : MonoBehaviour {

    public float fadeDurationSec;

    private Image myLevelTransitionScreen;

	// Use this for initialization
	void Start () {
        myLevelTransitionScreen = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        myLevelTransitionScreen.CrossFadeAlpha(0f, fadeDurationSec, false);

        if(myLevelTransitionScreen.color.a == 0)
        {
            gameObject.SetActive(false);
        }
	}
}
