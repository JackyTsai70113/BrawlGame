using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
    // Character Setting
    private int characterNumber;

    [Header("Hp Setting")]
    public int maxHp;
    public int hp;

    [Header("Tranparent Setting")]
    public Material[] colorMaterials;
    private MeshRenderer[] meshRenders;
    private int grassContacts;

    public AudioClip vanishAudio;

    // GameObjects
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
        InfoSetHP();
    }

    private void InfoSetHP()
    {
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
            case 1: ID = "Bot " + characterNumber;
                break;
            case 2: ID = "Bot " + characterNumber;
                break;
            case 3: ID = "Enemy " + (characterNumber - 2);
                break;
            case 4: ID = "Enemy " + (characterNumber - 2);
                break;
            case 5: ID = "Enemy " + (characterNumber - 2);
                break;
        }
        info.GetComponent<InfoSetter>().SetID(ID);
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
        if (this.hp > maxHp)
            this.hp = maxHp;
        InfoSetHP();
    }

    public void MinusHp(int damage)
    {
        hp -= damage;
        InfoSetHP();
        LayerMask playerLayer = 8;
        LayerMask enemyLayer = 9;
        GameStatus gameStatus = FindObjectOfType<GameStatus>();
        if (hp <= 0)
        {
            if (gameObject.layer == playerLayer)
            {
                AudioSource.PlayClipAtPoint(
                    vanishAudio, Camera.main.transform.position, 20f);
                gameStatus.SetPlayerStatus(false);
                gameStatus.MinusLifeNumber();
                Destroy(totalGameObject);
            }
            else if (gameObject.layer == enemyLayer)
            {
                //FindObjectOfType<Respawner>().
                //    RespawnCharacter(characterNumber);
                AudioSource.PlayClipAtPoint(
                    vanishAudio, Camera.main.transform.position, 20f);
                gameStatus.AddKills();
                FindObjectOfType<Level>().RespawnCharacter(characterNumber);
                Destroy(totalGameObject);
            }
        }
    }

    public int GetGrassContacts()
    {
        return grassContacts;
    }

    public void AddGrassNumber()
    {
        grassContacts++;
    }

    public void MinusGrassNumber()
    {
        grassContacts--;
    }

    public void BodyBeTransparent()
    {

        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = colorMaterials[1];
    }

    public void BodyBeNotTransparent()
    {
        foreach (MeshRenderer mr in meshRenders)
            mr.sharedMaterial = colorMaterials[0];
    }
}