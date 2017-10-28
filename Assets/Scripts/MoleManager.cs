using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public byte nrOfMoles = 5;
    public static Mole currentMole;
    private bool nextMole;

    public bool startNextMoleT;
    private float timeBeforeNextMole = 5;
    private float waitTime;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
        waitTime = timeBeforeNextMole;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            nextMole = true;  
        }

        if (nextMole)
        {
            nextMole = false;
            ChooseRandomMole();
            MoleAppears();
        }

        if (startNextMoleT)
        {
            StartNextMoleTimer();
        }
    }

    private void ChooseRandomMole()
    {
        int randomNr = Random.Range(0, 5);
        Debug.Log("randomnr: " + randomNr);
        currentMole = moles[randomNr];
        Debug.Log("mole: " + currentMole);
    }

    private void MoleAppears()
    {
        currentMole.isOutOfHole = true;
        currentMole.Popup();
        Managers.Note.StartNoteTimer();
    }

    public void StartNextMoleTimer()
    {
        waitTime -= Time.deltaTime;

        if(waitTime <= 0)
        {
            startNextMoleT = false;
            nextMole = true;
            waitTime = timeBeforeNextMole;
        }
    }
}
