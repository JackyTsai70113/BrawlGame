﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    int kills;
    int lifeNumber = 3;
    bool playerIsAlive;
    public Grass[] allGrass;
    GameObject playerBody;
    private void Start()
    {
        allGrass = FindObjectsOfType<Grass>();
    }

    public void AddKills()
    {
        kills++;
        if (kills >= 3)
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
