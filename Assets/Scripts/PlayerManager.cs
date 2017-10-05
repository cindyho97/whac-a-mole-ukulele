using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour  {

    public GameObject endImage;
    public GameObject winText;
    public GameObject loseText;
    private byte lives = 5;
    private ushort score;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.LOSE_LIFE, LoseLife);
        Messenger.AddListener(GameEvent.UPDATE_SCORE, UpdateScore);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, ShowFailPopup);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, ShowWinPopup);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LOSE_LIFE, LoseLife);
        Messenger.RemoveListener(GameEvent.UPDATE_SCORE, UpdateScore);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, ShowFailPopup);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, ShowWinPopup);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("pressed space");
            Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
        }
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

    public void StartGame()
    {
        Managers.MoleManager.noMoleStanding = true;
    }

    private void ShowWinPopup()
    {
        Debug.Log("winpopup");
        endImage.SetActive(true);
        winText.SetActive(true);
    }

    private void ShowFailPopup()
    {
        endImage.SetActive(true);
        loseText.SetActive(true);
    }

    private void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnQuitButton()
    {
        Application.Quit();
    }
}
