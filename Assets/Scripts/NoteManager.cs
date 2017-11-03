using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Sorted List --> ElementAt()
using UnityEngine.UI;

public class NoteManager : MonoBehaviour {

    private SortedList<string, int> noteNames;
    private SortedList<string, int> fourNotesList;
    private SortedList<string, int> eightNotesList;
    private int nrOfNotes;
    public string randomNote;
    private Mole currentMole;
    private int previousNr;

    // Timer
    public bool timerRunning;
    private float totalWaitTime;
    private float currentTime;
    private Image timerBar;

    // Use this for initialization
    void Start () {
        fourNotesList = new SortedList<string, int>
        {
            {"C", 102 }, {"E", 63 }, {"G", 38 },
        };
        eightNotesList = new SortedList<string, int>
        {
            {"C", 102 }, {"D", 81 }, {"E", 63 }, {"F", 54 },  {"G", 38 },  {"A", 23 }, {"B", 10}, {"Ch", 5}
        };
	}
	
	// Update is called once per frame
	void Update () {
        if (timerRunning)
        {
            UpdateTimerBar();
            CheckNoteInput();
        }
    }

    public void UpdateNoteList(int valueDropDown)
    {
        switch(valueDropDown)
        {
            case 0:
                noteNames = fourNotesList;
                break;
            case 1:
                noteNames = eightNotesList;
                break;
        }
        nrOfNotes = noteNames.Count;
    }

    public string GetRandomNote()
    {
        int randomNr;
        do
        {
            randomNr = Random.Range(0, nrOfNotes);
        }
        while (CheckSameNr(randomNr));

        previousNr = randomNr;
        string noteName = noteNames.ElementAt(randomNr).Key;
        return noteName;
    }

    private bool CheckSameNr(int currentNr)
    {
        if (currentNr == previousNr)
        {
            return true;  
        }
        else { return false; }
    }

    private void AssignCurrentMole()
    {
        currentMole = MoleManager.currentMole;
        timerBar = currentMole.timerBar;
    }

    public void StartNoteTimer()
    {
        Debug.Log("Start timer!");
        randomNote = GetRandomNote();   
        Debug.Log("random note: " + randomNote);

        AssignCurrentMole();
        currentMole.noteText.text = randomNote;
        currentMole.moveUp = true;
        currentTime = totalWaitTime;
        timerRunning = true;
        currentMole.timerBarObj.SetActive(true);
    }

    public void CheckPlayedNote(string randomNote)
    {
        string notePlayed = Managers.Note.CheckNoteInRange(Managers.SerialRead.currentNoteValue);
        currentMole.playedRightNote = Managers.Note.CheckRightNote(randomNote, notePlayed);
        Debug.Log("played note: " + notePlayed);
    }

    public string CheckNoteInRange(int noteValue)
    {
        string noteName = "";
        List<int> notesInRange = new List<int>() { noteValue - 3, noteValue - 2, noteValue - 1, noteValue, noteValue + 1, noteValue + 2, noteValue + 3};

        foreach(int note in notesInRange)
        {
            if (noteNames.ContainsValue(note))
            {
                int index = noteNames.IndexOfValue(note);
                noteName = noteNames.ElementAt(index).Key;
            }
        }

        if(noteName == "")
        {
            Debug.Log("Note not detected");
        }
        else
        {
            Debug.Log("Played note name: " + noteName);
        }
        
        return noteName;
    }

    public bool CheckRightNote(string randomNote, string notePlayed)
    {
        if(randomNote == notePlayed)
        {
            Debug.Log("Notes are the same!");
            return true;
        }
        else
        {
            Debug.Log("Note is wrong...");
            return false;
        }
    }

    private void CheckNoteInput()
    {
        // Check if there is note input detected
        if (Managers.SerialRead.CheckNotePlayed())
        {
            Managers.SerialRead.noteDetected = false;
            CheckPlayedNote(randomNote);
            Managers.Player.UpdatePlayerStatus(currentMole.playedRightNote);
        }
        else if (currentTime <= 0) // Timer hits 0
        {
            Managers.Player.UpdatePlayerStatus(currentMole.playedRightNote);
        }
        else
        {
            Debug.Log("No note played");
        }
    }

    public void UpdateNoteTime(float sliderValue)
    {
        totalWaitTime = sliderValue;
    }

    private void UpdateTimerBar()
    {
        timerBar.fillAmount = currentTime / totalWaitTime;
        currentTime -= Time.deltaTime;
    }
}
