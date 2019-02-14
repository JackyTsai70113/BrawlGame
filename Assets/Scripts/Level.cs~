using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    GameStatus gameStatus;

    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        RespawnCharacter(0);
        RespawnCharacter(3);
        RespawnCharacter(4);
        RespawnCharacter(5);
    }

    public void RespawnCharacter(int characterNumber)
    { 
        Vector3 pos = Vector3.zero;
        string teamTag = "";

        switch (characterNumber)
        {
            case 0:
                GameObject player = (GameObject)Instantiate(
                    playerPrefab, Vector3.zero, Quaternion.identity);
                PlayerBody playerBody =
                    player.GetComponentInChildren<PlayerBody>();
                playerBody.transform.position =
                    new Vector3(0, 0, -160);
                gameStatus.SetPlayerStatus(true);
                gameStatus.SetPlayerBody(playerBody.gameObject);
                return;
            case 1:
                pos = new Vector3(-10, 0, -160);
                teamTag = "Team0";
                break;
            case 2:
                pos = new Vector3(10, 0, -160);
                teamTag = "Team0";
                break;
            case 3:
                pos = new Vector3(-10, 0, 160);
                teamTag = "Team1";
                break;
            case 4:
                pos = new Vector3(0, 0, 160);
                teamTag = "Team1";
                break;
            case 5:
                pos = new Vector3(10, 0, 160);
                teamTag = "Team1";
                break;
        }
        GameObject enemy = Instantiate(
                enemyPrefab, Vector3.zero, Quaternion.identity);
        Enemy enemyBody = enemy.GetComponentInChildren<Enemy>();
        enemyBody.transform.position = pos;
        enemyBody.SetCharacterNumber(characterNumber);
        enemyBody.InitialHpAndComponents();
        enemyBody.tag = teamTag;
    }
}