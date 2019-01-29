using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;


    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public int GetCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return currentSceneIndex;
    }

    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLoseScene()
    {
        SceneManager.LoadScene(4);
    }
}
