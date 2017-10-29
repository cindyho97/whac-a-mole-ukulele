using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public Dropdown notesDropDown;
    public Slider timeNoteSlider;
    public Slider timeMoleSlider;

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
        Messenger.Broadcast(GameEvent.TIME_NOTE_UPDATED);
    }

    public void OnTimeMoleValueChanged()
    {
        Messenger.Broadcast(GameEvent.TIME_MOLE_UPDATED);
    }

    public void OnContinueButton()
    {


    }
}
