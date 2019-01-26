using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody: Character
{
    [Header("Movement Setting")]
    public bool speedUp;
    public float moveSpeed;
    public float rotationSpeed = 15;
    public float rotateToBulletDuration = 1f;

    //Shooting Variables
    private bool shooting = false;
    private Vector3 bulletDirection;

    //Components
    private Rigidbody rb;
   
    void Start()
    {
        InitialHpAndComponents();
        SetCharacterNumber(0);
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
        if (isShooting)
        {

        }
    }

    public void Move()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        Vector3 moveVelocity = 
            new Vector3(deltaX, 0, deltaZ);
        if (speedUp)
            moveVelocity *= 3;
        rb.MovePosition(transform.position + 
            Vector3.Normalize(moveVelocity) * moveSpeed * Time.deltaTime);
        //rotation
        if (shooting)
        {
            var newRotation = Quaternion.LookRotation(bulletDirection);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);
            
        }
        else if (deltaX != 0 || deltaZ != 0)
        {
            var newRotation = Quaternion.Euler(0,
                Mathf.Atan2(deltaX, deltaZ) * 180 / Mathf.PI, 0);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);
        }
        else
        {
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
        }
    }
    
    public void Shooting(Vector3 shootVelocity)
    {
        bulletDirection = shootVelocity;
        if (shooting == false)
            StartCoroutine(SetShootingCoroutine());
    }

    IEnumerator SetShootingCoroutine()
    {
        shooting = true;
        yield return new WaitForSeconds(rotateToBulletDuration);
        shooting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            //Debug.Log("character" + GetCharacterNumber() + "is shot by " +
            //    damageDealer.GetCharacterNumber());
            MinusHp(damageDealer.GetDamage());
            Destroy(other.gameObject, 0.1f);
        }
    }
}