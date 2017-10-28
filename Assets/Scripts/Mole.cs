using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

public class Mole : MonoBehaviour {

    private Vector3 startingPosition;
    private Vector3 endPosition;
    private bool moveUp = false;
    private bool moveDown = false;
    // Time to take from start to finish
    private float lerpTime = 0.5f;
    private float currentLerpTime = 0f;

    public bool isOutOfHole = false;
    public bool isHitByHammer = false;
    public bool playedRightNote = false;
    public bool playedNote = false;
  
    private string randomNote;

    // Timer
    private static bool timerRunning;
    private float totalWaitTime = 10;
    private float currentTime;
    public Image timerBar;
    private GameObject timerBarObj;

	void Start () {
        startingPosition = transform.position;
        endPosition = new Vector3(startingPosition.x, startingPosition.y + 100, startingPosition.z);
        timerBarObj = timerBar.gameObject.transform.parent.gameObject;
	}

    private void Update()
    { 
        if (timerRunning)
        {
            timerBar.fillAmount = currentTime / totalWaitTime;
            currentTime -= Time.deltaTime;
            if (Managers.SerialRead.CheckNotePlayed())
            {
                ResetNoteValue();
                Managers.SerialRead.noteDetected = false;
                CheckPlayedNote();
                UpdatePlayerStatus(playedRightNote);
            }
            else if(currentTime < 0)
            { 
                UpdatePlayerStatus(playedRightNote);
            }
            else
            {
                Debug.Log("No note played");
            }
        }

        if (moveUp)
        {
            Popup();
        }
        else if (moveDown)
        {
            Hide();
        }

    }

    public void StartNoteTimer()
    {
        Debug.Log("Start timer!");
        randomNote = Managers.Note.GetRandomNote();
        Debug.Log("random note: " + randomNote);
        
        // start moveUp animation
        moveUp = true;
        ResetNoteValue();
        currentTime = totalWaitTime;
        timerRunning = true;
        timerBarObj.gameObject.SetActive(true);
    }

    private void CheckPlayedNote()
    {
        string notePlayed = Managers.Note.CheckNoteInRange(Managers.SerialRead.currentNoteValue);
        playedRightNote = Managers.Note.CheckRightNote(randomNote, notePlayed);
        Debug.Log("played note: " + notePlayed);
    }

    private void UpdatePlayerStatus(bool playedRightNote)
    {
        timerRunning = false;
        timerBarObj.SetActive(false);

        if (playedRightNote)
        {
            isHitByHammer = true;
            // change to hitbyhammer sprite
            Debug.Log("Score updated");
            Messenger.Broadcast(GameEvent.UPDATE_SCORE);
        }
        else
        {
            Debug.Log("Lost life...");
            Messenger.Broadcast(GameEvent.LOSE_LIFE);
        }
        // moveDown animation
        moveDown = true;
    }

    private void ResetNoteValue()
    {
        Managers.SerialRead.currentNoteValue = 0;
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
}
