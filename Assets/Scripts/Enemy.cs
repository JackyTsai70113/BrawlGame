using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Movement Setting")]
    public bool moving;
    public float moveSpeed = 20;
    public float rotationSpeed = 15;
    public float rotateToBulletDuration = 1f;
    public int unitLength = 10;

    [Header("Target Setting")]
    public string targetObjectTag = "";
    public float strollDistance;
    public float shootingDistance;
    private GameObject target;
    private float distanceFromTarget;
    private LayerMask barrierLayerMask;
    private LayerMask barrierLayer;
    private LayerMask playerLayerMask;
    private LayerMask playerLayer;
    private LayerMask playerBulletLayer;

    [Header("Bullet Setting")]
    [SerializeField] GameObject bulletPrefab;
    public float bulletYPos = 8f;
    public float bulletDistance;

    [Header("Shooting Setting")]
    private bool shooting;
    public float shootSpeed = 10;
    private Vector3 shootVelocity;


    //Components
    private Rigidbody rb;

    //AutoCounters
    private int moveCounter = 0;
    private int shootCounter = 0;

    void Start()
    {
        //Simplify Component
        rb = GetComponent<Rigidbody>();

        InitialHpAndComponents();
        if (gameObject.tag == "Team0")
            targetObjectTag = "Team1";
        else if (gameObject.tag == "Team1")
            targetObjectTag = "Team0";


        //shooting Layer
        barrierLayerMask = 1 << 12;
        playerLayerMask = 1 << 8;
        barrierLayer = 12;
        playerLayer = 8;
        playerBulletLayer = 13;

    }

    void FixedUpdate()
    {
        SetTarget();

        Move();
        Shoot();
    }

    void Update()
    {
        info.GetComponent<InfoSetter>().
            SetInfoTransform(transform.position);
    }

    private void SetTarget()
    {
        if (target != null)
            return;

        foreach (GameObject targetObject in 
            GameObject.FindGameObjectsWithTag(targetObjectTag))
            if (targetObject != gameObject)
            {
                target = targetObject;
                break;
            }
        distanceFromTarget =
            Vector3.Distance(transform.position, target.transform.position);
    }

    public void Move()
    {

        if (!moving)
            return;
        if (moveCounter != 50)
        {
            moveCounter += 1;
            return;
        }
        moveCounter = 0;

        //Movement
        float deltaX = 0;
        float deltaZ = 0;
        Vector3 moveUnitVector = Vector3.Normalize(new Vector3(
            target.transform.position.x - transform.position.x,
            0, target.transform.position.z - transform.position.z));
        if (BarrierExistFront()) {
            deltaX = - moveUnitVector.z;
            deltaZ = moveUnitVector.x;
            rb.velocity = Vector3.Normalize(new Vector3(deltaX, 0, deltaZ)) * 
                moveSpeed;
        }
        else {
            if ( distanceFromTarget > strollDistance)
            {
                deltaX = moveUnitVector.x;
                deltaZ = moveUnitVector.z;
                rb.velocity = moveUnitVector * moveSpeed;
            }
            else
            {
                deltaX = Random.Range(-1.0f, 1.0f);
                deltaZ = Random.Range(-1.0f, 1.0f);
                rb.velocity = Vector3.Normalize(new Vector3(deltaX, 0, deltaZ)) * moveSpeed/2;
            }
        }
        //rotation
        if (shooting)
        {
            var newRotation = Quaternion.LookRotation(shootVelocity);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);
        }
        else 
        {
            var newRotation = Quaternion.LookRotation(rb.velocity);
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        newRotation,
                        rotationSpeed * Time.deltaTime);
        }
    }

    public void Shoot()
    { 
        if (!isShooting)
            return;
        if (!AbilityToShootPlayer() || 
            (distanceFromTarget > shootingDistance))
            return;
        if (shootCounter != 100)
        {
            shootCounter += 1;
            return;
        }

        shootCounter = 0;

        Vector3 bulletStartPos = transform.position +
            transform.up * bulletYPos +
            transform.forward * bulletDistance;
        shootVelocity = Vector3.Normalize(target.transform.position -
            transform.position) * shootSpeed;
        GameObject bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletStartPos,
            Quaternion.LookRotation(shootVelocity));
        bullet.GetComponent<Rigidbody>().velocity = shootVelocity;
        Destroy(bullet, 7f);
        if (shooting == false)
            StartCoroutine(SetShootingCoroutine());
        
    }

    IEnumerator SetShootingCoroutine()
    {
        shooting = true;
        yield return new WaitForSeconds(rotateToBulletDuration);
        //shooting = false;
    }

    private bool BarrierExistFront()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, bulletYPos, 0),
          unitLength * Vector3.Normalize(
            target.transform.position - transform.position));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, unitLength, barrierLayerMask))
            return (hit.collider.gameObject.layer == barrierLayer);
        return false;
    }

    private bool AbilityToShootPlayer()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, bulletYPos, 0),
          target.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,
                barrierLayerMask + playerLayerMask))
            return (hit.collider.gameObject.layer == playerLayer);
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerBulletLayer)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            //Debug.Log("character" + GetCharacterNumber() + "is shot by " +
            //    damageDealer.GetCharacterNumber());
            MinusHp(damageDealer.GetDamage());
            Destroy(other.gameObject, 0.1f);
        }
    }
}