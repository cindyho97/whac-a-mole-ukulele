using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour {

    private Mole[] moles;
    public byte nrOfMoles = 5;
    public static Mole currentMole;
    public bool nextMole;

    public Image nextMoleBar;
    public GameObject nextMoleBarObj;
    public bool startNextMoleT;
    private float timeBeforeNextMole;
    private float waitTime;

    private int previousMoleNr;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
        nextMoleBarObj = nextMoleBar.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (nextMole)
        {
            nextMole = false;
            ChooseRandomMole();
            MoleAppears();
        }
        
        // Mole timer running
        if (startNextMoleT)
        {
            StartNextMoleTimer();
        }
    }

    private void ChooseRandomMole()
    {
        int randomNr;
        do
        {
            randomNr = Random.Range(0, nrOfMoles);
        } while (CheckSameMoleNr(randomNr));

        previousMoleNr = randomNr;
        currentMole = moles[randomNr];
    }

    private bool CheckSameMoleNr(int currentNr)
    {
        if(currentNr == previousMoleNr)
        {
            return true;
        }
        else { return false; }
    }

    private void MoleAppears()
    {
        currentMole.isOutOfHole = true;
        currentMole.Popup();
        Managers.Note.StartNoteTimer();
    }

    public void StartNextMoleTimer()
    {
        nextMoleBarObj.SetActive(true);
        nextMoleBar.fillAmount = waitTime / timeBeforeNextMole;
        waitTime -= Time.deltaTime;

        if(waitTime <= 0)
        {
            nextMoleBarObj.SetActive(false);
            startNextMoleT = false;
            nextMole = true;
            waitTime = timeBeforeNextMole; 
        }
    }

    public void UpdateMoleTime(float sliderValue)
    {
        waitTime = sliderValue;
        timeBeforeNextMole = sliderValue;
    }

    
}
