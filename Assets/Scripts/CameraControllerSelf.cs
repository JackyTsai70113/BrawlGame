using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSelf : MonoBehaviour {

    Vector3 PlayerToCameraPos;
    PlayerBody playerBody;

    private bool faceToPlayer;
    private float zDistance;
    private float posY = 1126f;
    private float posZ = -810;
    private float rotX = 53.322f;
    // Use this for initialization
    void Start () {
        playerBody = FindObjectOfType<PlayerBody>();
        //transform.position = Vector3.zero;
        //StartCoroutine(printSth());
        if (playerBody != null)
        {
            PlayerToCameraPos = transform.position - new Vector3(0, 0, -130);
        }
        transform.rotation = Quaternion.Euler(rotX, 0, 0);
        


    }

    // Update is called once per frame
    void Update () {
        //transform.LookAt(player.transform);
        Move();
	}
    void Move()
    {
        if (playerBody != null)
        {
            Vector3 targetPos = playerBody.transform.position +
            PlayerToCameraPos;
            if (playerBody.transform.position.z > 120)
            {
                targetPos = new Vector3(0, 0, 120) + PlayerToCameraPos;
            }
            else if (playerBody.transform.position.z < -120)
            {
                targetPos = new Vector3(0, 0, -120) + PlayerToCameraPos;
            }

            targetPos = new Vector3(0, targetPos.y, targetPos.z);
            transform.position = targetPos;
        }

    }
}
