using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

    private Image timerCountBar;
    public float totalTime = 10;
    public float currentTime;

	// Use this for initialization
	void Start () {
        timerCountBar = GetComponent<Image>();
        currentTime = totalTime;
	}
	
	// Update is called once per frame
	void Update () {
        timerCountBar.fillAmount = currentTime / totalTime;
        currentTime -= Time.deltaTime;
	}
}
