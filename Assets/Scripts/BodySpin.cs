using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySpin : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Start()
    {
        transform.Rotate(Vector3.up,
        rotationSpeed * Time.deltaTime);
    }

    void Update()
    {
        transform.Rotate(Vector3.up,
        rotationSpeed * Time.deltaTime);
    }
}