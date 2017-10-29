using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour {

    public Mole[] moles;
    public byte nrOfMoles = 5;
    public static Mole currentMole;
    public bool nextMole;

    public Image nextMoleBar;
    public GameObject nextMoleBarObj;
    public bool startNextMoleT;
    private float timeBeforeNextMole = 5;
    private float waitTime;

	// Use this for initialization
	void Start () {
        moles = new Mole[nrOfMoles];
        moles = GameObject.FindObjectsOfType<Mole>();
        waitTime = timeBeforeNextMole;
        nextMoleBarObj = nextMoleBar.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    nextMole = true;  
        //}

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
        Debug.Log("moleTime: " + timeBeforeNextMole);
    }
}
