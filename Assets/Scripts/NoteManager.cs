using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteManager : MonoBehaviour {

    private SortedList<string, int> noteNames;
    private int nrOfNotes = 3;

	// Use this for initialization
	void Start () {
        noteNames = new SortedList<string, int>
        {
            {"C", 102 }, {"D", 81 }, {"E", 63 }, {"F", 54 },  {"G", 38 },  {"A", 23 }, {"B", 10}, {"C", 5 }
        };
	}
	
	// Update is called once per frame
	void Update () {

	}


    public string GetRandomNote()
    {
        int randomNr = Random.Range(0, nrOfNotes);
        string noteName = noteNames.ElementAt(randomNr).Key;
        return noteName;
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
