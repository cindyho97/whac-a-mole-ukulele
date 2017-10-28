using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public byte nrOfMoles = 5;
    public static Mole currentMole;
    private bool startGame;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            startGame = true;
            ChooseRandomMole();
            MoleAppears();
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
}
