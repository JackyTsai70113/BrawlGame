using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoSetter : MonoBehaviour {

    [Header("Body Setting")]
    public GameObject body;

    private float yPos = 25;

    [SerializeField] TextMeshPro HpText;
    Character character;
    Transform[] transforms;
    // Use this for initialization
    void Start () {
        transform.position =
            body.GetComponent<Rigidbody>().transform.position +
            new Vector3(0, yPos, 0);
        transform.rotation = 
            Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        HpText = GetComponentInChildren<TextMeshPro>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position =
            body.GetComponent<Rigidbody>().transform.position +
            new Vector3(0, yPos, 0);
    }
    public void SetHpText(int hp)
    {
        HpText.text = hp.ToString();
    }
    public void SetHpBar()
    {

    }
}
