﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer: MonoBehaviour 
{
    public int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}
