using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Mole : MonoBehaviour {

    private Vector3 startingPosition;
    private Vector3 endPosition;
    private bool moveUp = false;
    private bool moveDown = false;
    // Time to take from start to finish
    private float lerpTime = 0.5f;
    private float currentLerpTime = 0f;

    public bool isOutOfHole = false;
    public bool isInHole = false;
    public bool isHitByHammer = false;
    public bool playedRightNote = false;
    public bool playedNote = false;

    private static Timer noteTimer1;
    private float noteTimer;
    private static float noteTimerSec = 0;
    private bool startTimer = false;
    private static int waitTime = 5;
    private static bool timerRunning;
    private string randomNote;

	void Start () {
        startingPosition = transform.position;
        endPosition = new Vector3(startingPosition.x, startingPosition.y + 100, startingPosition.z);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            moveUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            moveDown = true;
        }
 
        if (moveUp)
        {
            Popup();
        }
        else if (moveDown)
        {
            Hide();
        }

        if (Input.GetKeyDown(KeyCode.T)) // start timer
        {
            Debug.Log("T is pressed");
            StartNoteTimer();
        }

        if (timerRunning)
        {
            if (Managers.SerialRead.CheckNotePlayed())
            {
                Managers.SerialRead.noteDetected = false;
                CheckPlayedNote();
                timerRunning = false;
                noteTimer1.Enabled = false;
                Debug.Log("timer stopped");
                noteTimerSec = 0;
                UpdatePlayerStatus(playedRightNote);
            }
            else
            {
                Debug.Log("No note played");
            }
        }
        
    }

    public void Popup()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float speed = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startingPosition, endPosition, speed);

        if(transform.position == endPosition)
        {
            moveUp = false;
            currentLerpTime = 0;
            isOutOfHole = true;
        }
    }

    public void Hide()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
            
        }

        float speed = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(endPosition, startingPosition, speed);

        if(transform.position == startingPosition)
        {
            moveDown = false;
            currentLerpTime = 0;
            isInHole = true;
        }
    }

    public void StartNoteTimer()
    {
        //startTimer = true;
        
        Debug.Log("Start timer!");
        randomNote = Managers.Note.GetRandomNote();
        Debug.Log("random note: " + randomNote);

        noteTimer1 = new Timer();
        noteTimer1.Interval = 500;
        noteTimer1.Elapsed += NoteTimer1_Elapsed;
        noteTimer1.Enabled = true;
        ResetNoteValue();
        timerRunning = true;
    }

    private void NoteTimer1_Elapsed(object sender, ElapsedEventArgs e)
    {
        noteTimerSec += .5f;
        Debug.Log("timer sec: " + noteTimerSec);

        if (noteTimerSec >= waitTime && timerRunning)
        {
            timerRunning = false;
            noteTimer1.Enabled = false;
            noteTimerSec = 0;
            Debug.Log("timer stopped");
        }
    }

    private void CheckPlayedNote()
    {
        string notePlayed = Managers.Note.CheckNoteInRange(Managers.SerialRead.CurrentNoteValue);
        playedRightNote = Managers.Note.CheckRightNote(randomNote, notePlayed);
        Debug.Log("played note: " + notePlayed);
    }

    private void UpdatePlayerStatus(bool playedRightNote)
    {
        if (playedRightNote)
        {
            Debug.Log("Score updated");
            Messenger.Broadcast(GameEvent.UPDATE_SCORE);
        }
        else
        {
            Debug.Log("Lost life...");
            Messenger.Broadcast(GameEvent.LOSE_LIFE);
        }

        ResetNoteValue();
    }

    private void ResetNoteValue()
    {
        Managers.SerialRead.CurrentNoteValue = 0;
    }

}
