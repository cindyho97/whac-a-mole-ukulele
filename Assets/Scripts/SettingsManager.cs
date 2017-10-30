using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public GameObject settingsImage;
    public Dropdown notesDropDown;
    public Slider timeNoteSlider;
    public Slider timeMoleSlider;
    public Text noteTimeText;
    public Text moleTimeText;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.NR_NOTES_UPDATED, UpdateNrNotes);
        Messenger.AddListener(GameEvent.TIME_NOTE_UPDATED, UpdateNoteTime);
        Messenger.AddListener(GameEvent.TIME_MOLE_UPDATED, UpdateMoleTime);   
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.NR_NOTES_UPDATED, UpdateNrNotes);
        Messenger.RemoveListener(GameEvent.TIME_NOTE_UPDATED, UpdateNoteTime);
        Messenger.RemoveListener(GameEvent.TIME_MOLE_UPDATED, UpdateMoleTime);
    }

    // Use this for initialization
    void Start () {
        UpdateSettingsValues();
    }
	
    private void UpdateNrNotes()
    {
        Managers.Note.UpdateNoteList(notesDropDown.value);
    }

    private void UpdateNoteTime()
    {
        Managers.Note.UpdateNoteTime(timeNoteSlider.value);
    }

    private void UpdateMoleTime()
    {
        Managers.MoleManager.UpdateMoleTime(timeMoleSlider.value);
    }

    public void OnNotesDropDownChanged()
    {
        Messenger.Broadcast(GameEvent.NR_NOTES_UPDATED);
    }

    public void OnTimeNoteValueChanged()
    {
        noteTimeText.text = timeNoteSlider.value.ToString();
        Messenger.Broadcast(GameEvent.TIME_NOTE_UPDATED);
    }

    public void OnTimeMoleValueChanged()
    {
        moleTimeText.text = timeMoleSlider.value.ToString();
        Messenger.Broadcast(GameEvent.TIME_MOLE_UPDATED);
    }

    public void OnContinueButton()
    {
        settingsImage.SetActive(false);
        SaveSettings();
    }

    public void OnSettingsButton()
    {
        if (settingsImage.activeSelf)
        {
            settingsImage.SetActive(false);
            SaveSettings();
        }
        else { settingsImage.SetActive(true); }
    }

    private void UpdateSettingsValues()
    {
        notesDropDown.value = PlayerPrefs.GetInt("NotesDropDown", 0);
        timeNoteSlider.value = PlayerPrefs.GetInt("TimeNoteSlider", 10);
        timeMoleSlider.value = PlayerPrefs.GetInt("TimeMoleSlider", 5);
        Managers.Note.UpdateNoteList(notesDropDown.value);
        Managers.Note.UpdateNoteTime(timeNoteSlider.value);
        Managers.MoleManager.UpdateMoleTime(timeMoleSlider.value);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("NotesDropDown", notesDropDown.value);
        PlayerPrefs.SetInt("TimeNoteSlider", (int)timeNoteSlider.value);
        PlayerPrefs.SetInt("TimeMoleSlider", (int)timeMoleSlider.value);
    }
}
