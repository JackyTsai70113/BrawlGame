using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameCanvasController : MonoBehaviour
{
    public GameObject firstLife;
    public GameObject secondLife;
    public GameObject thirdLife;
    public GameObject PlayerRespawnMessage;
    public TextMeshProUGUI PlayerRespawnDuration;
    public TextMeshProUGUI killsText;

    private int counter = 3;

    void Start()
    {
        PlayerRespawnMessage.SetActive(false);
    }

    public void SetLifeNumber(int n)
    {
        switch (n)
        {
            case 0:
                thirdLife.SetActive(false);
                break;
            case 1:
                secondLife.SetActive(false);
                SetRespawningTime();
                break;
            case 2:
                firstLife.SetActive(false);
                SetRespawningTime();
                break;
        }
    }

    private void SetRespawningTime()
    {
        PlayerRespawnMessage.SetActive(true);
        StartCoroutine(CountToRespawn());
    }

    public void SetKills(int kills, int targetKills)
    {
        killsText.text = "Kills: " + kills + "/" + targetKills;
    }

    IEnumerator CountToRespawn()
    {
        PlayerRespawnDuration.text = counter.ToString();
        counter--;
        yield return new WaitForSeconds(1f);
        if (counter >= 1)
            StartCoroutine(CountToRespawn());
        else
        {
            FindObjectOfType<Level>().RespawnCharacter(0);
            PlayerRespawnMessage.SetActive(false);
            counter = 3;
        }
    }
}