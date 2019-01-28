using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //[Header("Character Setting")]
    private int characterNumber;

    [Header("Hp Setting")]
    public int maxHp;
    public int hp;

    [Header("Tranparent Setting")]
    public Material[] colorMaterials;

    private MeshRenderer[] meshRenders;
    private int grassContacts = 0;
    public bool isShooting;

    public AudioClip vanishAudio;

    //GameObjects
    public GameObject totalGameObject;
    public GameObject info;
        
    
    public void InitialHpAndComponents()
    {
        //Simplify Component
        meshRenders = GetComponentsInChildren<MeshRenderer>();

        //Initial ID
        SetID(characterNumber);

        //Initial Hp
        hp = maxHp;
        info.GetComponent<InfoSetter>().SetHp(hp, maxHp);

       
    }

    public int GetCharacterNumber()
    {
        return characterNumber;
    }

    public void SetCharacterNumber(int characterNumber)
    {
        this.characterNumber = characterNumber;
    }

    public void SetID(int characterNumber)
    {
        string ID = "";
        switch (characterNumber)
        {
            case 0: ID = "Player";
                break;
            case 1: ID = "Bot" + characterNumber;
                break;
            case 2: ID = "Bot" + characterNumber;
                break;
            case 3: ID = "Enemy" + characterNumber;
                break;
            case 4: ID = "Enemy" + characterNumber;
                break;
        }
        info.GetComponent<InfoSetter>().SetID(ID);
    }

    public float GetHp()
    {
        return hp;
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
        if (this.hp > maxHp)
            this.hp = maxHp;
        info.GetComponent<InfoSetter>().SetHp(this.hp, maxHp);
    }

    public void MinusHp(int damage)
    {
        hp -= damage;
        info.GetComponent<InfoSetter>().SetHp(hp, maxHp);
        LayerMask playerLayer = 8;
        LayerMask enemyLayer = 9;
        if (hp <= 0 && gameObject.layer == playerLayer)
        {
            AudioSource.PlayClipAtPoint(
                vanishAudio, Camera.main.transform.position, 10f);
            Destroy(totalGameObject);
            FindObjectOfType<SceneLoader>().LoadLoseScene();
        }
        else if (hp <= 0 && gameObject.layer == enemyLayer)
        {
            //FindObjectOfType<Respawner>().
            //    RespawnCharacter(characterNumber);
            AudioSource.PlayClipAtPoint(
                vanishAudio, Camera.main.transform.position, 10f);
            Destroy(totalGameObject);
            FindObjectOfType<GameStatus>().AddKills();
        }
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