using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public bool noneOutOfHole = false; // No mole is completely out of hole --> there can only be one mole standing at a time
    private byte nrOfMoles = 5;
    private Mole currentMole;
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
        }

        // Check if there are moles out of hole
        if (!noneOutOfHole)
        {
            bool noMoles = true;
            foreach(Mole mole in moles)
            {
                if (mole.isOutOfHole)
                {
                    noMoles = false;
                }
            }

            if (noMoles)
            {
                noneOutOfHole = true;
            }
        }

        // Get mole out of hole
        if (noneOutOfHole && startGame)
        {
            noneOutOfHole = false;
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
        currentMole.StartNoteTimer();
    }
}
