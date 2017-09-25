using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour  {

    private byte lives = 5;
    private ushort score;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.LOSE_LIFE, LoseLife);
        Messenger.AddListener(GameEvent.UPDATE_SCORE, UpdateScore);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LOSE_LIFE, LoseLife);
        Messenger.RemoveListener(GameEvent.UPDATE_SCORE, UpdateScore);
    }
    private void LoseLife()
    {
        lives--;

        if(lives < 0)
        {
            lives = 0;
        }

        if(lives == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
    }

    private void UpdateScore()
    {
        score++;
    }
}
