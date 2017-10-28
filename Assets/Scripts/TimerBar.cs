using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {


    public Image timerCountBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timerCountBar.fillAmount = Mole.noteTimerSec / Mole.waitTime; 
	}
}
