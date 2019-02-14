using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    int kills;
    public int targetKills = 5;
    int lifeNumber = 3;
    bool playerIsAlive;
    GameObject playerBody;

    private void Start()
    {
        GetComponentInChildren<InGameCanvasController>().SetKills(kills, targetKills);
    }

    public void AddKills()
    {
        kills++;
        GetComponentInChildren<InGameCanvasController>().SetKills(kills, targetKills);
        if (kills >= targetKills)
            GetComponent<SceneLoader>().LoadWinScene();
    }

    public void MinusLifeNumber()
    {
        lifeNumber--;
        GetComponentInChildren<InGameCanvasController>().
            SetLifeNumber(lifeNumber);
        if (lifeNumber == 0)
            GetComponent<SceneLoader>().LoadLoseScene();
    }

    public void SetPlayerStatus(bool playerIsAlive)
    {
        this.playerIsAlive = playerIsAlive;
    }

    public void SetPlayerBody(GameObject playerBody)
    {
        this.playerBody = playerBody;
    }

    public bool GetPlayerStatus()
    {
        return playerIsAlive;
    }

    public GameObject GetPlayerBody()
    {
        return playerBody;
    }
}