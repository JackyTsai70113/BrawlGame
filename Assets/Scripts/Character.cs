using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //[Header("Character Setting")]
    private int characterNumber;

    [Header("Hp Setting")]
    public int maxHp;
    public int hp;
    public float hpPercent;

    [Header("Tranparent Setting")]
    public Material[] colorMaterials;

    private MeshRenderer[] meshRenders;
    private int grassContacts = 0;
    public bool isShooting;

    //GameObjects
    public GameObject info;
        
    
    public void InitialHpAndComponents()
    {
        //Simplify Component
        meshRenders = GetComponentsInChildren<MeshRenderer>();

        //Initial Hp
        hp = maxHp;
        info.GetComponent<InfoSetter>().SetHpText(hp);
    }

    public int GetCharacterNumber()
    {
        return characterNumber;
    }

    public void SetCharacterNumber(int characterNumber)
    {
        this.characterNumber += characterNumber;
    }

    public float GetHp()
    {
        return hp;
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
        info.GetComponent<InfoSetter>().SetHpText(hp);
    }

    public void MinusHp(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Debug.Log("Character " + characterNumber + " dead.");
        info.GetComponent<InfoSetter>().SetHpText(hp);
        
    }

    public float GetHpPercent()
    {
        hpPercent = this.hp / maxHp;
        return hpPercent;
    }

    public void AddGrassNumber()
    {
        grassContacts++;
        if (grassContacts > 0)
            BeTransparent();
    }

    public void MinusGrassNumber()
    {
        grassContacts--;
        if (grassContacts <= 0)
            BeNotTransparent();
    }

    public void BeTransparent()
    {
        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = colorMaterials[1];
    }

    public void BeNotTransparent()
    {
        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = colorMaterials[0];
    }

}