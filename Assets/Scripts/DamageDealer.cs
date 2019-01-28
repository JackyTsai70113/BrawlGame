using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer: MonoBehaviour {

    public int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
        //Debug.Log("set damage:" + damage);
    }

    public int GetDamage()
    {
        //Debug.Log("get damage:" + damage);
        return damage;
    }

    public void Hit()
    {
        //Destroy(gameObject);

    }
}
