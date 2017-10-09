using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float noteTimer;
    private int noteTimerSec;
    private bool startTimer = false;
    private int waitTime = 5;

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
        if (startTimer)
        {
            noteTimer += Time.deltaTime;
            noteTimerSec = (int)noteTimer % 60;
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
        string randomNote = Managers.Note.GetRandomNote();
        startTimer = true;

        while(noteTimerSec < waitTime)
        {
            if (SerialRead.databyteRead)
            {
                CheckRightNote();
            }
            else
            {
                // no input note detected
            }
        }
    }

    public void CheckRightNote()
    {
        valueToNoteName(Managers.SerialRead.CurrentNoteValue);
    }

    private string valueToNoteName(int noteValue)
    {
        string noteName = "";
        return noteName;
    }
}
