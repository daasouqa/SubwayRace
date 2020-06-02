using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gemText;
    public static int numberOfGems;
    public static bool gameOver;
    public static bool win;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject startingText;
    public GameObject pausePanel;
    public GameObject replayPanel;
    public static bool isPaused;
    public static bool isReplay;

    public static bool isGameStarted;

    public static List<GameObject> orderList;

    public AudioSource buttonAudio;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isReplay = false;
        isPaused = false;
        gameOver = false;
        win = false;
        isGameStarted = false;
        numberOfGems = 0;

        orderList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (win)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (isReplay)
        {
            replayPanel.SetActive(true);
            Time.timeScale = 0;
        }
        gemText.GetComponent<TextMeshProUGUI>().text = "Gems: " + numberOfGems;
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

    public void Replay()
    {
        buttonAudio.Play();
        SceneManager.LoadScene("Level");
    }

    public void QuitToMainMenu()
    {
        buttonAudio.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        buttonAudio.Play();
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        buttonAudio.Play();
        isPaused = true;
    }

    public void RestartMaybe()
    {
        buttonAudio.Play();
        isReplay = true;
    }

    public void CancelReplay()
    {
        buttonAudio.Play();
        isReplay = false;
        replayPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
