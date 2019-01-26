using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    [SerializeField] Material[] materials;
    MeshRenderer[] meshRenders;
	// Use this for initialization
	void Start () {
        meshRenders = GetComponentsInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == null)
            return;
        else if (collider.tag != "Human")
            return;
        collider.GetComponent<Character>().AddGrassNumber();
        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = materials[1];

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider == null)
            return;
        else if (collider.tag != "Human")
            return;
        collider.GetComponent<Character>().MinusGrassNumber();
        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = materials[0];

    }
}
