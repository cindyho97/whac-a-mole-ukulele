using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

    public static PlayerManager Player { get; private set; }
    public static MoleManager MoleManager { get; private set; }
    public static NoteManager Note { get; private set; }
    public static SerialRead SerialRead { get; private set; }
    public static SettingsManager Settings { get; private set; }

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        MoleManager = GetComponent<MoleManager>();
        Note = GetComponent<NoteManager>();
        SerialRead = GetComponent<SerialRead>();
        Settings = GetComponent<SettingsManager>();
    }
}
