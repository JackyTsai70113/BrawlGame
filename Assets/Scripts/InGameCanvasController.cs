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

    private int counter = 3;

    private void Start()
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
            default:
                break;
        }
    }

    private void SetRespawningTime()
    {
        PlayerRespawnMessage.SetActive(true);
        StartCoroutine(CountToRespawn());
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
            FindObjectOfType<Respawner>().RespawnCharacter(0);
            PlayerRespawnMessage.SetActive(false);
            counter = 3;
        }
    }
}
