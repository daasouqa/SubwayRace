using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonClick;


    private void Start()
    {
        Time.timeScale = 1;
        Debug.Log("MainMenu time scale back to 1");
    }
    public void StartGame()
    {
        StartCoroutine(PlaySound());
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySound());
        Application.Quit();
    }

    IEnumerator PlaySound()
    {
        buttonClick.Play();
        yield return new WaitWhile(() => buttonClick.isPlaying);
        //do something
    }

    
}
