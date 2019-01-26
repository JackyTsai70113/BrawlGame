using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSelf : MonoBehaviour {

    Vector3 PlayerToCameraPos;
    PlayerBody player;

    private bool faceToPlayer;
    private float zDistance;
    private float posY = 230f;
    private float posZ = -180;
    private float rotX = 53.322f;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerBody>();
        //transform.position = Vector3.zero;
        //StartCoroutine(printSth());
        transform.rotation = Quaternion.Euler(rotX, 0, 0);
        PlayerToCameraPos = new Vector3(0, posY, posZ);
        


    }

    // Update is called once per frame
    void Update () {
        //transform.LookAt(player.transform);
        Move();
	}
    void Move()
    {
        /*
        transform.rotation = Quaternion.Euler(xAngle, 0, 0);
        
        transform.position = player.transform.position +
            new Vector3(0, 0, -zDistance);
        */
        //transform.position = player.transform.position +
        //    PlayerToCameraPos;
        Vector3 targetPos = player.transform.position +
            PlayerToCameraPos;
        targetPos = new Vector3(0, targetPos.y, targetPos.z);
        if (targetPos.z <= (-310f)) targetPos.z = -310f;
        if (targetPos.z >= 310f) targetPos.z = 310f;
        transform.position = targetPos;
    }
}
