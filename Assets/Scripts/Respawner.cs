using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject Camera;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        /*
        RespawnCharacter(1);
        RespawnCharacter(2);
        */      
        RespawnCharacter(3);
        RespawnCharacter(4);
        RespawnCharacter(5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnCharacter(int characterNumber)
    { 
        if (characterNumber == 0)
        {
            GameObject player = (GameObject)Instantiate(
            playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerBody playerBody =
                player.GetComponentInChildren<PlayerBody>();
            playerBody.transform.position =
                new Vector3(0, 0, -160);
            playerBody.SetCharacterNumber(0);
            return;
        }


        Vector3 pos = Vector3.zero;
        string teamTag = "";

        switch (characterNumber)
        { 
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
        GameObject enemy = (GameObject)Instantiate(
                enemyPrefab, Vector3.zero, Quaternion.identity);
        Enemy enemyBody = enemy.GetComponentInChildren<Enemy>();
        enemyBody.transform.position = pos;
        enemyBody.SetCharacterNumber(characterNumber);
        enemyBody.tag = teamTag;
    }
}
