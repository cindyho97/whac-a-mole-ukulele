using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour  {

    public GameObject endImage;
    public GameObject winText;
    public GameObject loseText;
    public Image[] hearts;
    public Text scoreText;
    private byte lives;
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

    private void Start()
    {
        lives = (byte)hearts.Length;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
            LoseLife();
        }
    }
    private void LoseLife()
    {
        lives--;
        hearts[lives].gameObject.SetActive(false);
        if(lives < 0)
        {
            lives = 0;
        }

        if(lives == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
    }

    public void UpdatePlayerStatus(bool playedRightNote)
    {
        Mole currentMole = MoleManager.currentMole;
        Managers.Note.timerRunning = false;
        currentMole.timerBarObj.SetActive(false);

        if (playedRightNote)
        {
            currentMole.isHitByHammer = true;
            // change to hitbyhammer sprite
            Debug.Log("Score updated");
            Messenger.Broadcast(GameEvent.UPDATE_SCORE);
        }
        else
        {
            Debug.Log("Lost life...");
            Messenger.Broadcast(GameEvent.LOSE_LIFE);
        }
        // moveDown animation
        currentMole.moveDown = true;
        MoleManager.currentMole = null;
        Managers.MoleManager.startNextMoleT = true;
    }

    private void UpdateScore()
    {
        score += 50;
        scoreText.text = score.ToString();
    }

    private void ShowWinPopup()
    {
        endImage.SetActive(true);
        winText.SetActive(true);
    }

    private void ShowFailPopup()
    {
        endImage.SetActive(true);
        loseText.SetActive(true);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
