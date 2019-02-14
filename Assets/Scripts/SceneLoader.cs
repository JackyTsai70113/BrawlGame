using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;
    private float timeToWait = 3f;
    private float timeToWaitfForGame = 5f;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 0:
                StartCoroutine(WaitAndLoad(timeToWait));
                break;
            case 2:
                StartCoroutine(WaitAndLoad(timeToWaitfForGame));
                break;
            case 4:
                StartCoroutine(WaitAndLoadLobbyScene(timeToWait));
                break;
            case 5:
                StartCoroutine(WaitAndLoadLobbyScene(timeToWait));
                break;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadLoseScene()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(float time)
    {
        yield return new WaitForSeconds(time);
        LoadNextScene();
    }

    IEnumerator WaitAndLoadLobbyScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
}