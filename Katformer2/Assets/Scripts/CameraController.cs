using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject cameraTarget;
    public float cameraFollowDist;
    public float cameraShiftSpeed;
    public bool isFollowingTarget;
    public SpriteRenderer topBackgroundSpriteRenderer;
    public SpriteRenderer midBackgroundSpriteRenderer;
    public SpriteRenderer btmBackgroundSpriteRenderer;
    public Sprite topBackgroundSpriteA;
    public Sprite midBackgroundSpriteA;
    public Sprite btmBackgroundSpriteA;
    public Sprite topBackgroundSpriteB;
    public Sprite midBackgroundSpriteB;
    public Sprite btmBackgroundSpriteB;
    public Sprite topBackgroundSpriteC;
    public Sprite midBackgroundSpriteC;
    public Sprite btmBackgroundSpriteC;

    private Vector3 cameraTargetPosition;

	// Use this for initialization
	void Start () {
        isFollowingTarget = true;

        switch(SceneManager.GetActiveScene().name)
        {
            case "Level1":
                Color blueBackground = new Color(64 / 256.0f, 96 / 256.0f, 128 / 256.0f);
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteA;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteA;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteA;
                topBackgroundSpriteRenderer.color = blueBackground;
                midBackgroundSpriteRenderer.color = blueBackground;
                btmBackgroundSpriteRenderer.color = blueBackground;
                break;

            case "Level2":
                Color greenBackground = new Color(64 / 256.0f, 128 / 256.0f, 96 / 256.0f);
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteA;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteA;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteA;
                topBackgroundSpriteRenderer.color = greenBackground;
                midBackgroundSpriteRenderer.color = greenBackground;
                btmBackgroundSpriteRenderer.color = greenBackground;
                break;

            case "Level3":
                Color redBackground = new Color(192 / 256.0f, 64 / 256.0f, 64 / 256.0f);
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteA;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteA;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteA;
                topBackgroundSpriteRenderer.color = redBackground;
                midBackgroundSpriteRenderer.color = redBackground;
                btmBackgroundSpriteRenderer.color = redBackground;
                break;

            case "Level4":
                Color purpleBackground = new Color(160 / 256.0f, 64 / 256.0f, 160 / 256.0f);
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteA;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteA;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteA;
                topBackgroundSpriteRenderer.color = purpleBackground;
                midBackgroundSpriteRenderer.color = purpleBackground;
                btmBackgroundSpriteRenderer.color = purpleBackground;
                break;

            case "Level5":
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteB;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteB;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteB;
                break;

            case "LevelSelect":
                topBackgroundSpriteRenderer.sprite = topBackgroundSpriteC;
                midBackgroundSpriteRenderer.sprite = midBackgroundSpriteC;
                btmBackgroundSpriteRenderer.sprite = btmBackgroundSpriteC;
                topBackgroundSpriteRenderer.color = new Color(255, 255, 255);
                midBackgroundSpriteRenderer.color = new Color(255, 255, 255);
                btmBackgroundSpriteRenderer.color = new Color(255, 255, 255);
                break;

            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateToFollowTarget();
	}

    void UpdateToFollowTarget()
    {
        if(isFollowingTarget)
        {
            // Update x value used for new position vector
            // using position for the camera target
            cameraTargetPosition = new Vector3(
                cameraTarget.transform.position.x,
                transform.position.y,
                transform.position.z);

            // Update x value for new position vector
            // shifting the camera follow distance in the camera target's direction
            if (cameraTarget.transform.localScale.x > 0f)
            {
                cameraTargetPosition = new Vector3(
                    cameraTargetPosition.x + cameraFollowDist,
                    transform.position.y,
                    transform.position.z);
            }
            else
            {
                cameraTargetPosition = new Vector3(
                    cameraTargetPosition.x - cameraFollowDist,
                    transform.position.y,
                    transform.position.z);
            }

            // Update camera position vector to be the new position vector
            // using calculated position values above (NOT SMOOTH)
            //transform.position = cameraTargetPosition;

            // Update camera position vector to be the new position vector
            // using calculated position values with linear interpolation
            // so the visual transition looks more smooth
            transform.position = Vector3.Lerp(
                transform.position,
                cameraTargetPosition,
                cameraShiftSpeed * Time.deltaTime);

        }
    }
}
