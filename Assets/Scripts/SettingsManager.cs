using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public static float timeForPlayingNote;
    public static float timeBeforeNextMole;
    public Dropdown dropDown;

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
        Managers.Note.UpdateNoteList(dropDown.value);
    }

    private void UpdateNoteTime()
    {

    }

    private void UpdateMoleTime()
    {

    }

    public void OnContinueButton()
    {
        Messenger.Broadcast(GameEvent.TIME_NOTE_UPDATED);
        Messenger.Broadcast(GameEvent.TIME_MOLE_UPDATED);
    }

    public void OnValueChanged()
    {
        Messenger.Broadcast(GameEvent.NR_NOTES_UPDATED);
    }
}
