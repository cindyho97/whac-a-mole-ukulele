using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

    public static PlayerManager Player { get; private set; }

    private void Awake()
    {
        Player = GetComponent<PlayerManager>(); 
    }
}
