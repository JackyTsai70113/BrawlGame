using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    int kills;
    int lifeNumber = 3;

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
            FindObjectOfType<SceneLoader>().LoadLoseScene();
    }
}
