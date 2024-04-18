using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject optionsWindow;
    [SerializeField] private GameObject videoWindow;
    [SerializeField] private GameObject logWindow;
    [SerializeField] private GameObject regestartionWindow;

    public void NewGame()
    {
        SceneManager.LoadScene("First Level");
    }

    public void Options()
    {
        optionsWindow.SetActive(true);
        videoWindow.SetActive(false);
        logWindow.SetActive(false);
        regestartionWindow.SetActive(false);
    }

 

    public void Video()
    {
        optionsWindow.SetActive(false);
        videoWindow.SetActive(true);
        logWindow.SetActive(false);
        regestartionWindow.SetActive(false);
    }

    public void Log()
    {
        optionsWindow.SetActive(false);
        videoWindow.SetActive(false);
        logWindow.SetActive(true);
        regestartionWindow.SetActive(false);
    }

    public void Regestration()
    {
        optionsWindow.SetActive(false);
        videoWindow.SetActive(false);
        logWindow.SetActive(false);
        regestartionWindow.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}