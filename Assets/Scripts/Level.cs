using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject Camera;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RespawnCharacter(1);
        RespawnCharacter(2);
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
        Vector3 pos = new Vector3(300, 0, 0);

        if (characterNumber == 0) {
            GameObject player = (GameObject)Instantiate(
                playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponentInChildren<PlayerBody>().transform.position = 
                new Vector3(0, 0, -160);
            player.GetComponentInChildren<PlayerBody>().SetCharacterNumber(0);
        }
        else if(characterNumber == 1 || characterNumber == 2)
        {
            if(characterNumber == 1)
                pos = new Vector3(-10, 0, -160);
            else
                pos = new Vector3(10, 0, -160);
            GameObject enemy = (GameObject)Instantiate(
                enemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponentInChildren<Enemy>().transform.position = pos;
            enemy.GetComponentInChildren<Enemy>().SetCharacterNumber(characterNumber);
            enemy.GetComponentInChildren<Enemy>().gameObject.tag = "Team0";
        }
        else if (characterNumber == 3 || characterNumber == 4 ||
                 characterNumber == 5)
        {
            if (characterNumber == 3)
                pos = new Vector3(-10, 0, 160);
            else if(characterNumber == 4)
                pos = new Vector3(0, 0, 160);
            else
                pos = new Vector3(+10, 0, 160);
            GameObject enemy = (GameObject)Instantiate(
                enemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponentInChildren<Enemy>().transform.position = pos;
            enemy.GetComponentInChildren<Enemy>().SetCharacterNumber(characterNumber);
            enemy.GetComponentInChildren<Enemy>().gameObject.tag = "Team1";
        }   
    }
}
