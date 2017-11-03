using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent  {

    public const string LOSE_LIFE = "LOSE_LIFE";
    public const string UPDATE_SCORE = "UPDATE_SCORE";
    public const string LEVEL_COMPLETE = "LEVEL_COMPLETE";
    public const string LEVEL_FAILED = "LEVEL_FAILED";

    // Settings
    public const string NR_NOTES_UPDATED = "NR_NOTES_UPDATED";
    public const string TIME_NOTE_UPDATED = "TIME_NOTE_UPDATED";
    public const string TIME_MOLE_UPDATED = "TIME_MOLE_UPDATED";

    // Tuner
    public const string G_BUTTON_PRESSED = "G_BUTTON_PRESSED";
    public const string C_BUTTON_PRESSED = "C_BUTTON_PRESSED";
    public const string E_BUTTON_PRESSED = "E_BUTTON_PRESSED";
    //public const string A_BUTTON_PRESSED = "A_BUTTON_PRESSED";
    public const string LIGHT_UPDATED = "LIGHT_UPDATED";
}
