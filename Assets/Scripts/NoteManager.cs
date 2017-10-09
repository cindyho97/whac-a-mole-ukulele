using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {

    private string[] noteNamesFlat =
    {
        "D3","G3","B3"
    };
    private int nrOfNotes = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetRandomNote()
    {
        int randomNr = Random.Range(0, nrOfNotes - 1);
        string noteName = noteNamesFlat[randomNr];
        return noteName;
    }
}
