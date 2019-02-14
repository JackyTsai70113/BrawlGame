using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Material[] materials;
    MeshRenderer meshRender;
    GameObject playerBodyObject;
    GameStatus gameStatus;

	void Start () 
    {
        meshRender = GetComponentInChildren<MeshRenderer>();
        gameStatus = FindObjectOfType<GameStatus>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        CheckForTransparent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.gameObject == playerBodyObject)
        {
            other.GetComponent<PlayerBody>().AddGrassNumber();
            other.GetComponent<PlayerBody>().CheckForTransparent();
        }
        else if (other.tag == "Team1")
            other.GetComponent<Enemy>().AddGrassNumber();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;
        if (other.gameObject == playerBodyObject)
        {
            other.GetComponent<PlayerBody>().MinusGrassNumber();
            other.GetComponent<PlayerBody>().CheckForTransparent();
        }
        else if (other.tag == "Team1")
            other.GetComponent<Enemy>().MinusGrassNumber();
    }

    public void BeTransparent()
    {
        meshRender.sharedMaterial = materials[1];
    }

    public void BeNotTransparent()
    {
        meshRender.sharedMaterial = materials[0];
    }

    private void CheckForTransparent()
    {
        if (gameStatus.GetPlayerStatus())
        {
            playerBodyObject = gameStatus.GetPlayerBody();
            float distanceFromPlayer = Vector3.Distance
                (transform.position, playerBodyObject.transform.position);
            if (distanceFromPlayer <= 20)
                BeTransparent();
            else
                BeNotTransparent();
        }
    }
}