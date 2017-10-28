using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public bool noneOutOfHole = false; // No mole is completely out of hole --> there can only be one mole standing at a time
    private byte nrOfMoles = 5;
    private Mole currentMole;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
	}
	
	// Update is called once per frame
	void Update () {

        // check if there are moles out of hole
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

        // get mole out of hole
        if (noneOutOfHole)
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
    }

    private void MoleAppears()
    {
        currentMole.isOutOfHole = true;
        currentMole.Popup();
        currentMole.StartNoteTimer();
    }
}
