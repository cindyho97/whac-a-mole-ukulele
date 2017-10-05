using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public bool noMoleStanding = false; // There is no mole just standing out of hole --> there can only be one mole standing at a time
    private byte nrOfMoles = 5;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
	}
	
	// Update is called once per frame
	void Update () {
        if (noMoleStanding)
        {
            noMoleStanding = false;
            int randomNr = Random.Range(0, 5);
            Debug.Log("randomnr: " + randomNr);
            Mole currentMole = moles[randomNr];
            if (currentMole.isOutOfHole)
            {
                return;
            }

            currentMole.isOutOfHole = true;
            currentMole.Popup();
            //currentMole.RandomNoteGenerator
            //current
        }

    }

}
