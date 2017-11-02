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

    public GameObject hammer;
    private Animator hammerAnim;
    public bool hammerAnimComplete;
    private Hammer hammerScript;


    void Start () {
        startingPosition = transform.position;
        // Set end position mole
        endPosition = new Vector3(startingPosition.x, startingPosition.y+90, startingPosition.z);
        timerBarObj = timerBar.gameObject.transform.parent.gameObject;
        noteText = timerBarObj.GetComponentInChildren<Text>();
        moleImage = GetComponent<Image>();
        moleIdle = gameObject.GetComponent<Image>().sprite;
        hammerAnim = hammer.GetComponent<Animator>();
        hammerScript = hammer.GetComponent<Hammer>();
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

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    hammer.SetActive(true);
        //    hammerAnim.SetBool("HammerHit", true);
        //}
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
        }
        else
        {
            moleImage.sprite = moleIdle;
        }
    }

    public IEnumerator MoleHitAnimation()
    {
        
        isHitByHammer = true;
        hammer.SetActive(true);
        hammerScript.SetHammerAnim(true);

        // Wait till hammer anim is completed
        while (!hammerAnimComplete)
        {
            yield return null;
        }

        // Change mole sprite
        ChangeMoleSprite(true);
        yield return new WaitForSeconds(.5f);
        ChangeMoleSprite(false);
        hammerScript.SetHammerAnim(false);
        // Move down animation
        moveDown = true;

        // Wait same amount of sec as moveDown animation
        yield return new WaitForSeconds(lerpTime);
        Managers.MoleManager.startNextMoleT = true;
    }
}
