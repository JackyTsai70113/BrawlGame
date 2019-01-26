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
    public string targetObjectName = "";
    public float strollDistance;
    public float shootingDistance;
    private GameObject target;
    private float distanceFromTarget;
    private LayerMask barrierLayerMask;
    private LayerMask playerLayerMask;
    private LayerMask layerMask;

    [Header("Bullet Setting")]
    [SerializeField] GameObject bulletPrefab;
    public float bulletYPos = 8f;
    public float bulletDistance;

    [Header("Shooting Setting")]
    private bool shooting;
    public float shootSpeed = 10;
    public float shootDuration = 0.5f;
    private Vector3 shootVelocity;

    //Components
    private Rigidbody rb;

    //AutoCounters
    private int moveCounter = 0;
    private int shotCounter = 0;

    void Start()
    {
        //Simplify Component
        rb = GetComponent<Rigidbody>();

        InitialHpAndComponents();
        SetCharacterNumber(1);
        SetTarget();

        //shooting LayerMask
        barrierLayerMask = (1 << 12);
        playerLayerMask = (1 << 8);
        layerMask = barrierLayerMask + playerLayerMask;
    }

    void FixedUpdate()
    {
        distanceFromTarget =
            Vector3.Distance(transform.position, target.transform.position);
        Move();
        Shoot();
    }
    private void SetTarget()
    {
        foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag("Human"))
            if (targetObject != gameObject)
            {
                target = targetObject;
                break;
            }
        if (target == null)
            return;
    }

    public void Move()
    {
        if (!moving)
            return;
        if (moveCounter != 20)
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
            deltaX = moveUnitVector.z;
            deltaZ = moveUnitVector.x;
            rb.velocity = Vector3.Normalize(new Vector3(deltaX, 0, deltaZ)) * unitLength;
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
                rb.velocity = Vector3.Normalize(new Vector3(deltaX, 0, deltaZ)) * moveSpeed;
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
        //Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    public void Shoot()
    {
        if (!isShooting)
            return;
        if (!AbilityToShootPlayer() || 
            (distanceFromTarget > shootingDistance))
            return;
        if (shotCounter != 30)
        {
            shotCounter += 1;
            return;
        }
        shotCounter = 0;
        
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
          unitLength * Vector3.Normalize(target.transform.position - transform.position));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, unitLength, barrierLayerMask))
            return (hit.collider.gameObject.layer == 12);
        return false;
    }

    private bool AbilityToShootPlayer()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, bulletYPos, 0),
          target.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return (hit.collider.gameObject.layer == 8);
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            //Debug.Log("character" + GetCharacterNumber() + "is shot by " +
            //    damageDealer.GetCharacterNumber());
            MinusHp(damageDealer.GetDamage());
            Destroy(other.gameObject, 0.1f);
        }
    }
}