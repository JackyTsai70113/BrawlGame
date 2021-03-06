﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour 
{
    [Header("Bullet Setting")]
    public GameObject bulletPrefab;
    public float bulletYPos = 8f;
    public float bulletDistance;

    [Header("Shooting Setting")]
    public int damage;
    public float shootSpeed = 10;
    public float shootDuration = 2f;
    public AudioClip shootingAudio;

    private Coroutine shootingCoroutine;
    private int floorLayerMask = 1 << 10;

    void Update()
    {
        Shot();
    }

    public void Shot()
    {
        if (Input.GetButtonDown("Fire1"))
            shootingCoroutine = StartCoroutine(ShootContinously());
        else if (!Input.GetButton("Fire1"))
            if (shootingCoroutine != null)
                StopCoroutine(shootingCoroutine);
    }

    IEnumerator ShootContinously()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 bulletTargetPos = Vector3.zero;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayerMask))
                bulletTargetPos = hit.point;
            Vector3 bulletEndPos = bulletTargetPos;
            Vector3 bulletStartPos = transform.position +
                new Vector3(0, bulletYPos, 0) +
                transform.forward * bulletDistance;
            Vector3 shootVelocity = 
                Vector3.Normalize(bulletEndPos - bulletStartPos) * shootSpeed;
            GameObject bullet = Instantiate(bulletPrefab,bulletStartPos,
                Quaternion.LookRotation(shootVelocity));
            bullet.GetComponent<Rigidbody>().velocity = shootVelocity;
            Destroy(bullet, 5f);

            // player need infomation to rotate.
            GetComponent<PlayerBody>().Shooting(shootVelocity);

            AudioSource.PlayClipAtPoint(
                shootingAudio, Camera.main.transform.position, 0.2f);
            yield return new WaitForSeconds(shootDuration);
        }
    }
}