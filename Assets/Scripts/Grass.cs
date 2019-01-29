using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    [SerializeField] Material[] materials;
    MeshRenderer meshRender;
	// Use this for initialization
	void Start () {
        meshRender = GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.tag != "Team0" && other.tag != "Team1")
            return;
        other.GetComponent<Character>().AddGrassNumber();
        if(other.tag == "Team0")
            meshRender.sharedMaterial = materials[1];

    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;
        if (other.tag != "Team0" && other.tag != "Team1")
            return;
        other.GetComponent<Character>().MinusGrassNumber();
        if (other.tag == "Team0")
             meshRender.sharedMaterial = materials[0];

    }
}
