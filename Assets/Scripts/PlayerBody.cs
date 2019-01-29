using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody: Character
{
    public bool invulnerable;
    [Header("Movement Setting")]
    public bool speedUp;
    public float moveSpeed;
    public float rotationSpeed = 15;
    public float rotateToBulletDuration = 1f;

    //Shooting Variables
    private bool ifRotateToBullet = false;
    private Vector3 bulletDirection;
    private Rigidbody rb;

    public AudioClip healingAudio;
    public AudioClip ScreamingAudio;
    Coroutine HealSelfCoroutine;
    public float durationToWaitForHealSelf = 3f;
    public int hpToHealSelf;

    LayerMask enemyBulletLayer = 14;




    void Start()
    {
        InitialHpAndComponents();
        SetCharacterNumber(0);
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MoveAndRotate();
    }
    private void Update()
    {
        info.GetComponent<InfoSetter>().
            SetInfoTransform(transform.position);
        if (hp < maxHp && HealSelfCoroutine == null)
        {
            ResetHealSelfCoroutine();   
        }

    }

    private void MoveAndRotate()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        Vector3 deltaMovement = new Vector3(deltaX, 0, deltaZ);
        if (speedUp)
            rb.MovePosition(transform.position +
                Vector3.Normalize(deltaMovement) * moveSpeed * 3 * Time.deltaTime);
        else
            rb.MovePosition(transform.position +
                Vector3.Normalize(deltaMovement) * moveSpeed * Time.deltaTime);

        //rotation
        if (ifRotateToBullet)
        {
            var newRotation = Quaternion.LookRotation(bulletDirection);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);

        }
        else if (Mathf.Abs(deltaX) > 0.001 || Mathf.Abs(deltaZ) > 0.001)
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
            transform.position -= new Vector3(0, transform.position.y, 0);
        }

    }

    public void Shooting(Vector3 shootVelocity)
    {
        bulletDirection = shootVelocity;
        if (ifRotateToBullet == false)
            StartCoroutine(SetShootingCoroutine());
        if(hp < maxHp)
            ResetHealSelfCoroutine();
    }

    private void ResetHealSelfCoroutine()
    {
        if (HealSelfCoroutine != null)
        {
            StopCoroutine(HealSelfCoroutine);
        }
        HealSelfCoroutine = StartCoroutine(HealSelf());
    }

    IEnumerator HealSelf()
    {
        yield return new WaitForSeconds(durationToWaitForHealSelf);
        if(hp < maxHp)
        {
            AddHp(hpToHealSelf);
            AudioSource.PlayClipAtPoint(
                healingAudio, Camera.main.transform.position, 0.2f);
            HealSelfCoroutine = StartCoroutine(HealSelf());
        }
    }

    public void LoseAbilityToHeal()
    {
        if(HealSelfCoroutine != null)
            StopCoroutine(HealSelfCoroutine);
    }

    IEnumerator SetShootingCoroutine()
    {
        ifRotateToBullet = true;
        yield return new WaitForSeconds(rotateToBulletDuration);
        ifRotateToBullet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == enemyBulletLayer)
        {
            if (!invulnerable)
            {
                AudioSource.PlayClipAtPoint(
                    ScreamingAudio, Camera.main.transform.position, 0.2f);
                DamageDealer damageDealer =
                    other.gameObject.GetComponent<DamageDealer>();
                MinusHp(damageDealer.GetDamage());
                ResetHealSelfCoroutine();
            }
            Destroy(other.gameObject);
        }
    }
}