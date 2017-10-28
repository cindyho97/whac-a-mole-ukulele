using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour {

    private SortedList<string, int> noteNames;
    private int nrOfNotes;
    public string randomNote;
    private Mole currentMole;

    // Timer
    public bool timerRunning;
    private float totalWaitTime = 10;
    private float currentTime;
    private Image timerBar;

    // Use this for initialization
    void Start () {
        noteNames = new SortedList<string, int>
        {
            {"C", 102 }, {"D", 81 }, {"E", 63 }, {"F", 54 },  {"G", 38 },  {"A", 23 }, {"B", 10}, {"CHigh", 5}
        };
        nrOfNotes = noteNames.Count;
	}
	
	// Update is called once per frame
	void Update () {
        if (timerRunning)
        {
            timerBar.fillAmount = currentTime / totalWaitTime;
            currentTime -= Time.deltaTime;

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
    }

    public string GetRandomNote()
    {
        int randomNr = Random.Range(0, nrOfNotes);
        string noteName = noteNames.ElementAt(randomNr).Key;
        return noteName;
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
            Debug.Log("note value: " + note);
            if (noteNames.ContainsValue(note))
            {
                int index = noteNames.IndexOfValue(note);
                Debug.Log("index: " + noteNames.IndexOfValue(note));
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
}
