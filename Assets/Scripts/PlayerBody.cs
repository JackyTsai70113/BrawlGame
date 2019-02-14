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
    private bool shooting = false;
    private Vector3 bulletDirection;
    private Rigidbody rb;

    [Header("SFX and VFX")]
    public AudioClip healingAudio;
    public AudioClip damagedAudio;
    bool haveRunningParticle;
    public GameObject runningParticleVFX;
    public GameObject healingVFX;

    Coroutine HealSelfCoroutine;
    public float durationToWaitForHealSelf = 3f;
    public int hpToHealSelf;

    LayerMask enemyBulletLayer = 14;

    void Start()
    {
        InitialHpAndComponents();
        SetCharacterNumber(0);
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<Camera>().
            GetComponent<CameraControllerSelf>().SetPlayerBody();
    }

    void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void Update()
    {
        info.GetComponent<InfoSetter>().SetInfoTransform(transform.position);
        if (HealSelfCoroutine == null)
            ResetHealSelfCoroutine();   
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
        if (Mathf.Abs(deltaX) > 0.001 || Mathf.Abs(deltaZ) > 0.001)
            if(!haveRunningParticle)
                StartCoroutine(TriggerRunningParticle());

        //rotation
        if (shooting)
        {
            var newRotation = Quaternion.LookRotation(bulletDirection);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);

        }
        else if (Mathf.Abs(deltaX) > 0.001 || Mathf.Abs(deltaZ) > 0.001)
        {
            var newRotation = Quaternion.Euler(
                0, Mathf.Atan2(deltaX, deltaZ) * 180 / Mathf.PI, 0);
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
        if (shooting == false)
            StartCoroutine(SetShootingCoroutine());
        ResetHealSelfCoroutine();
    }

    private void ResetHealSelfCoroutine()
    {
        if (hp == maxHp)
            return;
        if (HealSelfCoroutine != null)
            StopCoroutine(HealSelfCoroutine);
        HealSelfCoroutine = StartCoroutine(HealSelf());
    }

    IEnumerator HealSelf()
    {
        yield return new WaitForSeconds(durationToWaitForHealSelf);
        if(hp < maxHp)
        {
            AddHp(hpToHealSelf);
            GameObject healingObject = Instantiate(healingVFX, transform);
            Destroy(healingObject, 2f);
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

    private IEnumerator SetShootingCoroutine()
    {
        shooting = true;
        yield return new WaitForSeconds(rotateToBulletDuration);
        shooting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == enemyBulletLayer)
        {
            if (!invulnerable)
            {
                AudioSource.PlayClipAtPoint(
                    damagedAudio, Camera.main.transform.position, 0.2f);
                DamageDealer damageDealer =
                    other.gameObject.GetComponent<DamageDealer>();
                MinusHp(damageDealer.GetDamage());
                ResetHealSelfCoroutine();
            }
            Destroy(other.gameObject);
        }
    }

    public void CheckForTransparent()
    {
        if (GetGrassContacts() > 0)
            BodyBeTransparent();
        else
            BodyBeNotTransparent();
    }

    IEnumerator TriggerRunningParticle()
    {
        haveRunningParticle = true;
        GameObject runningParticleObject = 
            Instantiate(runningParticleVFX, transform);
        Destroy(runningParticleObject, 2f);
        yield return new WaitForSeconds(3f);
        haveRunningParticle = false;
    }
}