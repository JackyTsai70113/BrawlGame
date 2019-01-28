using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    int kills;
    private bool[] liveCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLiveCharacter(int characterNumber, bool characterStatus)
    {
        liveCharacter[characterNumber] = characterStatus;
    }

    public bool GetLiveCharacter(int characterNumber)
    {
        return liveCharacter[characterNumber];
    }

    public void AddKills()
    {
        kills++;
        if (kills >= 3)
            GetComponent<SceneLoader>().LoadWinScene();
    }

}
