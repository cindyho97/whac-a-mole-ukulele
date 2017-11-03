using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Sorted List --> ElementAt()

public class Tuner : MonoBehaviour {

    private SortedList<string,int> tuneNotes;
    public static bool enableInput;
    public Button buttonG;
    public Button buttonC;
    public Button buttonE;
    //public Button buttonA;
    public Slider noteSlider;

    public Image lightImage;
    private Color32 red = new Color32(245, 90, 90, 255);
    private Color32 green = new Color32(65, 210, 50, 255);
    private Color32 buttonYellow = new Color32(248, 207, 73, 255);

    private int noteValue;
    private int sliderRange = 20;
    public Text noteValueText;
    public Text minValueText;
    public Text maxValueText;
    private Button activeButton;
    private Button previousButton;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.G_BUTTON_PRESSED,()=> SetButtonPressed("G"));
        Messenger.AddListener(GameEvent.C_BUTTON_PRESSED, ()=> SetButtonPressed("C"));
        Messenger.AddListener(GameEvent.E_BUTTON_PRESSED, ()=> SetButtonPressed("E"));
        //Messenger.AddListener(GameEvent.A_BUTTON_PRESSED, OnAButton);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.G_BUTTON_PRESSED, () => SetButtonPressed("G"));
        Messenger.RemoveListener(GameEvent.C_BUTTON_PRESSED, () => SetButtonPressed("C"));
        Messenger.RemoveListener(GameEvent.E_BUTTON_PRESSED, () => SetButtonPressed("E"));
        //Messenger.RemoveListener(GameEvent.A_BUTTON_PRESSED, OnAButton);
    }
    // Use this for initialization
    void Start () {
		tuneNotes = new SortedList<string, int>
        { 
            {"G", 38 }, {"C", 102 }, {"E", 63 } // {"A", }
        };

        enableInput = true;
    }
	
	// Update is called once per frame
	void Update () {
        CheckNoteInput();
	}

    private void CheckNoteInput()
    {
        // Note played
        if (Managers.SerialRead.CheckNotePlayed() && enableInput)
        {
            enableInput = false;
            Managers.SerialRead.noteDetected = false;
            Debug.Log("noteValue: " + Managers.SerialRead.currentNoteValue); 
            noteSlider.value = Managers.SerialRead.currentNoteValue;
            Debug.Log("notesliderValue" + noteSlider.value);
        }

        // Update ready light
        if (enableInput)
        {
            SetLight(true);
        }
        else{SetLight(false);}
    }

    private void SetLight(bool enable)
    {
        if (enable)
        {
            lightImage.color = green;
        }
        else {lightImage.color = red;}
    }

    private void ChangeButtonColor(Button button, bool pressed)
    {
        ColorBlock cb = button.colors; 
        if (pressed)
        {
            cb.normalColor = button.colors.pressedColor;
        }
        else
        {
            cb.normalColor = buttonYellow;
        }
    }

    public void OnGButton()
    {
        Messenger.Broadcast(GameEvent.G_BUTTON_PRESSED);
    }

    public void OnCButton()
    {
        Messenger.Broadcast(GameEvent.C_BUTTON_PRESSED);
    }

    public void OnEButton()
    {
        Messenger.Broadcast(GameEvent.E_BUTTON_PRESSED);
    }

    private void SetNoteValue(string button)
    {
        int index = tuneNotes.IndexOfKey(button);
        noteValue = tuneNotes.ElementAt(index).Value;
    }

    private void SetNoteSliderValues()
    {
        noteSlider.minValue = noteValue - sliderRange;
        noteSlider.maxValue = noteValue + sliderRange;

        minValueText.text = noteSlider.minValue.ToString();
        maxValueText.text = noteSlider.maxValue.ToString();
        noteValueText.text = noteValue.ToString();
    }

    private void SetButtonPressed(string buttonLetter)
    {
        CheckActiveButton(buttonLetter);
        SetNoteValue(buttonLetter);  
        SetNoteSliderValues();
    }

    private void CheckActiveButton(string buttonLetter)
    {        
        if(previousButton != null)
        {
            previousButton = activeButton;
        }

        switch (buttonLetter)
        {
            case "G":
                activeButton = buttonG;
                break;
            case "C":
                activeButton = buttonC;
                break;
            case "E":
                activeButton = buttonE;
                break;
                //case "A":
                //    activeButton = buttonA;
                //    break;
        }
        ChangeButtonColor(activeButton, true);

        if (activeButton != previousButton && previousButton != null)
        {
            ChangeButtonColor(previousButton, false);
        }
    }

    public void OnContinueButton()
    {
        gameObject.SetActive(false);
    }

    public void OnTunerButton()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else { gameObject.SetActive(true); }
    }
}
