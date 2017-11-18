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
    public Sprite moleInjured;
    private Sprite moleIdle;

    public GameObject hammer;
    private Animator hammerAnim;
    public bool hammerAnimComplete;
    private Hammer hammerScript;


    void Start () {
        startingPosition = transform.position;
        // Set end position mole
        endPosition = new Vector3(startingPosition.x, startingPosition.y+80, startingPosition.z);
        timerBarObj = timerBar.gameObject.transform.parent.gameObject;
        noteText = timerBarObj.GetComponentInChildren<Text>();
        moleImage = GetComponent<Image>();
        moleIdle = gameObject.GetComponent<Image>().sprite;
        hammerAnim = hammer.GetComponent<Animator>();
        hammerScript = hammer.GetComponent<Hammer>();
    }

    private void FixedUpdate()
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
            ChangeMoleSprite("idle");
            currentLerpTime = 0;
            isOutOfHole = false;
            StartCoroutine(SetDefaultNotePlayedColor());
        }
    }

    public void ChangeMoleSprite(string status)
    {
        if (status == "hit")
        {
            moleImage.sprite = moleHit;
        }
        else if(status == "injured")
        {
            moleImage.sprite = moleInjured;
        }
        else if(status == "idle")
        {
            moleImage.sprite = moleIdle;
        }
    }

    public IEnumerator MoleHitAnimation()
    {
        moveUp = false;
        isHitByHammer = true;
        hammer.SetActive(true);
        hammerScript.SetHammerAnim(true);

        // Wait till hammer anim is completed
        while (!hammerAnimComplete)
        {
            yield return null;
        }

        // Change mole sprite
        ChangeMoleSprite("hit");
        yield return new WaitForSeconds(.45f);
        ChangeMoleSprite("injured");
        hammerScript.SetHammerAnim(false);
        // Move down animation
        moveDown = true;

        // Delay of nextMoleTimer
        //yield return new WaitForSeconds(lerpTime);
        while (isOutOfHole)
        {
            yield return null;
        }
        Managers.MoleManager.startNextMoleT = true;
    }

    private IEnumerator SetDefaultNotePlayedColor()
    {
        yield return new WaitForSeconds(1);
        Managers.Note.ChangeNotePlayedColor("yellow");
        Managers.Note.playedNoteText.text = " ";
    }
}
