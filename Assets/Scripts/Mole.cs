using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

public class Mole : MonoBehaviour {

    private Vector3 startingPosition;
    private Vector3 endPosition;
    public bool moveUp = false;
    public bool moveDown = false;
    // Time to take from start to finish
    private float lerpTime = 0.5f;
    private float currentLerpTime = 0f;

    public bool isOutOfHole = false;
    public bool isHitByHammer = false;
    public bool playedRightNote = false;
    public bool playedNote = false;
  
    public Image timerBar;
    public GameObject timerBarObj;
    public Text noteText;

    private Image moleImage;
    public Sprite moleHit;
    private Sprite moleIdle;

	void Start () {
        startingPosition = transform.position;
        endPosition = new Vector3(startingPosition.x, startingPosition.y+90, startingPosition.z);
        timerBarObj = timerBar.gameObject.transform.parent.gameObject;
        noteText = timerBarObj.GetComponentInChildren<Text>();
        moleImage = GetComponent<Image>();
        moleIdle = gameObject.GetComponent<Image>().sprite;
	}

    private void Update()
    { 
        if (moveUp)
        {
            Popup();
        }
        else if (moveDown)
        {
            Hide();
        }
    }

    // Move up
    public void Popup()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float speed = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startingPosition, endPosition, speed);

        if (transform.position == endPosition)
        {
            moveUp = false;
            currentLerpTime = 0;
            isOutOfHole = true;
        }
    }

    // Move down
    public void Hide()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float speed = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(endPosition, startingPosition, speed);

        if (transform.position == startingPosition)
        {
            moveDown = false;
            currentLerpTime = 0;
            isOutOfHole = false;
        }
    }

    public void ChangeMoleSprite(bool isHitByHammer)
    {
        if (isHitByHammer)
        {
            moleImage.sprite = moleHit;
            Debug.Log("hit sprite");
        }
        else
        {
            moleImage.sprite = moleIdle;
        }
        Debug.Log("mole sprite: " + moleImage.sprite);
    }

    public IEnumerator MoleHitAnimation()
    {
        isHitByHammer = true;
        // hammer animation
        ChangeMoleSprite(true);
        yield return new WaitForSeconds(1);
        ChangeMoleSprite(false);
        moveDown = true;
        // Wait same amount of sec as moveDown animation
        yield return new WaitForSeconds(lerpTime);
        Managers.MoleManager.startNextMoleT = true;
    }
}
