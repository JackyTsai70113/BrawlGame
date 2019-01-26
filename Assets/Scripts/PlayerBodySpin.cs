using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySpin : MonoBehaviour
{
    public float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up,
        rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up,
        rotationSpeed * Time.deltaTime);
    }

}
